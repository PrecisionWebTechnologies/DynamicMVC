using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DynamicMVC.Shared.Extensions
{
    public static class TypeExtensions
    {
        public static bool ImplementsInterface<T>(this Type type)
        {
            return typeof(T).IsAssignableFrom(type);
        }

        public static bool ImplementsInterface(this Type type, Type interfaceType)
        {
            return interfaceType.IsAssignableFrom(type);
        }

        public static IEnumerable<Type> ImplementsInterface<T>(this IEnumerable<Type> types)
        {
            return types.Where(x => x.ImplementsInterface<T>()).ToList();
        }

        public static IEnumerable<Type> ImplementsInterface(this IEnumerable<Type> types, Type interfaceType)
        {
            return types.Where(x => x.ImplementsInterface(interfaceType)).ToList();
        }

        public static IEnumerable<T> SelectInterfacesInAssembly<T>(this Type type) where T : class
        {
            var types = type.Assembly.GetTypes().ImplementsInterface<T>().ToList();
            types = types.Where(x => !(x == typeof(T))).ToList();
            return types.Where(x => !(x is T)).Select(x => (T)Activator.CreateInstance(x)).ToList();
        }

        public static IEnumerable<Type> SelectInterfaceTypesInAssembly(this Type type)
        {
            var types = type.Assembly.GetTypes().ImplementsInterface(type).ToList();
            return types.Where(x => !(x == type) && !x.IsAbstract).ToList();
        }

        public static IEnumerable<Type> SelectInterfaceTypesInAssembly(this Assembly assembly, Type type)
        {
            var types = assembly.GetTypes().ImplementsInterface(type).ToList();
            return types.Where(x => !(x == type) && !x.IsAbstract).ToList();
        }

        public static bool HasAttribute<T>(this Type type)
        {
            return type.GetCustomAttributes().Any(x => x is T);
        }

        public static IEnumerable<Type> GetTypesWithAttribute<T>(this IEnumerable<Type> types) where T : Attribute
        {
            return types.GetPublicNonAbstractTypes().Where(t => t.GetCustomAttributes().Any(x => x.GetType() == typeof(T)));
        }

        public static IEnumerable<Type> GetPublicNonAbstractTypes(this IEnumerable<Type> types)
        {
            return types.Where(t => t != null && t.IsPublic && !t.IsAbstract);
        }
    }
}
