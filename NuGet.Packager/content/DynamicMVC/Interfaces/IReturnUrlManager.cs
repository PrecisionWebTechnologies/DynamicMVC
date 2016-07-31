using DynamicMVC.DynamicEntityMetadataLibrary.Models;

namespace DynamicMVC.UI.DynamicMVC.Interfaces
{
    public interface IReturnUrlManager
    {
        string GetReturnUrl(string action, string controllerName, RouteValueDictionaryWrapper routeValueDictionaryWrapper, bool removeNestedReturnUrl = false);
        /// <summary>
        /// ScopeIdentity can be used to allow the create a ReturnUrl that includes the primary key for an entity that has not been created yet.
        /// You should use ScopeIdentity instead of the id value.  
        /// For example, you could have a create actionlink that returns to the edit page once the entity has been created.
        /// </summary>
        /// <param name="returnUrl">The ReturnUrl that may contain ScopeIdentity</param>
        /// <param name="dynamicEntityMetadata"></param>
        /// <param name="createModel"></param>
        /// <returns></returns>
        string ReplaceScopeIdentity(string returnUrl, DynamicEntityMetadata dynamicEntityMetadata, dynamic createModel);
    }
}