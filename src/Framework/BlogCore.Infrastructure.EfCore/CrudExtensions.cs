using BlogCore.Core;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BlogCore.Infrastructure.EfCore
{
    public interface IRetrieveItemWithPage
    {
        int CurrentPage { get; }
    }

    public static class CrudExtensions
    {
        public static async Task<TCreateItemResponse> CreateItemFlowAsync<TDbContext, TEntity, TCreateItemRequest, TCreateItemResponse>(
            this IEfRepository<TDbContext, TEntity> repo,
            TCreateItemRequest request,
            Func<TCreateItemRequest, TEntity> mapCreateItemFunc,
            Func<TEntity, TCreateItemResponse> mapResponseFunc,
            Action<TEntity> raiseEventAction = null)
            where TEntity : EntityBase
            where TDbContext : DbContext
            where TCreateItemRequest : UseCase.IRequest<TCreateItemResponse>
        {
            var createEntity = mapCreateItemFunc(request);
            var entityCreated = await repo.AddAsync(createEntity);
            raiseEventAction?.Invoke(entityCreated);
            return mapResponseFunc(entityCreated);
        }

        public static async Task<PaginatedItem<TRetrieveItemsResponse>> RetrieveListItemFlowAsync<TDbContext, TEntity, TRetrieveItemsResponse>(
            this IEfRepository<TDbContext, TEntity> repo,
            IOptions<PagingOption> pagingOption,
            IRetrieveItemWithPage request,
            Expression<Func<TEntity, TRetrieveItemsResponse>> expr)
            where TEntity : EntityBase
            where TDbContext : DbContext
        {
            var criterion = new Criterion(request.CurrentPage, pagingOption.Value.PageSize, pagingOption.Value);
            return await repo.QueryAsync(criterion, expr);
        }

        public static async Task<TRetrieveItemResponse> RetrieveItemFlowAsync<TDbContext, TEntity, TRetrieveItemResponse>(
            this IEfRepository<TDbContext, TEntity> repo, Guid id,
            Func<TEntity, TRetrieveItemResponse> mapDataFunc)
            where TEntity : EntityBase
            where TDbContext : DbContext
        {
            var retrieved = await repo.GetByIdAsync(id);
            return mapDataFunc(retrieved);
        }

        public static async Task<TUpdateItemResponse> UpdateItemFlowAsync<TDbContext, TEntity, TUpdateItemResponse>(
            this IEfRepository<TDbContext, TEntity> repo,
            Guid id,
            Func<TEntity, TEntity> updateMappingFunc,
            Func<TEntity, TUpdateItemResponse> mapResponseFunc,
            Action<TEntity> raiseEventAction = null,
            params Expression<Func<TEntity, object>>[] includes)
            where TEntity : EntityBase
            where TDbContext : DbContext
        {
            var item = await repo.GetByIdAsync(id, includes);
            var itemMapped = updateMappingFunc(item);
            var itemUpdated = await repo.UpdateAsync(itemMapped);
            raiseEventAction?.Invoke(itemUpdated);
            return mapResponseFunc(itemUpdated);
        }

        public static async Task<TDeleteItemResponse> DeleteItemFlowAsync<TDbContext, TEntity, TDeleteItemResponse>(
            this IEfRepository<TDbContext, TEntity> repo,
            Guid id,
            Func<TEntity, TDeleteItemResponse> mapResponseFunc,
            Action<TEntity> raiseEventAction = null,
            params Expression<Func<TEntity, object>>[] includes)
            where TEntity : EntityBase
            where TDbContext : DbContext
        {
            var item = await repo.GetByIdAsync(id, includes);
            var itemDeleted = await repo.DeleteAsync(item);
            raiseEventAction?.Invoke(itemDeleted);
            return mapResponseFunc(itemDeleted);
        }

        public static async Task RaiseEventAsync<TEntity>(this IMediator mediator, TEntity entity)
            where TEntity : EntityBase
        {
            foreach (var @event in entity.GetEvents())
                await mediator.Publish(@event);
        }

        public static async Task ValidateRequestAsync<TItemRequest>(this IValidator<TItemRequest> validator, TItemRequest request)
        {
            var failureTask = await validator.ValidateAsync(request);
            var failures = failureTask.Errors
                .Where(error => error != null)
                .ToList();

            if (failures.Count > 0)
            {
                throw new Core.ValidationException("[CRUD] Validation Exception.", failures);
            }
        }
    }
}