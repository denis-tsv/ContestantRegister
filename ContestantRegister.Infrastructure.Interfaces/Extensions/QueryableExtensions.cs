using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ContestantRegister.Services.Extensions
{
    
    public static class QueryableExtensions
    {
        public static IOrmExtensionsHider OrmExtensionsHider { get; set; }

        public static Task<List<T>> ToListAsync<T>(this IQueryable<T> source)
        {
            return OrmExtensionsHider.ToListAsync(source);
        }

        public static Task<T> SingleOrDefaultAsync<T>(this IQueryable<T> source)
        {
            return OrmExtensionsHider.SingleOrDefaultAsync(source);
        }

        public static Task<T> SingleOrDefaultAsync<T>(this IQueryable<T> source, Expression<Func<T, bool>> predicate)
        {
            return OrmExtensionsHider.SingleOrDefaultAsync(source, predicate);
        }

        public static Task<T> SingleAsync<T>(this IQueryable<T> source, Expression<Func<T, bool>> predicate)
        {
            return OrmExtensionsHider.SingleAsync(source, predicate);
        }

        public static Task<T> SingleAsync<T>(this IQueryable<T> source)
        {
            return OrmExtensionsHider.SingleAsync(source);
        }

        public static Task<bool> AnyAsync<T>(this IQueryable<T> source, Expression<Func<T, bool>> predicate)
        {
            return OrmExtensionsHider.AnyAsync(source, predicate);
        }

        public static Task<T> FirstOrDefaultAsync<T>(this IQueryable<T> source, Expression<Func<T, bool>> predicate)
        {
            return OrmExtensionsHider.FirstOrDefaultAsync(source, predicate);
        }

        public static Task<T> FirstOrDefaultAsync<T>(this IQueryable<T> source)
        {
            return OrmExtensionsHider.FirstOrDefaultAsync(source);
        }

        public static Task<int> CountAsync<T>(this IQueryable<T> source, Expression<Func<T, bool>> predicate)
        {
            return OrmExtensionsHider.CountAsync(source, predicate);
        }

        public static IQueryable<TEntity> Include<TEntity, TProperty>(this IQueryable<TEntity> source, Expression<Func<TEntity, TProperty>> navigationPropertyPath) where TEntity : class
        {
            return OrmExtensionsHider.Include(source, navigationPropertyPath);
        }

        public static IQueryable<TEntity> Include<TEntity>(this IQueryable<TEntity> source, string navigationPropertyPath) where TEntity : class
        {
            return OrmExtensionsHider.Include(source, navigationPropertyPath);
        }
    }
}
