using System;
using System.Collections.Generic;
using System.Linq;
using DynamicMVC.Shared.Interfaces;

namespace DynamicMVC.Shared.Managers
{
    public class NamingConventionManager : INamingConventionManager
    {
        public string GetForiegnKeyByComplexProperty(string typeName, string complexPropertyName)
        {
            return complexPropertyName + "Id";
        }

        public bool IsController(Type type)
        {
            return type.Name.ToUpper().EndsWith("CONTROLLER");
        }

        /// <summary>
        /// Performs a case insenstive search for controller name.
        /// </summary>
        /// <param name="controllerNames">Controller names in the project</param>
        /// <param name="typeName">Dynamimc Entity Type Name</param>
        /// <returns>Returns controller name with origonal casing.</returns>
        public string FindControllerName(IEnumerable<string> controllerNames, string typeName)
        {
            var dictionary = new Dictionary<string, string>();
            foreach (var controllerName in controllerNames)
            {
                if (dictionary.ContainsKey(controllerName.ToUpper()))
                {
                    continue;
                }

                dictionary.Add(controllerName.ToUpper(), controllerName);
            }
            if (dictionary.ContainsKey(typeName.ToUpper() + "CONTROLLER"))
                return dictionary[typeName.ToUpper() + "CONTROLLER"];

            return null;
        }

        public string DynamicMenuCategory()
        {
            return "Dynamic";
        }

        public string FindDefaultPropertyName(string typeName, IEnumerable<string> propertyNames)
        {
            var properties = propertyNames.ToList();
            if (properties.Contains("Name"))
                return "Name";
            if (properties.Any(x => x.Contains("Name")))
                return properties.First(x => x.Contains("Name"));
            if (properties.Contains("Description"))
                return "Description";
            if (properties.Any(x => x.Contains("Description")))
                return properties.First(x => x.Contains("Description"));
            return "Id";
        }
    }
}