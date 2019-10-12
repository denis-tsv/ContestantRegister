using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ContestantRegister.Services.Extensions;
using Microsoft.EntityFrameworkCore;

namespace ContestantRegister.Infrastructure.Implementation
{
    public class EfOrmExtensionsHider : IOrmExtensionsHider
    {
        public Task<List<T>> ToListAsync<T>(IQueryable<T> source)
        {
            return EntityFrameworkQueryableExtensions.ToListAsync(source);
        }

        public Task<TSource> SingleOrDefaultAsync<TSource>(IQueryable<TSource> source)
        {
            return EntityFrameworkQueryableExtensions.SingleOrDefaultAsync(source);
        }

        public Task<T> SingleAsync<T>(IQueryable<T> source)
        {
            return EntityFrameworkQueryableExtensions.SingleAsync(source);
        }

        public Task<TSource> SingleOrDefaultAsync<TSource>(IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
        {
            return EntityFrameworkQueryableExtensions.SingleOrDefaultAsync(source, predicate);
        }

        public Task<T> SingleAsync<T>(IQueryable<T> source, Expression<Func<T, bool>> predicate)
        {
            return EntityFrameworkQueryableExtensions.SingleAsync(source, predicate);
        }

        public Task<bool> AnyAsync<T>(IQueryable<T> source, Expression<Func<T, bool>> predicate)
        {
            return EntityFrameworkQueryableExtensions.AnyAsync(source, predicate);
        }

        public Task<T> FirstOrDefaultAsync<T>(IQueryable<T> source, Expression<Func<T, bool>> predicate)
        {
            return EntityFrameworkQueryableExtensions.FirstOrDefaultAsync(source, predicate);
        }

        public Task<T> FirstOrDefaultAsync<T>(IQueryable<T> source)
        {
            return EntityFrameworkQueryableExtensions.FirstOrDefaultAsync(source);
        }

        public Task<int> CountAsync<T>(IQueryable<T> source, Expression<Func<T, bool>> predicate)
        {
            return EntityFrameworkQueryableExtensions.CountAsync(source, predicate);
        }

        public IQueryable<TEntity> Include<TEntity, TProperty>(IQueryable<TEntity> source, Expression<Func<TEntity, TProperty>> navigationPropertyPath) where TEntity : class
        {
            return EntityFrameworkQueryableExtensions.Include(source, navigationPropertyPath);
        }

        public IQueryable<TEntity> Include<TEntity>(IQueryable<TEntity> source, string navigationPropertyPath) where TEntity : class
        {
            return EntityFrameworkQueryableExtensions.Include(source, navigationPropertyPath);
        }
    }
}
