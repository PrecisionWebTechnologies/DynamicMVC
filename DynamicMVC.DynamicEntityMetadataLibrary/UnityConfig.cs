using DynamicMVC.DynamicEntityMetadataLibrary.Builders;
using DynamicMVC.DynamicEntityMetadataLibrary.Interfaces;
using DynamicMVC.DynamicEntityMetadataLibrary.Managers;
using DynamicMVC.DynamicEntityMetadataLibrary.Models;
using DynamicMVC.Shared.Extensions;
using Microsoft.Practices.Unity;
using ReflectionLibrary.Interfaces;

namespace DynamicMVC.DynamicEntityMetadataLibrary
{
    public class UnityConfig
    {
        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IDynamicEntityMetadataManager, DynamicEntityMetadataManager>();
            container.RegisterType<IDynamicEntityMetadataBuilder, DynamicEntityMetadataBuilder>();
            container.RegisterType<INavigationPropertyManager, NavigationPropertyManager>();
            container.RegisterCollection<IDynamicEntityMetadataPropertyFixup>();
            container.RegisterType<IDynamicPropertyMetadataBuilder, DynamicPropertyMetadataBuilder>();
            container.RegisterCollection<IDynamicEntityMetadataValidator>();
            container.RegisterType<IDynamicMethodManager, DynamicMethodManager>();
            container.RegisterType<IDynamicOperationManager, DynamicOperationManager>();
            container.RegisterType<IReflectedClass, ReflectedDynamicClass>();
            container.RegisterType<IReflectedDynamicClass, ReflectedDynamicClass>();
            container.RegisterType<IReflectionManager, ReflectionManager>();
        }
    }
}
