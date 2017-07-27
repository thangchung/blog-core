using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogCore.Core;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BlogCore.Infrastructure.EfCore
{
    public static class EfRepositoryExtensions
    {
        public static async Task<IReadOnlyList<TEntity>> QueryAsync<TDbContext, TEntity>(
            this IEfRepository<TDbContext, TEntity> repo, 
            PageInfo pageInfo,
            params Expression<Func<TEntity, object>>[] includeProperties)
            where TDbContext : DbContext
            where TEntity : EntityBase
        {
            var dbSet = repo.DbContext.Set<TEntity>() as IQueryable<TEntity>;
            foreach (var includeProperty in includeProperties)
            {
                dbSet = dbSet.Include(includeProperty);
            }

            return await dbSet
                .Skip(pageInfo.CurrentPage)
                .Take(pageInfo.TotalPage)
                .AsNoTracking()
                .ToListAsync();
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

        public static async Task<IEnumerable<TEntity>> FindAllAsync<TDbContext, TEntity>(
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

            return await dbSet.Where(filter)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}