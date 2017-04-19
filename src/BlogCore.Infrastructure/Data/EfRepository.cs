using System.Collections.Generic;
using System.Linq;
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

        public virtual TEntity GetById(int id)
        {
            return DbContext.Set<TEntity>()
                .SingleOrDefault(e => e.Id == id);
        }

        public List<TEntity> List()
        {
            return DbContext.Set<TEntity>().ToList();
        }

        public List<TEntity> List(ISpecification<TEntity> spec)
        {
            return DbContext.Set<TEntity>()
                .Include(spec.Include)
                .Where(spec.Criteria)
                .ToList();
        }

        public TEntity Add(TEntity entity)
        {
            DbContext.Set<TEntity>().Add(entity);
            DbContext.SaveChanges();

            return entity;
        }

        public void Delete(TEntity entity)
        {
            DbContext.Set<TEntity>().Remove(entity);
            DbContext.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            DbContext.Entry(entity).State = EntityState.Modified;
            DbContext.SaveChanges();
        }
    }
}