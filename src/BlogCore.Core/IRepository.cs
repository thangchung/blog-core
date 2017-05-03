using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogCore.Core
{
    public interface IRepository<TEntity> where TEntity : EntityBase
    {
        Task<TEntity> GetByIdAsync(int id);
        Task<IEnumerable<TEntity>> ListAsync();
        Task<IEnumerable<TEntity>> ListAsync(ISpecification<TEntity> spec);
        Task<TEntity> AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
    }
}