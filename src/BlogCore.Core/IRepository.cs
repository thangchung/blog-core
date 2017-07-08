using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BlogCore.Core
{
    public interface IRepository<TDbContext, TEntity> 
        where TEntity : EntityBase
        where TDbContext : DbContext
    {
        Task<TEntity> GetByIdAsync(Guid id);
        Task<IReadOnlyList<TEntity>> ListAsync();
        Task<IEnumerable<TEntity>> ListAsync(ISpecification<TEntity> spec);
        Task<TEntity> AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
    }
}