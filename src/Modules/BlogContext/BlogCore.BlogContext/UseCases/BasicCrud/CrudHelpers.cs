using BlogCore.Core;
using BlogCore.Infrastructure.EfCore;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace BlogCore.BlogContext.UseCases.BasicCrud
{
    public static class CrudHelpers
    {
        public static async Task<TCreateItemResponse> CreateItemProcessAsync<TEntity, TCreateItemRequest, TCreateItemResponse>(
            IMediator mediator,
            TCreateItemRequest request,
            IValidator<TCreateItemRequest> createItemValidator,
            Func<TCreateItemRequest, Task<TEntity>> extendCreatingItemFunc)
            where TEntity : EntityBase
            where TCreateItemRequest : BlogCore.Infrastructure.UseCase.IRequest<TCreateItemResponse>
        {
            // validate request
            var failures = createItemValidator.Validate(request)
                .Errors
                .Where(error => error != null)
                .ToList();
            if (failures.Count > 0)
            {
                throw new BlogCore.Core.ValidationException("Validation exception", failures);
            }

            // delegate for processing outside
            var entity = await extendCreatingItemFunc(request);

            // raise events
            foreach (var @event in entity.GetEvents())
                await mediator.Publish(@event);

            // create new instance using reflection
            var createType = typeof(TCreateItemResponse).GetTypeInfo();
            var createInstance = Activator.CreateInstance(createType, new object[] { entity.Id });
            var instanceCreated = (TCreateItemResponse)createInstance;

            return await Task.FromResult(instanceCreated);
        }

        public static async Task<PaginatedItem<TRetrieveItemsResponse>> RetrieveItemsProcessAsync<TDbContext, TEntity, TRetrieveItemsResponse>(
            IEfRepository<TDbContext, TEntity> repo,
            IOptions<PagingOption> pagingOption,
            IRetrieveItemWithPage request,
            Expression<Func<TEntity, TRetrieveItemsResponse>> expr)
            where TEntity : EntityBase
            where TDbContext : DbContext
        {
            var criterion = new Criterion(request.CurrentPage, pagingOption.Value.PageSize, pagingOption.Value);
            return await repo.QueryAsync(criterion, expr);
        }

        public static async Task<TRetrieveItemResponse> RetrieveItemProcessAsync<TDbContext, TEntity, TRetrieveItemResponse>(
            IEfRepository<TDbContext, TEntity> repo, Guid id,
            Func<TEntity, TRetrieveItemResponse> mapDataFunc)
            where TEntity : EntityBase
            where TDbContext : DbContext
        {
            var blog = await repo.GetByIdAsync(id);
            return mapDataFunc(blog);
        }

        public static async Task<TUpdateItemResponse> UpdateItemProcessAsync<TDbContext, TEntity, TUpdateItemResponse>(
                IEfRepository<TDbContext, TEntity> repo,
                Guid id,
                Func<TEntity, TEntity> updateMappingFunc,
                Func<TEntity, TUpdateItemResponse> returnMappingFunc)
            where TEntity : EntityBase
            where TDbContext : DbContext
        {
            var blog = await repo.GetByIdAsync(id);
            var blogUpdated = updateMappingFunc(blog);
            return await repo.UpdateAsync(blogUpdated)
                .ContinueWith((updatedTask) =>
                {
                    TEntity returnEntity = updatedTask.GetAwaiter().GetResult();
                    return returnMappingFunc(returnEntity);
                });
        }

        public static async Task<TDeleteItemResponse> DeleteItemProcessAsync<TDbContext, TEntity, TDeleteItemResponse>(
            IEfRepository<TDbContext, TEntity> repo, Guid id)
            where TEntity : EntityBase
            where TDbContext : DbContext
        {
            var item = await repo.GetByIdAsync(id);
            return await repo.DeleteAsync(item)
                .ContinueWith(deletedTask =>
                {
                    var idDeleted = deletedTask.GetAwaiter().GetResult();
                    return (TDeleteItemResponse)Activator.CreateInstance(
                        typeof(TDeleteItemResponse), idDeleted);
                }
            );
        }
    }
}
