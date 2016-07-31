using System.Collections.Generic;
using System.Linq;
using System.Web;
using DynamicMVC.DynamicEntityMetadataLibrary.Models;
using DynamicMVC.Shared.Enums;
using DynamicMVC.UI.DynamicMVC.Extensions;
using DynamicMVC.UI.DynamicMVC.Interfaces;
using ReflectionLibrary.Enums;

namespace DynamicMVC.UI.DynamicMVC.Managers
{
    public class RequestManager : IRequestManager
    {
        private IDictionary<string, object> _routeDataDictionary;
        public IDictionary<string, object> RouteDataDictionary
        {
            get
            {
                if (_routeDataDictionary == null)
                    _routeDataDictionary = HttpContext.Current.Request.RequestContext.RouteData.Values;
                return _routeDataDictionary;
            }
            set { _routeDataDictionary = value; }
        }

      
        private RouteValueDictionaryWrapper _queryStringDictionary;
        
        public RouteValueDictionaryWrapper QueryStringDictionary
        {
            get
            {
                if (_queryStringDictionary == null)
                    _queryStringDictionary = HttpContext.Current.Request.QueryString.ToRouteValues();
                return _queryStringDictionary;
            }
            set { _queryStringDictionary = value; }
        }

        private bool _correctedTypes;
        public void CorrectQuerystringTypes(DynamicEntityMetadata dynamicEntityMetadata)
        {
            if (!_correctedTypes)
            {
                foreach (var key in QueryStringDictionary.GetKeys())
                {
                    var property = dynamicEntityMetadata.DynamicPropertyMetadatas.SingleOrDefault(x => x.PropertyName() == key);
                    if (property != null && property.IsSimple())
                    {
                        var origonalValue = QueryStringDictionary.GetValue(key).ToString();
                        //There is an issue with html.checkbox helper.  It sends down true,false when checked
                        if (property.SimpleTypeEnum() == SimpleTypeEnum.Bool && origonalValue == "true,false")
                            origonalValue = "true";
                        var parsedValue = property.ParseValue()(origonalValue);
                        QueryStringDictionary.SetValue(key, parsedValue);
                    }
                }
                _correctedTypes = true;
            }
        }

        public bool PagingParametersDoNotExist()
        {
            return !QueryStringDictionary.ContainsKey("OrderBy") || !QueryStringDictionary.ContainsKey("Page") ||
                   !QueryStringDictionary.ContainsKey("PageSize");
        }

        public void AddPagingParameters(string defaultOrderBy, int page, int pageSize, string keyName)
        {
            if (defaultOrderBy == null)
                defaultOrderBy = keyName + " Desc";

            QueryStringDictionary.SetValue("OrderBy", defaultOrderBy);
            QueryStringDictionary.SetValue("Page", page);
            QueryStringDictionary.SetValue("PageSize", pageSize);
        }

        public RouteValueDictionaryWrapper RouteValueDictionaryWrapper()
        {
            return QueryStringDictionary;
        }

        public string OrderBy()
        {
            return QueryStringDictionary.GetValue("OrderBy").ToString();
        }

        /// <summary>
        /// A Property List found in query string that is used to filter the properties on a view.  This does not override previous scaffolding rules.
        /// </summary>
        /// <returns></returns>
        public string ViewProperties()
        {
            if (QueryStringDictionary.ContainsKey("ViewProperties"))
                return QueryStringDictionary.GetValue("ViewProperties").ToString();
            return null;
        }
    }
}
