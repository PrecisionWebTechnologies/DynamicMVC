using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;

namespace ReflectionLibrary.Extensions
{
    internal static class UnityExtensions
    {
        public static void RegisterCollection<T>(this IUnityContainer container)
        {
            var interfaceType = typeof(T);
            var concreteTypes = SelectInterfaceTypesInAssembly(interfaceType);
            foreach (var concreteType in concreteTypes)
            {
                container.RegisterType(interfaceType, concreteType, concreteType.Name);
            }
        }

        private static IEnumerable<Type> SelectInterfaceTypesInAssembly(Type type)
        {
            var types = type.Assembly.GetTypes().ImplementsInterface(type).ToList();
            return types.Where(x => !(x == type) && !x.IsAbstract).ToList();
        }

        private static IEnumerable<Type> ImplementsInterface(this IEnumerable<Type> types, Type interfaceType)
        {
            return types.Where(x => x.ImplementsInterface(interfaceType)).ToList();
        }

        private static bool ImplementsInterface(this Type type, Type interfaceType)
        {
            return interfaceType.IsAssignableFrom(type);
        }
    }
}
