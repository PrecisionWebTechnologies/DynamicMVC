using System;
using DynamicMVC.Shared.Enums;
using ReflectionLibrary.Enums;

namespace DynamicMVC.ApplicationMetadataLibrary.Interfaces
{
    /// <summary>
    /// Defines what can be a simple type and parses them from a string
    /// </summary>
    public interface ISimpleTypeParser
    {
        /// <summary>
        /// Used by the application to define what is a simple type
        /// </summary>
        /// <returns></returns>
        Type GetSimpleType();

        /// <summary>
        /// Parses the string value into the correct data type for the simple type
        /// </summary>
        /// <param name="value">string representation of the simple type data</param>
        /// <returns>Returns correct type for the given simple type</returns>
        dynamic Parse(string value);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        SimpleTypeEnum SimpleTypeEnum();
    }
}
