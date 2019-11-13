using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;

namespace ContestantRegister.Tests
{
    public class ReferencesTests
    {
        [Fact]
        public void CrossContextReferences()
        {
            var contexts = new List<string>
            {
                "Admin", "Frontend"
            };

            var hostAssembly = Assembly.Load("ContestantRegister");
            var assemblies = hostAssembly
                    .GetReferencedAssemblies()
                    .Where(x => x.Name.StartsWith("ContestantRegister"))
                    .Select(Assembly.Load)
                    .ToList();
            assemblies.Add(hostAssembly);

            for (int i = 0; i < contexts.Count; i++)
            {
                for (int j = 0; j < contexts.Count; j++)
                {
                    if (i == j) continue;

                    foreach (var assembly in assemblies)
                    {
                        foreach (var reference in assembly.GetReferencedAssemblies())
                        {
                            //not only reference, but real usage of class from other assembly
                            Assert.False(assembly.FullName.Contains(contexts[i]) && reference.FullName.Contains(contexts[j]), 
                                $"Cross-context reference from '{assembly.FullName}' to '{reference.FullName}'");
                        }
                    }
                }
            }
        }

        [Fact]
        public void CrossLayerReferences()
        {
            var _layers = new List<(string From, string To)>
            {
                ( "UseCases", "DataAccess"),
                ( "UseCases", "Infrastructure.Implementation"),
                ( "UseCases", "DomainServices.Implementation"),
                //Если выделить контроллеры в отельный слой контроллеры, то с них нужно запретить ссылки на DomainServices, DataAccess, Infrastructure (и интерфейсы, и реализация)
            };

            var hostAssembly = Assembly.Load("ContestantRegister");
            var assemblies = hostAssembly
                    .GetReferencedAssemblies()
                    .Where(x => x.Name.StartsWith("ContestantRegister"))
                    .Select(Assembly.Load)
                    .ToList();
            assemblies.Add(hostAssembly);

            foreach (var layer in _layers)
            {
                foreach (var assembly in assemblies)
                {
                    foreach (var reference in assembly.GetReferencedAssemblies())
                    {
                        Assert.False(assembly.FullName.Contains(layer.From) && reference.FullName.Contains(layer.To),
                            $"Cross-layer reference from '{assembly.FullName}' to '{reference.FullName}'");
                    }
                }
            }
        }
    }
}
