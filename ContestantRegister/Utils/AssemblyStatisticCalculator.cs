using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ContestantRegister.Utils
{
    public class AssemblyStat
    {
        public string Name { get; set; }
        public int References { get; set; }
        public int Referenced { get; set; }
        public int Interfaces { get; set; }
        public int AbstractClasses { get; set; }
        public int StaticClassesWithoutMentods { get; set; }
        public int StaticClassesWithMentods { get; set; }
        public int Classes { get; set; }
        public int Structs { get; set; }
        public int Events { get; set; }
        public int Exceptions { get; set; }
        public int DTOs { get; set; }
        public int Enums { get; set; }
        public int EventArgs { get; set; }
    }

    public class AssemblyStatisticCalculator
    {
        private static HashSet<string> BaseMethods = new HashSet<string>() { "ToString", "Equals", "GetHashCode", "GetType" };
        public static void Caculate()
        {
            var assemblies = Assembly
                .GetEntryAssembly()
                .GetReferencedAssemblies()
                .Where(x => x.Name.StartsWith("ContestantRegister"))
                .Select(Assembly.Load).ToList();
            assemblies.Add(Assembly.GetEntryAssembly());
            var stats = new List<AssemblyStat>();
            var dict = new Dictionary<string, int>();
            foreach (var assembly in assemblies)
            {
                var stat = new AssemblyStat { Name = assembly.GetName().Name };
                var refs = assembly.GetReferencedAssemblies().Where(x => x.Name.StartsWith("ContestantRegister")).ToList();
                stat.References = refs.Count;
                foreach (var r in refs)
                {
                    if (dict.ContainsKey(r.Name))
                    {
                        dict[r.Name] = dict[r.Name] + 1;
                    }
                    else
                        dict[r.Name] = 1;
                }
                var types = assembly.GetTypes();
                stat.Interfaces = types.Count(x => x.IsInterface);
                stat.AbstractClasses = types.Count(x => x.IsClass && x.IsAbstract && !x.IsSealed);
                //https://stackoverflow.com/questions/2639418/use-reflection-to-get-a-list-of-static-classes
                stat.StaticClassesWithoutMentods = types.Count(x => x.IsClass && x.IsAbstract && x.IsSealed && 
                                                                    x.GetMethods().All(m => BaseMethods.Contains(m.Name)));
                stat.StaticClassesWithMentods = types.Count(x => x.IsClass && x.IsAbstract && x.IsSealed &&
                                                                    x.GetMethods().Any(m => !BaseMethods.Contains(m.Name)));
                stat.Classes = types.Count(x => x.IsClass && !x.IsAbstract && !x.IsSealed);
                stat.Structs = types.Count(x => x.IsValueType && !x.IsEnum);
                stat.Enums = types.Count(x => x.IsEnum);
                stat.Exceptions = types.Count(x => typeof(Exception).IsAssignableFrom(x));
                stat.Events = types.Count(x => typeof(Delegate).IsAssignableFrom(x));
                stat.EventArgs = types.Count(x => typeof(EventArgs).IsAssignableFrom(x));
                stat.DTOs = types.Count(IsDtoType);
                stats.Add(stat);
            }
            foreach (var p in dict)
            {
                var stat = stats.First(x => x.Name == p.Key);
                stat.Referenced = p.Value;
            }

            using (var writer = new StreamWriter("stat.csv"))
            using (var csv = new CsvWriter(writer))
            {
                csv.WriteRecords(stats);
            }
        }

        private static bool IsDtoType(Type type)
        {
            if (type.Assembly.GetName().Name == "ContestantRegister.Entities") return false;
            if (!type.IsClass || type.IsAbstract) return false;
            if (type.GetFields().Any() || 
                type.GetMethods().Any(x => !x.Name.StartsWith("get_")  && !x.Name.StartsWith("set_") && !BaseMethods.Contains(x.Name)) || 
                type.GetEvents().Any() ||
                type.GetConstructors().Length != 1)
                return false;

            return type.GetProperties().All(x => x.CanRead && x.CanWrite);
        }
    }
}
