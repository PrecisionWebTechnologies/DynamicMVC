using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using DynamicMVC.Data.Interfaces;
using DynamicMVC.DynamicEntityMetadataLibrary.Interfaces;
using DynamicMVC.DynamicEntityMetadataLibrary.Models;
using DynamicMVC.Shared;
using DynamicMVC.UI.DynamicMVC.Interfaces;
using DynamicMVC.UI.DynamicMVC.ViewModels;

namespace DynamicMVC.UI.DynamicMVC.Managers
{
    /// <summary>
    /// Exposes methods called by the client application
    /// </summary>
    public class DynamicMvcManager : IDynamicMvcManager
    {
        private readonly IDynamicEntityMetadataManager _dynamicEntityMetadataManager;
        private readonly INavigationPropertyManager _navigationPropertyManager;

        public DynamicMvcManager(IDynamicEntityMetadataManager dynamicEntityMetadataManager, INavigationPropertyManager navigationPropertyManager)
        {
            _dynamicEntityMetadataManager = dynamicEntityMetadataManager;
            _navigationPropertyManager = navigationPropertyManager;
        }

        /// <summary>
        /// Provides DynamicMVC with everything it needs to read from the client application
        /// </summary>
        public void RegisterDynamicMvc()
        {
            DynamicMVCContext.DynamicEntityMetadatas = _dynamicEntityMetadataManager.GetDynamicEntityMetadatas();
        }

