using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogCore.Core;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BlogCore.Infrastructure.EfCore
{
    /// <summary>
    /// Reference the way to include the relationship at https://stackoverflow.com/questions/38250040/passing-lambda-as-parameter-into-repository-method-in-efcore
    /// TODO: add OrderBy as https://stackoverflow.com/questions/5047561/how-to-pass-an-array-of-orderby-expression-to-a-method
    /// </summary>
    public static class EfRepositoryExtensions
    {
        public static async Task<PaginatedItem<TResponse>> QueryAsync<TDbContext, TEntity, TResponse>(
            this IEfRepository<TDbContext, TEntity> repo,
            Criterion criterion,
            Expression<Func<TEntity, TResponse>> selector,
            params Expression<Func<TEntity, object>>[] includeProperties)
            where TDbContext : DbContext
            where TEntity : EntityBase
            where TResponse : IMessage
        {
            return await GetDataAsync(repo, criterion, selector, null, includeProperties);
        }

        public static async Task<PaginatedItem<TResponse>> FindAllAsync<TDbContext, TEntity, TResponse>(
            this IEfRepository<TDbContext, TEntity> repo,
            Criterion criterion,
            Expression<Func<TEntity, TResponse>> selector,
            Expression<Func<TEntity, bool>> filter,
            params Expression<Func<TEntity, object>>[] includeProperties)
            where TDbContext : DbContext
            where TEntity : EntityBase
            where TResponse : IMessage
        {
            return await GetDataAsync(repo, criterion, selector, filter, includeProperties);
        }

        public static async Task<TEntity> FindOneAsync<TDbContext, TEntity>(
            this IEfRepository<TDbContext, TEntity> repo,
            Expression<Func<TEntity, bool>> filter,
            params Expression<Func<TEntity, object>>[] includeProperties)
            where TDbContext : DbContext
            where TEntity : EntityBase
        {
            var dbSet = repo.DbContext.Set<TEntity>() as IQueryable<TEntity>;
            foreach (var includeProperty in includeProperties)
            {
                dbSet = dbSet.Include(includeProperty);
            }

            return await dbSet.FirstOrDefaultAsync(filter);
        }

        private static async Task<PaginatedItem<TResponse>> GetDataAsync<TDbContext, TEntity, TResponse>(
            IEfRepository<TDbContext, TEntity> repo,
            Criterion criterion,
            Expression<Func<TEntity, TResponse>> selector,
            Expression<Func<TEntity, bool>> filter = null,
            params Expression<Func<TEntity, object>>[] includeProperties)
            where TDbContext : DbContext
            where TEntity : EntityBase
            where TResponse : IMessage
        {
            var queryable = repo.DbContext.Set<TEntity>() as IQueryable<TEntity>;
            var totalRecord = await queryable.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalRecord / criterion.PageSize);

            foreach (var includeProperty in includeProperties)
            {
                queryable = queryable.Include(includeProperty);
            }

            if (filter != null)
                queryable = queryable.Where(filter);

            if (!string.IsNullOrWhiteSpace(criterion.SortBy))
            {
                var isDesc = string.Equals(criterion.SortOrder, "desc", StringComparison.OrdinalIgnoreCase) ? true : false;
                queryable = queryable.OrderByPropertyName(criterion.SortBy, isDesc);
            }
            else
            {
                queryable = queryable.OrderByPropertyName("Id", true);
            }

            if (criterion.CurrentPage > totalPages)
            {
                criterion.SetCurrentPage(totalPages);
            }

            var results = await queryable
                .Skip(criterion.CurrentPage * criterion.PageSize)
                .Take(criterion.PageSize)
                .AsNoTracking()
                .Select(selector)
                .ToListAsync();

            return new PaginatedItem<TResponse>(totalRecord, totalPages, results);
        }
    }

    public class PaginatedItem<TResponse> where TResponse : IMessage
    {
        public PaginatedItem(int totalItems, int totalPages, IReadOnlyList<TResponse> items)
        {
            TotalItems = totalItems;
            TotalPages = totalPages;
            Items = items;
        }

        public int TotalItems { get; private set; }

        public long TotalPages { get; private set; }

        public IReadOnlyList<TResponse> Items { get; private set; }
    }
}