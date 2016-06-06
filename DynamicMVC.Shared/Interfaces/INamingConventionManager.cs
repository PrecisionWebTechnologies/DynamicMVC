using System;
using System.Collections.Generic;

namespace DynamicMVC.Shared.Interfaces
{
    public interface INamingConventionManager
    {
        string GetForiegnKeyByComplexProperty(string typeName, string complexPropertyName);
        bool IsController(Type type);
        string FindControllerName(IEnumerable<string> controllerNames, string typeName);
        string DynamicMenuCategory();
        string FindDefaultPropertyName(string typeName, IEnumerable<string> propertyNames);
    }
}