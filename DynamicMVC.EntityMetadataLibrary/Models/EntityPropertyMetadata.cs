using System;
using System.Collections.Generic;
using DynamicMVC.Shared.Enums;
using ReflectionLibrary.Enums;

namespace DynamicMVC.EntityMetadataLibrary.Models
{
    /// <summary>
    /// Holds Metadata for a DynamicEntity property as desired by the developer.  This can be configured to be different than the default behavior or from what exists in the client application
    /// </summary>
    public class EntityPropertyMetadata
    {
        public EntityPropertyMetadata()
        {
            EntityPropertyAttributes = new HashSet<Attribute>();
        }

        public EntityPropertyMetadata(string propertyName, string typeName, Func<object, dynamic> getValueFunction, Action<object, dynamic> setValueAction)
            : this()
        {
            PropertyName = propertyName;
            TypeName = typeName;
            GetValueFunction = getValueFunction;
            SetValueAction = setValueAction;
        }

        public EntityMetadata EntityMetadata { get; set; }
        public string TypeName { get; set; }
        public string PropertyName { get; set; }
        public ICollection<Attribute> EntityPropertyAttributes { get; set; }
        public bool IsSimple { get; set; }
        public bool IsCollection { get; set; }
        public bool IsComplexEntity { get; set; }
        public Func<object, object> GetValueFunction { get; set; }
        public Action<object, object> SetValueAction { get; set; }
        public bool IsNullableType { get; set; }
        public Func<string, dynamic> ParseValue { get; set; }
        public SimpleTypeEnum SimpleTypeEnum { get; set; }
    }
}
