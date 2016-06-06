using DynamicMVC.EntityMetadataLibrary.Builders;
using DynamicMVC.EntityMetadataLibrary.Interfaces;
using Microsoft.Practices.Unity;

namespace DynamicMVC.EntityMetadataLibrary
{
    public class UnityConfig
    {
        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            ApplicationMetadataLibrary.UnityConfig.RegisterTypes(container);
            container.RegisterType<IEntityMetadataManager, EntityMetadataManager>();
            container.RegisterType<IEntityMetadataBuilder, EntityMetadataBuilder>();
            container.RegisterType<IEntityPropertyMetadataBuilder, EntityPropertyMetadataBuilder>();
        }
    }
}
