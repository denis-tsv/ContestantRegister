using System;
using System.Collections.Concurrent;

namespace AutoFilter.Filters.Convert
{
    internal class FilterValueConvertersCache
    {
        private static readonly ConcurrentDictionary<Type, IFilverValueConverter> Cache =
            new ConcurrentDictionary<Type, IFilverValueConverter>();

        public static IFilverValueConverter GetConverter(Type type)
        {
            return Cache.GetOrAdd(type, (IFilverValueConverter)Activator.CreateInstance(type));
        }
    }
}
