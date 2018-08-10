using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Sombra.Core.Extensions
{
    public static class AssemblyExtensions
    {
        public static IEnumerable<Type> GetGenericInterfaceTypes(this Assembly assembly, Type type)
        {
            return assembly.GetTypes()
                .Where(x =>
                    x.GetInterfaces().Where(i => i.IsGenericType).Any(i => i.GetGenericTypeDefinition() == type) &&
                    !x.IsInterface && !x.IsAbstract);
        }
    }
}