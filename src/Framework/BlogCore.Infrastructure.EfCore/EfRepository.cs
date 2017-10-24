using BlogCore.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace BlogCore.Infrastructure.EfCore
{
    public class EfRepository<TDbContext, TEntity> : IEfRepository<TDbContext, TEntity>
        where TDbContext : DbContext
        where TEntity : EntityBase
    {
        public TDbContext DbContext { get; private set; }

        public EfRepository(TDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public virtual async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await DbContext.Set<TEntity>()
                .AsNoTracking()
                .SingleOrDefaultAsync(e => e.Id.Equals(id));
        }

        public async Task<IReadOnlyList<TEntity>> ListAsync()
        {
            return await DbContext.Set<TEntity>()
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await DbContext.Set<TEntity>().AddAsync(entity);
            await DbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<Guid> DeleteAsync(TEntity entity)
        {
            DbContext.Set<TEntity>().Remove(entity);
            await DbContext.SaveChangesAsync();
            return await Task.FromResult(entity.Id);
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            DbContext.Entry(entity).State = EntityState.Modified;
            await DbContext.SaveChangesAsync();
            return await Task.FromResult(entity);
        }

        public IObservable<PaginatedItem<TEntity>> ListStream(
            Expression<Func<TEntity, bool>> filter = null, 
            Criterion criterion = null, 
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            if (criterion.PageSize < 1 || criterion.PageSize > criterion.DefaultPagingOption.PageSize)
            {
                criterion.SetPageSize(criterion.DefaultPagingOption.PageSize);
            }

            var queryable = DbContext.Set<TEntity>().AsNoTracking() as IQueryable<TEntity>;
            var totalRecord = queryable.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecord / criterion.PageSize);

            foreach (var includeProperty in includeProperties)
            {
                queryable = queryable.Include(includeProperty);
            }

            IQueryable<TEntity> criterionQueryable = null;
            if (criterion != null && filter != null)
            {
                criterionQueryable = queryable.Skip(criterion.CurrentPage * criterion.PageSize)
                    .Take(criterion.PageSize)
                    .Where(filter);
            }
            else if (criterion == null)
            {
                criterionQueryable = queryable.Skip(criterion.CurrentPage * criterion.PageSize)
                    .Take(criterion.PageSize);
            }
            else if (filter == null)
            {
                criterionQueryable = queryable.Where(filter);
            }

            return Observable.FromAsync(async () =>
                new PaginatedItem<TEntity>(
                    totalRecord,
                    totalPages,
                    await criterionQueryable.ToListAsync()));
        }
    }
}