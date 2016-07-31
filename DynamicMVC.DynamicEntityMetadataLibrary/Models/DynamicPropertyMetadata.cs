using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using DynamicMVC.Annotations;
using DynamicMVC.Shared.Extensions;
using DynamicMVC.Shared.Interfaces;
using ReflectionLibrary.Enums;
using ReflectionLibrary.Extensions;
using ReflectionLibrary.Interfaces;
#pragma warning disable 1591

namespace DynamicMVC.DynamicEntityMetadataLibrary.Models
{
    /// <summary>
    /// Holds Metadata for a DynamicEntity property after it has been parsed by dynamic mvc
    /// </summary>
    public class DynamicPropertyMetadata : IPropertyWithPropertyName
    {
        public DynamicPropertyMetadata(IReflectedProperty reflectedProperty, IEnumerable<IReflectedClass> reflectedClasses)
        {
            ReflectedProperty = reflectedProperty;
            ReflectedClasses = reflectedClasses;
        }

        public IReflectedProperty ReflectedProperty { get; set; }
        private IEnumerable<IReflectedClass> ReflectedClasses { get; set; }

        public bool Scaffold()
        {
            var scaffoldAttribute = ReflectedProperty.GetAttribute<ScaffoldColumnAttribute>();

            if (IsPrimaryKey())
            {
                return scaffoldAttribute != null && scaffoldAttribute.Scaffold;
            }
            else
            {
                if (scaffoldAttribute != null)
                    return scaffoldAttribute.Scaffold;
                else
                    return IsDynamicCollection() || IsSimple() || IsDynamicEntity();
            }
        }

        public override string ToString()
        {
            return ReflectedProperty.Name + " - " + TypeName();
        }

        public bool ListFilterIndexHide()
        {
            var filterUIHint = (FilterUIHintAttribute)ReflectedProperty.Attributes.SingleOrDefault(x => x is FilterUIHintAttribute);
            return filterUIHint != null && filterUIHint.FilterUIHint == "None";
        }

        public DynamicFilterUIHintAttribute GetDynamicFilterUIHintAttribute()
        {
            if (ReflectedProperty.GetAttribute<DynamicFilterUIHintAttribute>() == null)
                throw new NotSupportedException("Cannot retreive DynamicFilterUIAttribute for " + PropertyName() + ".  You can call HasDynamicFilter method to determine if this property has a DynamicFilterUIAttribute");
            return ReflectedProperty.GetAttribute<DynamicFilterUIHintAttribute>();
        }

        public string PropertyName()
        {
            return ReflectedProperty.Name;
        }

        public bool HasDynamicFilterUIAttribute()
        {
            return ReflectedProperty.HasAttribute<DynamicFilterUIHintAttribute>() && !ListFilterIndexHide();
        }

        public string DisplayName()
        {
            var displayNameAttribute = ReflectedProperty.GetAttribute<DisplayNameAttribute>();
            var displayAttribute = ReflectedProperty.GetAttribute<DisplayAttribute>();
            if (displayNameAttribute != null)
            {
                return displayNameAttribute.DisplayName;
            }
            else if (displayAttribute != null)
            {
                return displayAttribute.Name;
            }
            else
            {
                return ReflectedProperty.Name;
            }
        }

        public bool IsSimple()
        {
            return ReflectedProperty.IsSimple;
        }

        public bool IsDynamicCollection()
        {
            return ReflectedProperty.IsCollection && ReflectedClasses.Any(x => x.Name == ReflectedProperty.CollectionItemTypeName);
        }

        public bool IsDynamicEntity()
        {
            return ReflectedProperty.IsComplex && ReflectedClasses.Any(x => x.Name == ReflectedProperty.PropertyTypeName);
        }

        public bool IsNullableType()
        {
            return ReflectedProperty.IsNullable;
        }

        public string UIHint()
        {
            var uiHint = ReflectedProperty.Attributes.GetAttribute<UIHintAttribute>();
            if (uiHint != null)
              return uiHint.UIHint;
            return null;
        }

        /// <summary>
        /// DataType specified by DataTypeAttribute
        /// </summary>
        public DataType DataType()
        {
            var dataTypeAttribute = (DataTypeAttribute)ReflectedProperty.GetAttribute<DataTypeAttribute>();
            if (dataTypeAttribute != null)
                return dataTypeAttribute.DataType;
            return default(DataType);
        }

        public string TypeName()
        {
            return ReflectedProperty.PropertyTypeName;
        }

        public string CollectionItemTypeName()
        {
            return ReflectedProperty.CollectionItemTypeName;
        }

        public bool AllowSort()
        {
            return !ReflectedProperty.IsCollection && ReflectedProperty.Attributes.All(x => !(x is DynamicSortNoneAttribute));
        }

        public bool IsPrimaryKey()
        {
            var keyName = DynamicEntityMetadata.ReflectedClass.GetAttribute<DynamicEntityAttribute>().Key;
            return PropertyName() == keyName;
        }

        public Func<string, dynamic> ParseValue()
        {
            return ReflectedProperty.SimpleTypeParser.Parse;
        }

        public Action<object, object> SetValueAction()
        {
            return ReflectedProperty.ReflectedPropertyOperations.SetValueAction;
        }

        public Func<object, object> GetValueFunction()
        {
            return ReflectedProperty.ReflectedPropertyOperations.GetValueFunction;
        }

        public DynamicEntityMetadata DynamicEntityMetadata { get; set; }

        public SimpleTypeEnum SimpleTypeEnum()
        {
            return ReflectedProperty.SimpleTypeEnum;
        }

    }
}
