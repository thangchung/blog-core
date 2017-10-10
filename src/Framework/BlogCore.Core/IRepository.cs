using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BlogCore.Core
{
    public interface IRepository<TEntity> where TEntity : EntityBase
    {
        Task<TEntity> GetByIdAsync(Guid id);
        Task<IReadOnlyList<TEntity>> ListAsync();
        Task<TEntity> AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        IObservable<PaginatedItem<TEntity>> ListStream(
            Expression<Func<TEntity, bool>> filter = null, 
            Criterion criterion = null, 
            params Expression<Func<TEntity, object>>[] includeProperties);
    }
}