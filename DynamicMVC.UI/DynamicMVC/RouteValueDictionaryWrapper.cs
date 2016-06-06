using System.Collections.Generic;
using System.Web.Routing;

namespace DynamicMVC.UI.DynamicMVC
{
    /// <summary>
    /// This class wraps all RouteValueDictionaries in project.  This allows for easier debugging by setting conditional breakpoints in add method.
    /// </summary>
    public class RouteValueDictionaryWrapper
    {
        public RouteValueDictionaryWrapper()
        {
            _routeValueDictionary = new RouteValueDictionary();
        }

        private readonly RouteValueDictionary _routeValueDictionary;

        public RouteValueDictionary GetRouteValueDictionary()
        {
            return _routeValueDictionary;
        }

        public void Add(string key, object value)
        {
            _routeValueDictionary.Add(key, value);
        }

        public void SetValue(string key, object value)
        {
            _routeValueDictionary[key] = value;
        }
        public bool ContainsKey(string key)
        {
            return _routeValueDictionary.ContainsKey(key);
        }

        public object GetValue(string key)
        {
            return _routeValueDictionary[key];
        }

        public void Remove(string key)
        {
            _routeValueDictionary.Remove(key);
        }

        public IEnumerable<string> GetKeys()
        {
            var results = new List<string>();
            foreach (var kvp in _routeValueDictionary)
            {
                results.Add(kvp.Key);
            }
            return results;
        }
    }
}