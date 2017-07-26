using BlogCore.Core;
using Microsoft.EntityFrameworkCore;

namespace BlogCore.Infrastructure.EfCore
{
    public interface IEfRepository<TDbContext, TEntity> : IRepository<TEntity>
        where TDbContext : DbContext
        where TEntity : EntityBase
    {
        TDbContext DbContext { get; }
    }
}