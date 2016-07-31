using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web.Mvc;

namespace DynamicMVC.UI.DynamicMVC.Extensions
{
    public static class RouteValueExtensions
    {
        public static RouteValueDictionaryWrapper ToRouteValues(this NameValueCollection queryString)
        {
            if (queryString == null || queryString.HasKeys() == false) return new RouteValueDictionaryWrapper();

            if (queryString.AllKeys.Distinct().Count() != queryString.AllKeys.Count())
                throw new Exception("ToRouteValues method was called with duplicate querystring key values");
            var routeValues = new RouteValueDictionaryWrapper();
            foreach (string key in queryString.AllKeys)
                routeValues.Add(key, queryString[key]);

            return routeValues;
        }

        public static RouteValueDictionaryWrapper ToRouteValues(this IDictionary<string, object> queryString)
        {
            if (queryString == null || queryString.Keys.Count == 0) return new RouteValueDictionaryWrapper();
            var routeValueDictionaryWrapper = new RouteValueDictionaryWrapper();
            foreach (string key in queryString.Keys)
                routeValueDictionaryWrapper.Add(key, queryString[key]);

            return routeValueDictionaryWrapper;
        }

        public static RouteValueDictionaryWrapper ToRouteValues(this NameValueCollection queryString, Object obj)
        {
            var routeValueDictionaryWrapper = new RouteValueDictionaryWrapper();
            if (queryString != null)
            {
                if (queryString.AllKeys.Distinct().Count() != queryString.AllKeys.Count())
                    throw new Exception("ToRouteValues method was called with duplicate querystring key values");
                foreach (string key in queryString)
                {
                    //values passed in object override those already in collection
                    if (!routeValueDictionaryWrapper.ContainsKey(key))
                        routeValueDictionaryWrapper.SetValue(key, queryString[key]);
                }
            }
            return routeValueDictionaryWrapper;
        }

        public static RouteValueDictionaryWrapper ToRouteValues(this FormCollection formCollection)
        {
            var routeValueDictionaryWrapper = new RouteValueDictionaryWrapper();
            foreach (var key in formCollection.AllKeys)
            {
                if (key == "__RequestVerificationToken") continue;
                routeValueDictionaryWrapper.SetValue(key, formCollection[key]);
            }
            return routeValueDictionaryWrapper;
        }
        public static IDictionary<string, object> RouteValueDictionaryTypeCorrection(this IDictionary<string, object> routeValueDictionary, DynamicEntityMetadataLibrary.Models.DynamicEntityMetadata dynamicEntityMetadata)
        {
            foreach (var propertyInfo in dynamicEntityMetadata.DynamicPropertyMetadatas.Where(x => routeValueDictionary.ContainsKey(x.PropertyName())))
            {
                if (propertyInfo.TypeName() == "int")
                {
                    routeValueDictionary[propertyInfo.PropertyName()] = int.Parse(routeValueDictionary[propertyInfo.PropertyName()].ToString());
                }
            }
            return routeValueDictionary;
        }

        public static RouteValueDictionaryWrapper CloneAndAddPage(this RouteValueDictionaryWrapper routeValueDictionaryWrapper, int pagesToAdd)
        {
            var result = routeValueDictionaryWrapper.Clone();
            var page = int.Parse(result.GetValue("Page").ToString());
            page = page + pagesToAdd;
            if (page < 1)
                page = 1;
            result.SetValue("Page", page);

            return result;
        }

        public static RouteValueDictionaryWrapper Clone(this RouteValueDictionaryWrapper routeValueDictionaryWrapper)
        {
            var result = new RouteValueDictionaryWrapper();
            foreach (var kvp in routeValueDictionaryWrapper.GetRouteValueDictionary())
            {
                result.Add(kvp.Key, kvp.Value);
            }
            return result;
        }
    }
}
