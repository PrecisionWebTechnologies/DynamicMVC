using System;
using System.Collections.Generic;
using System.Reflection;

namespace ReflectionLibrary.Interfaces
{
    /// <summary>
    /// Custom Attribute Provider Manager defines how to query .net types for attributes
    /// </summary>
    public interface ICustomAttributeProviderManager
    {
        /// <summary>
        /// Get Custom Attributes including inheritence attributes
        /// </summary>
        /// <param name="customAttributeProvider">An object that implements ICustomAttributeProvider</param>
        /// <returns></returns>
        ICollection<Attribute> GetAttributes(ICustomAttributeProvider customAttributeProvider);

        /// <summary>
        /// Determine if a class that implements ICustomAttributeProvider has a particular attribute
        /// </summary>
        /// <typeparam name="T">The type of attribute to look for</typeparam>
        /// <param name="customAttributeProvider">An object that implements ICustomAttributeProvider</param>
        /// <returns>Returns true if the class is decorated with the specified attribute.</returns>
        bool HasAttribute<T>(ICustomAttributeProvider customAttributeProvider);
    }
}
