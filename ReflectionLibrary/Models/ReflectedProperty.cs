using System;
using System.Collections.Generic;
using ReflectionLibrary.Interfaces;
#pragma warning disable 1591

namespace ReflectionLibrary.Models
{
    public class ReflectedProperty : IReflectedProperty
    {
        public ReflectedProperty()
        {
            Attributes = new HashSet<Attribute>();
        }

        public string PropertyName { get; set; }
        public string PropertyType { get; set; }
        public bool IsNullable { get; set; }

        public IReflectedClass ReflectedClass { get; set; }
        public ICollection<Attribute> Attributes { get; set; }

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

        public override string ToString()
        {
            return String.Format("{0}({1}) - {2} Attributes", PropertyName, PropertyType, Attributes.Count);
        }
    }
}
