using System;
using System.Linq;
using BlogCore.Core;
using Microsoft.EntityFrameworkCore.Query;

namespace BlogCore.Infrastructure.EfCore
{
    internal class Includable<TEntity> : IIncludable<TEntity> 
        where TEntity : EntityBase
    {
        internal Includable(IQueryable<TEntity> queryable)
        {
            // C# 7 syntax, just rewrite it "old style" if you do not have Visual Studio 2017
            Input = queryable ?? throw new ArgumentNullException(nameof(queryable));
        }

        internal IQueryable<TEntity> Input { get; }
    }

    internal class Includable<TEntity, TProperty> :
        Includable<TEntity>, IIncludable<TEntity, TProperty>
        where TEntity : EntityBase
    {
        internal Includable(IIncludableQueryable<TEntity, TProperty> queryable) :
            base(queryable)
        {
            IncludableInput = queryable;
        }

        internal IIncludableQueryable<TEntity, TProperty> IncludableInput { get; }
    }
}