using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Routing;
using DynamicMVC.Data.Interfaces;
using DynamicMVC.UI.DynamicMVC.ViewModels;

namespace DynamicMVC.UI.DynamicMVC.Interfaces
{
    public interface IDynamicMvcManager
    {
        /// <summary>
        /// Provides DynamicMVC with everything it needs to read from the client application
        /// </summary>
        void RegisterDynamicMvc();
        /// <summary>
        /// Sets routeCollection for models that do not have a controller defined
        /// </summary>
        /// <param CustomPropertyName="routeCollection"></param>
        /// <param name="routeCollection">The route collection for the mvc application.</param>
        void SetDynamicRoutes(RouteCollection routeCollection);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        int GetItemsCountFunction(Type type);

        string GetSelectItemText(Type type, dynamic value, string textFieldName);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        object GetItemByTypeAndKeyFunction(Type type, dynamic keyValue);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        IEnumerable GetItemsByTypeFunction(Type type);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<DynamicMenuItemViewModel> GetDynamicMenuItems();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        DynamicEntityMetadataLibrary.Models.DynamicEntityMetadata GetDynamicEntityMetadata(string typeName);

        /// <summary>
        /// This is called on the create to load related entities
        /// </summary>
        /// <param name="dynamicEntityMetadata"></param>
        /// <param name="item"></param>
        /// <param name="dynamicRepository"></param>
        /// <param name="includes"></param>
        void LoadCreateIncludes(DynamicEntityMetadataLibrary.Models.DynamicEntityMetadata dynamicEntityMetadata, dynamic item, IDynamicRepository dynamicRepository, params string[] includes);

        void RegisterDynamicMvc(IEnumerable<EntityMetadataLibrary.Models.EntityMetadata> entityMetadatas);
        DynamicMVCContextOptions Options { get; }
    }
}