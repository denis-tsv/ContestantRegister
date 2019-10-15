using AutoFilter;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;

namespace ContestantRegister.Cqrs.Features._Common.ListViewModel
{
    public static class OrderByCache
    {
        private static readonly ConcurrentDictionary<Type, string> Cache = new ConcurrentDictionary<Type, string>();

        public static string GetOrderBy(Type type)
        {
            return Cache.GetOrAdd(type, CalcOrderBy(type));
        }

        public static string SetOrderBy(Type type, string property)
        {
            return Cache.AddOrUpdate(type, property, (t, p) => property);
        }

        private static string CalcOrderBy(Type type)
        {
            var orderByProps = TypeInfoCache.GetPublicProperties(type)
                .Select(x => new
                {
                    Property = x,
                    OrderByAttribute = x.GetCustomAttribute(typeof(OrderByAttribute)) as OrderByAttribute
                })
                .Where(x => x.OrderByAttribute != null)
                .ToArray();

            string orderBy = null;
            if (orderByProps.Length == 1)
            {
                orderBy = orderByProps[0].OrderByAttribute.IsDesc ?
                    $"{orderByProps[0].Property.Name}.DESC" :
                    orderByProps[0].Property.Name;
            }

            return orderBy;
        }
    }
}
