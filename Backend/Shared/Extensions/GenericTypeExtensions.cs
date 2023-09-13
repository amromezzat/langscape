using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Shared.Extensions
{
    public static class GenericTypeExtensions
    {
        public static IEnumerable<Type> GetDerivedClasses(this Type baseClass)
        {
            return Assembly.GetAssembly(baseClass).GetTypes()
                .Where(type => type.IsSubclassOf(baseClass) && !type.IsAbstract);
        }

        public static IEnumerable<Type> GetImplementingClasses(this Type assignableInterface)
        {
            return Assembly.GetAssembly(assignableInterface).GetTypes()
                .Where(type => assignableInterface.IsAssignableFrom(type) && !type.IsAbstract && type.IsClass);
        }
    }
}