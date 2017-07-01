using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogCore.Core;
using Microsoft.EntityFrameworkCore;

namespace BlogCore.Infrastructure.EfCore
{
    public class EfRepository<TDbContext, TEntity> : IRepository<TDbContext, TEntity> 
        where TEntity : EntityBase
        where TDbContext : DbContext
    {
        protected readonly TDbContext DbContext;

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
            return await DbContext.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> ListAsync(ISpecification<TEntity> spec)
        {
            return await DbContext.Set<TEntity>()
                .AsNoTracking()
                .Include(spec.Include)
                .Where(spec.Criteria)
                .ToListAsync();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await DbContext.Set<TEntity>().AddAsync(entity);
            await DbContext.SaveChangesAsync();

            return entity;
        }

        public async Task DeleteAsync(TEntity entity)
        {
            DbContext.Set<TEntity>().Remove(entity);
            await DbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            DbContext.Entry(entity).State = EntityState.Modified;
            await DbContext.SaveChangesAsync();
        }
    }

    public class EfRepository<TEntity> : EfRepository<BlogCoreDbContext, TEntity> where TEntity : EntityBase
    {
        public EfRepository(BlogCoreDbContext dbContext) : base(dbContext)
        {
        }
    }

    public interface IRepository<TDbContext, TEntity> : IRepository<TEntity>
        where TEntity : EntityBase
        where TDbContext : DbContext
    {
           
    }
}