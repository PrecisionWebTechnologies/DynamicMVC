using DynamicMVC.DynamicEntityMetadataLibrary.Interfaces;
using DynamicMVC.Shared.Extensions;
using DynamicMVC.UI.DynamicMVC.Factories;
using DynamicMVC.UI.DynamicMVC.Interfaces;
using DynamicMVC.UI.DynamicMVC.Managers;
using DynamicMVC.UI.DynamicMVC.ViewModelBuilders;
using Microsoft.Practices.Unity;

namespace DynamicMVC.UI.DynamicMVC
{
    public class DynamicMVCUnityConfig
    {
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your types here


            Data.UnityConfig.RegisterTypes(container);
            ReflectionLibrary.UnityConfig.RegisterTypes(container);
            ReflectionLibrary.UnityConfig.InjectedContainer = container;
            DynamicEntityMetadataLibrary.UnityConfig.RegisterTypes(container);
            container.RegisterType<IDynamicCreateViewModelBuilder, DynamicCreateViewModelBuilder>();
            container.RegisterType<IDynamicDeleteViewModelBuilder, DynamicDeleteViewModelBuilder>();
            container.RegisterType<IDynamicEditViewModelBuilder, DynamicEditViewModelBuilder>();
            container.RegisterType<IDynamicDetailsViewModelBuilder, DynamicDetailsViewModelBuilder>();
            container.RegisterType<IDynamicIndexPageViewModelBuilder, DynamicIndexPageViewModelBuilder>();
            container.RegisterType<IDynamicIndexViewModelBuilder, DynamicIndexViewModelBuilder>();
            container.RegisterCollection<IDynamicEditorModelBuilder>();
            container.RegisterType<IDynamicFilterManager, DynamicFilterManager>();
            container.RegisterType<IDynamicFilterFactory, DynamicFilterFactory>();
            container.RegisterCollection<IDynamicDisplayPartialModelBuilder>();
            container.RegisterType<IRequestManager, RequestManager>();
            container.RegisterType<IDynamicEntitySearchManager, DynamicEntitySearchManager>();
            container.RegisterType<IUrlManager, UrlManager>();
            container.RegisterType<IReturnUrlManager, ReturnUrlManager>();
            container.RegisterType<IDynamicMvcManager, DynamicMvcManager>();
            container.RegisterCollection<IDynamicFilter>();
            container.RegisterType<ISelectListItemManager, SelectListItemManager>();
            container.RegisterType<IPagingManager, PagingManager>();
            container.RegisterType<IDynamicPropertyViewModelBuilder, DynamicPropertyViewModelBuilder>();
            container.RegisterCollection<IDynamicMethodInvoker>(typeof(DynamicMVCUnityConfig).Assembly);
        }
    }
}