        public DynamicMVCContextOptions Options
        {
            get { return DynamicMVCContext.Options; }
        }
        /// <summary>
        /// Sets routeCollection for models that do not have a controller defined
        /// </summary>
        /// <param CustomPropertyName="routeCollection"></param>
        /// <param name="routeCollection">The route collection for the mvc application.</param>
        public void SetDynamicRoutes(RouteCollection routeCollection)
        {
            var newRouteCollection = new RouteCollection();
            var firstRoute = routeCollection.First();
            newRouteCollection.Add(firstRoute);
            foreach (var dynamicEntityMetadata in DynamicMVCContext.DynamicEntityMetadatas)
            {
                if (dynamicEntityMetadata.ControllerExists())
                {
                    newRouteCollection.MapRoute(
                name: "Dynamic" + dynamicEntityMetadata.TypeName(),
                url: dynamicEntityMetadata.TypeName() + "/{action}/{id}",
                defaults: new { controller = dynamicEntityMetadata.TypeName(), action = "Index", id = UrlParameter.Optional, typeName = dynamicEntityMetadata.TypeName() }
                );
                }
                else
                {
                    newRouteCollection.MapRoute(
                    name: "Dynamic" + dynamicEntityMetadata.TypeName(),
                    url: dynamicEntityMetadata.TypeName() + "/{action}/{id}",
                    defaults: new { controller = "Dynamic", action = "Index", id = UrlParameter.Optional, typeName = dynamicEntityMetadata.TypeName() }
                    );
                }

            }
            foreach (var oldRoute in routeCollection.Where(x => x != firstRoute))
            {
                newRouteCollection.Add(oldRoute);
            }
            routeCollection.Clear();
            foreach (var newRoute in newRouteCollection)
            {
                routeCollection.Add(newRoute);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public int GetItemsCountFunction(Type type)
        {
            using (var dynamicRepository = Container.Resolve<IDynamicRepository>())
            {
                return dynamicRepository.GetItemsCount(type);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public object GetItemByTypeAndKeyFunction(Type type, dynamic keyValue)
        {
            var keyName = DynamicMVCContext.GetDynamicEntityMetadata(type.Name).KeyProperty().PropertyName();
            using (var dynamicRepository = Container.Resolve<IDynamicRepository>())
            {
                return dynamicRepository.GetItem(type, keyName, keyValue);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IEnumerable<dynamic> GetItemByTypeAndPropertyNameFunction(Type type, string propertyName, dynamic propertyValue)
        {
            using (var dynamicRepository = Container.Resolve<IDynamicRepository>())
            {
                return dynamicRepository.GetItems(type,propertyName, propertyValue);
            }
        }

        public string GetSelectItemText(Type type, dynamic value, string textFieldName)
        {
            var textProperty = type.GetProperties().Single(x => x.Name == textFieldName);
            var v = value.ToString();
            IEnumerable<dynamic> items = GetItemByTypeAndPropertyNameFunction(type, textFieldName, v);
            
            if (!items.Any() || items.Count()>1)
                return "";
            return textProperty.GetValue(items.First()).ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public IEnumerable GetItemsByTypeFunction(Type type)
        {
            using (var dynamicRepository = Container.Resolve<IDynamicRepository>())
            {
                return dynamicRepository.GetItems(type);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DynamicMenuItemViewModel> GetDynamicMenuItems()
        {
            var results = new List<DynamicMenuItemViewModel>();
            foreach (var dynamicEntityMetadata in DynamicMVCContext.DynamicEntityMetadatas.Where(x => x.DynamicMenuInfo().HasMenuItem))
            {
                var menuItem = new DynamicMenuItemViewModel(dynamicEntityMetadata, dynamicEntityMetadata.DynamicMenuInfo().MenuItemDisplayName);
                var parentMenu = results.SingleOrDefault(x => x.DisplayName == dynamicEntityMetadata.DynamicMenuInfo().MenuItemCategory);
                if (parentMenu == null)
                {
                    parentMenu = new DynamicMenuItemViewModel(dynamicEntityMetadata.DynamicMenuInfo().MenuItemCategory);
                    results.Add(parentMenu);
                }
                parentMenu.DynamicMenuItemViewModels.Add(menuItem);
            }
            return results;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public DynamicEntityMetadata GetDynamicEntityMetadata(string typeName)
        {
            return DynamicMVCContext.GetDynamicEntityMetadata(typeName);
        }

        /// <summary>
        /// This is called on the create to load related entities
        /// </summary>
        /// <param name="dynamicEntityMetadata"></param>
        /// <param name="item"></param>
        /// <param name="dynamicRepository"></param>
        /// <param name="includes"></param>
        public void LoadCreateIncludes(DynamicEntityMetadata dynamicEntityMetadata, dynamic item, IDynamicRepository dynamicRepository, params string[] includes)
        {
            foreach (var include in includes.ToList())
            {
                var currentInclude = include;
                string subinclude = null;
                if (currentInclude.Contains("."))
                {
                    var index = currentInclude.IndexOf('.');
                    subinclude = currentInclude.Substring(index + 1);
                    currentInclude = include.Substring(0, index);
                }
                currentInclude = currentInclude.Trim();
                var property = dynamicEntityMetadata.DynamicPropertyMetadatas.Single(x => x.PropertyName() == currentInclude);
                if (property.IsDynamicCollection())
                {
                    //its not possible to have a fk to this object b/c its not created yet
                }
                else if (property.IsDynamicEntity())
                {
                    var typeName = property.TypeName();
                    var dynamicMetadata = GetDynamicEntityMetadata(typeName);
                    var id = ((DynamicComplexPropertyMetadata)property).DynamicForiegnKeyPropertyMetadata.GetValueFunction()(item);
                    var value = string.IsNullOrWhiteSpace(subinclude) ?
                        dynamicRepository.GetItem(dynamicMetadata.EntityTypeFunction()(), dynamicMetadata.KeyProperty().PropertyName(), id)
                        : dynamicRepository.GetItem(dynamicMetadata.EntityTypeFunction()(), dynamicMetadata.KeyProperty().PropertyName(), id, subinclude);
                    property.SetValueAction()(item, value);

                    //Add item to the collection for this entity
                    var collectionProperty = _navigationPropertyManager.GetCollectionProperty(dynamicEntityMetadata, property);
                    if (collectionProperty != null)
                    {
                        var collection = collectionProperty.GetValueFunction()(value);
                        //Todo:  throw exception here if collection is null
                        collection .GetType().GetMethod("Add").Invoke(collection, new[] { item });
                    }
                }
            }
        }

    }
}
