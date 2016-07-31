using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ReflectionLibrary.Interfaces;

namespace ReflectionLibrary.Managers
{
    /// <summary>
    /// Custom Attribute Provider Manager defines how to query .net types for attributes
    /// </summary>
    public class CustomAttributeProviderManager : ICustomAttributeProviderManager
    {
        /// <summary>
        /// Get Custom Attributes including inheritence attributes
        /// </summary>
        /// <param name="customAttributeProvider">An object that implements ICustomAttributeProvider</param>
        /// <returns></returns>
        public ICollection<Attribute> GetAttributes(ICustomAttributeProvider customAttributeProvider)
        {
            return customAttributeProvider.GetCustomAttributes(true).ToList().Select(x => (Attribute)x).ToList();
        }

        /// <summary>
        /// Determine if a class that implements ICustomAttributeProvider has a particular attribute
        /// </summary>
        /// <typeparam name="T">The type of attribute to look for</typeparam>
        /// <param name="customAttributeProvider">An object that implements ICustomAttributeProvider</param>
        /// <returns>Returns true if the class is decorated with the specified attribute.</returns>
        public bool HasAttribute<T>(ICustomAttributeProvider customAttributeProvider)
        {
            return GetAttributes(customAttributeProvider).Any(x => x.GetType() == typeof (T));
        }
    }
}
