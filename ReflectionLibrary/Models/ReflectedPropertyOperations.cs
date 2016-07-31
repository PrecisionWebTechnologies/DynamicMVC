using System;
using System.Reflection;
using ReflectionLibrary.Interfaces;

namespace ReflectionLibrary.Models
{
    /// <summary>
    /// Provides access to methods that require the underlying reflected property to be defined.
    /// </summary>
    public class ReflectedPropertyOperations : IReflectedPropertyOperations
    {
        /// <summary>
        /// 
        /// </summary>
        public PropertyInfo PropertyInfo { get; set; }

        /// <summary>
        /// Func value , item //return propertyInfo.GetValue(item);
        /// </summary>
        public Func<dynamic, dynamic> GetValueFunction { get; set; }
        public dynamic GetValue(dynamic item)
        {
            if (item == null)
                throw new Exception("GetPropertyInfoValueFunction should not be called with a null item passed into it.");
            return GetValueFunction(item);
        }

        /// <summary>
        /// propertyInfo.SetValue(item, value);
        /// </summary>
        public Action<dynamic, dynamic> SetValueAction { get; set; }
        public void SetPropertyInfoValueFunction(dynamic item, dynamic value)
        {
            SetValueAction(item, value);
        }
    }
}
