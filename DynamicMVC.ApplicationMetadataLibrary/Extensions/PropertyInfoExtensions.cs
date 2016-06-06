using System;
using System.Reflection;

namespace DynamicMVC.ApplicationMetadataLibrary.Extensions
{
    internal static class PropertyInfoExtensions
    {
        internal static dynamic GetPropertyInfoValueFunction(this PropertyInfo propertyInfo, dynamic item)
        {
            if (item == null)
                throw new Exception("GetPropertyInfoValueFunction should not be called with a null item passed into it.");
            return propertyInfo.GetValue(item);
        }

        internal static void SetPropertyInfoValueFunction(this PropertyInfo propertyInfo, dynamic item, dynamic value)
        {
            propertyInfo.SetValue(item, value);
        }
    }
}
