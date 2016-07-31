using DynamicMVC.DynamicEntityMetadataLibrary.Models;
using DynamicMVC.UI.DynamicMVC.Extensions;
using DynamicMVC.UI.DynamicMVC.Interfaces;

namespace DynamicMVC.UI.DynamicMVC.Managers
{
    public class ReturnUrlManager : IReturnUrlManager
    {
        private readonly IUrlManager _urlManager;

        public ReturnUrlManager(IUrlManager urlManager)
        {
            _urlManager = urlManager;
        }

        public string GetReturnUrl(string action, string controllerName, RouteValueDictionaryWrapper routeValueDictionaryWrapper, bool removeNestedReturnUrl = false)
        {
            var routeValueDictionary = routeValueDictionaryWrapper;
            if (removeNestedReturnUrl)
            {
                routeValueDictionary = routeValueDictionaryWrapper.Clone();
                routeValueDictionary.Remove("ReturnUrl");
            }
            return _urlManager.Url.Action(action, controllerName, routeValueDictionary.GetRouteValueDictionary());
        }

        /// <summary>
        /// ScopeIdentity can be used to allow the create a ReturnUrl that includes the primary key for an entity that has not been created yet.
        /// You should use ScopeIdentity instead of the id value.  
        /// For example, you could have a create actionlink that returns to the edit page once the entity has been created.
        /// </summary>
        /// <param name="returnUrl">The ReturnUrl that may contain ScopeIdentity</param>
        /// <param name="dynamicEntityMetadata"></param>
        /// <param name="createModel"></param>
        /// <returns></returns>
        public string ReplaceScopeIdentity(string returnUrl, DynamicEntityMetadata dynamicEntityMetadata, dynamic createModel)
        {
            if (returnUrl.Contains("ScopeIdentity"))
            {
                var keyValue = dynamicEntityMetadata.KeyProperty().GetValueFunction()(createModel);
                returnUrl = returnUrl.Replace("ScopeIdentity", keyValue.ToString());
            }
            return returnUrl;
        }
    }
}
