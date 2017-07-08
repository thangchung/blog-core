using System;
using System.Linq.Expressions;

namespace BlogCore.Core
{
    public interface ISpecification<TEntity>
    {
        Expression<Func<TEntity, bool>> Criteria { get; }
        Expression<Func<TEntity, object>> Include { get; }
    }
}