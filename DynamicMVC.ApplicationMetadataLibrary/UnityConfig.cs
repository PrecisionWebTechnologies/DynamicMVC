using System;
using DynamicMVC.ApplicationMetadataLibrary.Builders;
using DynamicMVC.ApplicationMetadataLibrary.Interfaces;
using DynamicMVC.ApplicationMetadataLibrary.Managers;
using DynamicMVC.ApplicationMetadataLibrary.Models;
using DynamicMVC.Shared.Extensions;
using DynamicMVC.Shared.Interfaces;
using DynamicMVC.Shared.Managers;
using Microsoft.Practices.Unity;

namespace DynamicMVC.ApplicationMetadataLibrary
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
            container.RegisterType<IApplicationControllerMethodMetadataBuilder, ApplicationControllerMethodMetadataBuilder>();
            container.RegisterType<INamingConventionManager, NamingConventionManager>();
            container.RegisterType<ITypeManager, TypeManager>();
            container.RegisterType<IApplicationEntityMetadataPropertyBuilder, ApplicationEntityMetadataPropertyBuilder>();
            container.RegisterType<IApplicationControllerMetadataBuilder, ApplicationControllerMetadataBuilder>();
            container.RegisterType<IApplicationEntityBuilder, ApplicationEntityBuilder>();
            container.RegisterType<IApplicationEntityMetadataBuilder, ApplicationEntityMetadataBuilder>();
            container.RegisterType<IApplicationMetadataManager, ApplicationMetadataManager>();
            container.RegisterType<IApplicationMetadataValidationManager, ApplicationMetadataValidationManager>();
            container.RegisterCollection<IApplicationMetadataSummaryValidator>();
            container.RegisterCollection<IApplicationMetadataProviderValidator>();
            container.RegisterType<ApplicationMetadataSummary, ApplicationMetadataSummary>();
            container.RegisterCollection<IApplicationMetadataSummaryPreValidateProcess>();
            container.RegisterCollection<ISimpleTypeParser>();
            container.RegisterType<IReflectedClassesBuilder, ReflectedClassesBuilder>();
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
