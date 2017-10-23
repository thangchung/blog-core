using BlogCore.Core;
using BlogCore.Infrastructure.EfCore;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace BlogCore.BlogContext.UseCases.BasicCrud
{
    public static class CrudHelpers
    {
        public static async Task<TCreateItemResponse> CreateItemHandler<TEntity, TCreateItemRequest, TCreateItemResponse>(
            IMediator mediator,
            TCreateItemRequest request,
            IValidator<TCreateItemRequest> createItemValidator,
            Func<TCreateItemRequest, Task<TEntity>> extendCreatingItemFunc)
            where TEntity : EntityBase
            where TCreateItemRequest : BlogCore.Infrastructure.UseCase.IRequest<TCreateItemResponse>
        {
            // validate request
            var validationResult = createItemValidator.Validate(request);
            if (validationResult.IsValid == false)
            {
                return await Task.FromResult((TCreateItemResponse)Activator.CreateInstance(typeof(TCreateItemResponse), Guid.Empty, validationResult));
            }

            var entity = await extendCreatingItemFunc(request);

            // raise events
            foreach (var @event in entity.GetEvents())
                await mediator.Publish(@event);

            // compose and return values
            return await Task.FromResult(
                (TCreateItemResponse)Activator.CreateInstance(
                    typeof(TCreateItemResponse).GetTypeInfo(),
                    entity.Id,
                    validationResult));
        }

        public static async Task<PaginatedItem<TRetrieveItemsResponse>> RetrieveItemsHandler<TDbContext, TEntity, TRetrieveItemsResponse>(
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

        public static async Task<TDeleteItemResponse> DeleteItemProcess<TDbContext, TEntity, TDeleteItemResponse>(
              IEfRepository<TDbContext, TEntity> repo,
              IDeleteItemWithIdentity request
            )
            where TEntity : EntityBase
            where TDbContext : DbContext
        {
            var item = await repo.GetByIdAsync(request.Id);
            return await repo.DeleteAsync(item)
                .ContinueWith(a =>
                    (TDeleteItemResponse)Activator.CreateInstance(typeof(TDeleteItemResponse).GetTypeInfo()));
        }
    }
}
