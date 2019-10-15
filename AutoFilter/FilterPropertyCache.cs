using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;

namespace AutoFilter
{
    internal static class FilterPropertyCache
    {
        private static readonly ConcurrentDictionary<Type, FilterProperty[]> Cache = new ConcurrentDictionary<Type, FilterProperty[]>();

        public static FilterProperty[] GetFilterProperties(Type type)
        {
            return Cache.GetOrAdd(type, CalcFilterProperties(type));
        }

        private static FilterProperty[] CalcFilterProperties(Type type)
        {
            var props = TypeInfoCache
                .GetPublicProperties(type)
                .Select(x => new FilterProperty
                {
                    PropertyInfo = x,
                    FilterPropertyAttribute = x.GetCustomAttribute<FilterPropertyAttribute>()                        
                })
                .ToArray();

            //у каждого свойства дожне быть атрибут, чтобы кешировать вычисленный PropertyExpression
            foreach(var x in props)
            {
                if (x.FilterPropertyAttribute == null)
                    x.FilterPropertyAttribute = new FilterPropertyAttribute();
            }

            return props;
        }
    }
}
