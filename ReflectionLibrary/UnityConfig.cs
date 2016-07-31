using System;
using Microsoft.Practices.Unity;
using ReflectionLibrary.Builders;
using ReflectionLibrary.Extensions;
using ReflectionLibrary.Interfaces;
using ReflectionLibrary.Managers;
using ReflectionLibrary.Models;

namespace ReflectionLibrary
{
    public class UnityConfig
    {
        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        // ReSharper disable once ParameterHidesMember
        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<ICustomAttributeProviderManager, CustomAttributeProviderManager>();
            container.RegisterType<IReflectedClass, ReflectedClass>();
            container.RegisterType<IReflectedClassBuilder, ReflectedClassBuilder>();
            container.RegisterType<IReflectedLibraryManager, ReflectionLibraryManager>();
            container.RegisterType<IReflectedMethod, ReflectedMethod>();
            container.RegisterType<IReflectedMethodBuilder, ReflectedMethodBuilder>();
            container.RegisterType<IReflectedProperty, ReflectedProperty>();
            container.RegisterType<IReflectedPropertyBuilder, ReflectedPropertyBuilder>();
            container.RegisterType<IAttributeMergeManager, AttributeMergeManager>();
            container.RegisterType<IReflectedMethodOperations, ReflectedMethodOperations>();
            container.RegisterType<IReflectedClassOperations, ReflectedClassOperations>();
            container.RegisterType<IReflectedPropertyOperations, ReflectedPropertyOperations>();
            container.RegisterType<IPropertyTypeManager, PropertyTypeManager>();
            container.RegisterCollection<ISimpleTypeParser>();
        }

        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            if (InjectedContainer != null)
                return InjectedContainer;
            return container.Value;
        }

        public static IUnityContainer InjectedContainer { get; set; }
        #endregion
    }
}
