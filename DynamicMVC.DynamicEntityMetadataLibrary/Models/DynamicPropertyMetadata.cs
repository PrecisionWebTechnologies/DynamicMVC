using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using DynamicMVC.Annotations;
using DynamicMVC.EntityMetadataLibrary.Models;
using DynamicMVC.Shared.Enums;
using DynamicMVC.Shared.Interfaces;

namespace DynamicMVC.DynamicEntityMetadataLibrary.Models
{
    /// <summary>
    /// Holds Metadata for a DynamicEntity property after it has been parsed by dynamic mvc
    /// </summary>
    public class DynamicPropertyMetadata : IPropertyWithPropertyName
    {
        public DynamicPropertyMetadata(EntityPropertyMetadata entityPropertyMetadata)
        {
            GetValueFunction = entityPropertyMetadata.GetValueFunction;
            PropertyName = entityPropertyMetadata.PropertyName;
            IsSimple = entityPropertyMetadata.IsSimple;
            IsComplexEntity = entityPropertyMetadata.IsComplexEntity;
            IsCollection = entityPropertyMetadata.IsCollection;
            IsNullableType = entityPropertyMetadata.IsNullableType;

            var uiHint = (UIHintAttribute)entityPropertyMetadata.EntityPropertyAttributes.SingleOrDefault(x => x is UIHintAttribute);
            if (uiHint != null)
                UIHint = uiHint.UIHint;

            var dataTypeAttribute = (DataTypeAttribute)entityPropertyMetadata.EntityPropertyAttributes.SingleOrDefault(x => x is DataTypeAttribute);
            if (dataTypeAttribute != null)
                DataType = dataTypeAttribute.DataType;

            TypeName = entityPropertyMetadata.TypeName;

            AllowSort = !entityPropertyMetadata.IsCollection && entityPropertyMetadata.EntityPropertyAttributes.All(x => !(x is DynamicSortNoneAttribute));
            SetValueAction = entityPropertyMetadata.SetValueAction;


            var filterUIHint = (FilterUIHintAttribute)entityPropertyMetadata.EntityPropertyAttributes.SingleOrDefault(x => x is FilterUIHintAttribute);
            ListFilterIndexHide = filterUIHint != null && filterUIHint.FilterUIHint == "None";

            HasDynamicFilterUIAttribute = entityPropertyMetadata.EntityPropertyAttributes.Any(x => x is DynamicFilterUIHintAttribute) && !ListFilterIndexHide;
            DynamicFilterUIHintAttribute = (DynamicFilterUIHintAttribute)entityPropertyMetadata.EntityPropertyAttributes.FirstOrDefault(x => x is DynamicFilterUIHintAttribute);
            SimpleTypeEnum = entityPropertyMetadata.SimpleTypeEnum;
        }

        public bool Scaffold { get; set; }

        public override string ToString()
        {
            return PropertyName + " - " + TypeName;
        }

        public bool ListFilterIndexHide { get; set; }

        public DynamicFilterUIHintAttribute GetDynamicFilterUIHintAttribute()
        {
            if (DynamicFilterUIHintAttribute == null)
                throw new NotSupportedException("Cannot retreive DynamicFilterUIAttribute for " + PropertyName + ".  You can call HasDynamicFilter method to determine if this property has a DynamicFilterUIAttribute");
            return DynamicFilterUIHintAttribute;
        }

        private DynamicFilterUIHintAttribute DynamicFilterUIHintAttribute { get; set; }
        public bool HasDynamicFilterUIAttribute { get; set; }
        public string DisplayName { get; set; }
        public Func<object, object> GetValueFunction { get; set; }
        public string PropertyName { get; set; }
        public bool IsSimple { get; set; }
        public bool IsCollection { get; set; }
        public bool IsComplexEntity { get; set; }
        public bool IsNullableType { get; set; }
        public string UIHint { get; set; }
        /// <summary>
        /// DataType specified by DataTypeAttribute
        /// </summary>
        public DataType DataType { get; set; }
        public string TypeName { get; set; }
        public bool AllowSort { get; set; }
        public bool IsPrimaryKey { get; set; }
        public Func<string, dynamic> ParseValue { get; set; }
        public Action<object, object> SetValueAction { get; set; }

        public DynamicEntityMetadata DynamicEntityMetadata { get; set; }
        public SimpleTypeEnum SimpleTypeEnum { get; set; }

    }
}
