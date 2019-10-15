using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AutoFilter.Extensions
{
    public static class QuerableExtensions
    {
        public static IQueryable<T> Where<T, TParam>(this IQueryable<T> queryable,
            Expression<Func<T, TParam>> first, Expression<Func<TParam, bool>> second)
        {
            return queryable.Where(first.Combine(second));
        }

        public static IQueryable<T> WhereAny<T, TParam>(this IQueryable<T> queryable,
            Expression<Func<T, IEnumerable<TParam>>> first, Expression<Func<TParam, bool>> second)
        {
            return queryable.Where(first.Combine(second));
        }
    }
}
