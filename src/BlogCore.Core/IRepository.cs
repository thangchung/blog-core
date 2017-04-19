using System.Collections.Generic;

namespace BlogCore.Core
{
    public interface IRepository<TEntity> where TEntity : EntityBase
    {
        TEntity GetById(int id);
        List<TEntity> List();
        List<TEntity> List(ISpecification<TEntity> spec);
        TEntity Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}