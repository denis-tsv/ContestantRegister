using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AutoFilter
{
    public static class TypeInfoCache
    {
        private static readonly ConcurrentDictionary<Type, PropertyInfo[]> PropertiesCache =
            new ConcurrentDictionary<Type, PropertyInfo[]>();

        private static readonly ConcurrentDictionary<Type, Dictionary<string, PropertyInfo>> PropertiesDictionaryCache =
            new ConcurrentDictionary<Type, Dictionary<string, PropertyInfo>>();

        private static readonly ConcurrentDictionary<Type, MethodInfo[]> MethodsCache =
            new ConcurrentDictionary<Type, MethodInfo[]>();


        public static PropertyInfo[] GetPublicProperties(Type type)
        {
            return PropertiesCache.GetOrAdd(type, type
                .GetProperties()
                .Where(x => x.CanRead && x.CanWrite)
                .ToArray());
        }

        public static IReadOnlyDictionary<string, PropertyInfo> GetPublicPropertiesDictionary(Type type)
        {
            return PropertiesDictionaryCache.GetOrAdd(type, GetPublicProperties(type).ToDictionary(x => x.Name, x => x));
        }

        public static MethodInfo[] GetPublicMethods(Type type)
        {
            return MethodsCache.GetOrAdd(type, type
                .GetMethods()
                .Where(x => x.IsPublic && !x.IsAbstract)
                .ToArray());
        }
    }
}
