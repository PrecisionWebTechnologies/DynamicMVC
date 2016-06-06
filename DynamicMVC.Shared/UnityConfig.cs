using System;
using DynamicMVC.Shared.Interfaces;
using DynamicMVC.Shared.Managers;
using Microsoft.Practices.Unity;

namespace DynamicMVC.Shared
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
            container.RegisterType<INamingConventionManager, NamingConventionManager>();
            container.RegisterType<IValidationManager, ValidationManager>();
            container.RegisterType<IPropertyFilterManager, PropertyFilterManager>();
            ReflectionLibrary.UnityConfig.RegisterTypes(container);
            ReflectionLibrary.UnityConfig.InjectedContainer = container;
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
            return container.Value;
        }
        #endregion
    }
}
