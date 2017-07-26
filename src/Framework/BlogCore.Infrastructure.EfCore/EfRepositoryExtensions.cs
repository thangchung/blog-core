using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogCore.Core;
using Microsoft.EntityFrameworkCore;

namespace BlogCore.Infrastructure.EfCore
{
    public static class EfRepositoryExtensions
    {
        public static async Task<IReadOnlyList<TEntity>> ListAsync<TDbContext, TEntity>(this IEfRepository<TDbContext, TEntity> repo, PageInfo pageInfo, Action<DbSet<TEntity>> doAnother = null)
            where TDbContext : DbContext
            where TEntity : EntityBase
        {
            var dbSet = repo.DbContext.Set<TEntity>();
            doAnother?.Invoke(dbSet);
            return await dbSet
                .Skip(pageInfo.CurrentPage)
                .Take(pageInfo.TotalPage)
                .AsNoTracking()
                .ToListAsync();
        }

        public static async Task<IEnumerable<TEntity>> ListAsync<TDbContext, TEntity>(this IEfRepository<TDbContext, TEntity> repo, ISpecification<TEntity> spec)
            where TDbContext : DbContext
            where TEntity : EntityBase
        {
            return await repo.DbContext.Set<TEntity>()
                .AsNoTracking()
                .Include(spec.Include)
                .Where(spec.Criteria)
                .ToListAsync();
        }
    }
}