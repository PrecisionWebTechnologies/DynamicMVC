using System.Reflection;
using Microsoft.Practices.Unity;

namespace DynamicMVC.Shared.Extensions
{
    public static class UnityExtensions
    {
        public static void RegisterCollection<T>(this IUnityContainer container)
        {
            var interfaceType = typeof(T);
            var concreteTypes = interfaceType.SelectInterfaceTypesInAssembly();
            foreach (var concreteType in concreteTypes)
            {
                container.RegisterType(interfaceType, concreteType, concreteType.Name);
            }
        }

        public static void RegisterCollection<T>(this IUnityContainer container, Assembly assembly)
        {
            var interfaceType = typeof(T);
            var concreteTypes = assembly.SelectInterfaceTypesInAssembly(interfaceType);
            foreach (var concreteType in concreteTypes)
            {
                container.RegisterType(interfaceType, concreteType, concreteType.Name);
            }
        }
    }
}
