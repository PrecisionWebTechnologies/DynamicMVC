using System;
using System.Collections.Generic;

namespace ReflectionLibrary.Interfaces
{
    public interface IReflectedProperty : IEntityWithAttributes
    {
        string PropertyName { get; set; }
        IReflectedClass ReflectedClass { get; set; }
        ICollection<Attribute> Attributes { get; set; }
        string PropertyType { get; set; }
        /// <summary>
        /// Func value , item
        /// </summary>
        Func<object, object> GetValueFunction { get; set; }

        /// <summary>
        /// propertyInfo.SetValue(item, value);
        /// </summary>
        Action<object, object> SetValueAction { get; set; }

        bool IsNullable { get; set; }

        dynamic GetValue(dynamic item);
        void SetPropertyInfoValueFunction(dynamic item, dynamic value);
    }
}
