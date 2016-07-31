using System;
using System.Reflection;

namespace ReflectionLibrary.Interfaces
{
    /// <summary>
    /// Provides access to methods that require the underlying reflected property to be defined.
    /// </summary>
    public interface IReflectedPropertyOperations
    {
        /// <summary>
        /// Func value , item
        /// </summary>
        Func<object, object> GetValueFunction { get; set; }

        /// <summary>
        /// propertyInfo.SetValue(item, value);
        /// </summary>
        Action<object, object> SetValueAction { get; set; }

        /// <summary>
        /// Used to access functionality other than what is accessable in Reflected Property Operations
        /// </summary>
        PropertyInfo PropertyInfo { get; set; }
    }
}
