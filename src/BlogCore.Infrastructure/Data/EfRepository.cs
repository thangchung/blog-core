using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogCore.Core;
using Microsoft.EntityFrameworkCore;

namespace BlogCore.Infrastructure.Data
{
    public class EfRepository<TEntity> : IRepository<TEntity> where TEntity : EntityBase
    {
        protected readonly BlogCoreDbContext DbContext;

        public EfRepository(BlogCoreDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            return await DbContext.Set<TEntity>()
                .SingleOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<TEntity>> ListAsync()
        {
            return await DbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> ListAsync(ISpecification<TEntity> spec)
        {
            return await DbContext.Set<TEntity>()
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
}