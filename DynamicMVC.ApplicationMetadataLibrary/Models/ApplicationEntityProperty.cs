using System;
using System.Collections.Generic;
using System.Reflection;
using DynamicMVC.ApplicationMetadataLibrary.Extensions;
using DynamicMVC.ApplicationMetadataLibrary.Interfaces;
using DynamicMVC.Shared;
using DynamicMVC.Shared.Extensions;
using DynamicMVC.Shared.Interfaces;

namespace DynamicMVC.ApplicationMetadataLibrary.Models
{
    /// <summary>
    /// Holds information about a property of a class being reflected for a given ApplicationEntity
    /// </summary>
    public class ApplicationEntityProperty
    {
        private readonly ITypeManager _typeManager;

        public ApplicationEntityProperty()
        {
            _typeManager = Container.Resolve<ITypeManager>();
            Attributes = new HashSet<Attribute>();
            ApplicationEntityTypes = new HashSet<Type>();
        }
        public ApplicationEntityProperty(PropertyInfo propertyInfo, ICollection<Type> applicationEntityTypes)
            : this()
        {
            ApplicationEntityTypes = applicationEntityTypes;
            PropertyName = propertyInfo.Name;
            DynamicTypeName = GetDynamicTypeName(propertyInfo);
            GetValueFunction = propertyInfo.GetPropertyInfoValueFunction;
            SetValueAction = propertyInfo.SetPropertyInfoValueFunction;
            Attributes.AddRange(propertyInfo.GetCustomAttributes());
            IsSimple = _typeManager.IsSimple(propertyInfo.PropertyType);
            IsDynamicCollection = GetIsDynamicCollection(propertyInfo);
            IsComplexEntity = GetIsComplexEntity(propertyInfo);
            IsNullable = _typeManager.IsNullableType(propertyInfo.PropertyType);
            if (IsSimple)
            {
                SimpleTypeParser = _typeManager.GetSimpleTypeParser(propertyInfo.PropertyType);
                if (SimpleTypeParser == null)
                    throw new Exception("SimpleTypeParser could not be found for type " + propertyInfo.PropertyType.Name);
            }

        }

        private ICollection<Type> ApplicationEntityTypes { get; set; }

        public ICollection<Attribute> Attributes { get; set; }
        public bool IsSimple { get; set; }
        public bool IsNullable { get; set; }
        public Action<object, object> SetValueAction { get; set; }
        public Func<object, object> GetValueFunction { get; set; }
        public string DynamicTypeName { get; set; }
        public string PropertyName { get; set; }
        public bool IsDynamicCollection { get; set; }
        public bool IsComplexEntity { get; set; }

        public ISimpleTypeParser SimpleTypeParser { get; set; }

        public string GetDynamicTypeName(PropertyInfo propertyInfo)
        {
            if (GetIsDynamicCollection(propertyInfo))
            {
                return _typeManager.CollectionType(propertyInfo.PropertyType).Name;
            }
            return propertyInfo.PropertyType.Name;
        }

        public bool GetIsDynamicCollection(PropertyInfo propertyInfo)
        {
            return (_typeManager.IsCollection(propertyInfo.PropertyType) && ApplicationEntityTypes.Contains(_typeManager.CollectionType(propertyInfo.PropertyType)));
        }
        public bool GetIsComplexEntity(PropertyInfo propertyInfo)
        {
            return ApplicationEntityTypes.Contains(propertyInfo.PropertyType);
        }
    }
}
