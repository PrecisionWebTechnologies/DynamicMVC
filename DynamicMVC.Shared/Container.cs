using Microsoft.Practices.Unity;

namespace DynamicMVC.Shared
{
    public static class Container
    {
        public static void RegisterInstance<T>(T item) where T : class
        {
            GetConfiguredContainer().RegisterInstance(item);   
        }

        public static T Resolve<T>() where T : class
        {
            return GetConfiguredContainer().Resolve<T>();
        }

        public static T Resolve<T>(string name) where T : class
        {
            return GetConfiguredContainer().Resolve<T>(name);
        }

        public static IUnityContainer EagerLoadedContainer { get; set; }
        
        public static IUnityContainer GetConfiguredContainer()
        {
            if (EagerLoadedContainer!=null)
                return EagerLoadedContainer;

           return UnityConfig.GetConfiguredContainer();    
        }

        public static void EagerLoad()
        {
            var container = new UnityContainer();
            UnityConfig.RegisterTypes(container);
            EagerLoadedContainer = container;
        }
    }
}
