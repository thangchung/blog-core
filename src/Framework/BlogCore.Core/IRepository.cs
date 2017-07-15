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
        Task<IReadOnlyList<TEntity>> ListAsync(PageInfo pageInfo, Func<IIncludable<TEntity>, IIncludable> includes = null);
        Task<IEnumerable<TEntity>> ListAsync(ISpecification<TEntity> spec);
        Task<TEntity> AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
    }

    /// <summary>
    /// Reference at https://stackoverflow.com/questions/42904414/multiple-includes-in-ef-core
    /// </summary>
    public interface IIncludable
    {
    }

    public interface IIncludable<out TEntity> : IIncludable
        where TEntity : EntityBase
    {
    }

    public interface IIncludable<out TEntity, out TProperty> : IIncludable<TEntity>
        where TEntity : EntityBase
    {
    }
}