using System;
using System.Linq;
using BlogCore.Core;

namespace BlogCore.Infrastructure.EfCore
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> IncludeMultiple<T>(this IQueryable<T> query,
            Func<IIncludable<T>, IIncludable> includes)
            where T : EntityBase
        {
            if (includes == null)
                return query;

            var includable = (Includable<T>) includes(new Includable<T>(query));
            return includable.Input;
        }
    }
}