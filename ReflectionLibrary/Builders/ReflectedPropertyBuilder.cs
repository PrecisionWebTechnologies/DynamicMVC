using System;
using ReflectionLibrary.Interfaces;
using Microsoft.Practices.ObjectBuilder2;

namespace ReflectionLibrary.Builders
{
    /// <summary>
    /// Class used to define how to construct a ReflectedProperty Class
    /// </summary>
    public class ReflectedPropertyBuilder : IReflectedPropertyBuilder
    {
        private readonly Func<IReflectedProperty> _reflectedPropertyResolver;
        private readonly Func<IReflectedPropertyOperations> _reflectedPropertyOperationsResolver;
        private readonly ICustomAttributeProviderManager _customAttributeProviderManager;
        private readonly IPropertyTypeManager _propertyTypeManager;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="reflectedPropertyResolver"></param>
        /// <param name="reflectedPropertyOperationsResolver"></param>
        /// <param name="customAttributeProviderManager"></param>
        /// <param name="propertyTypeManager"></param>
        public ReflectedPropertyBuilder(Func<IReflectedProperty> reflectedPropertyResolver, Func<IReflectedPropertyOperations> reflectedPropertyOperationsResolver, ICustomAttributeProviderManager customAttributeProviderManager, IPropertyTypeManager propertyTypeManager)
        {
            _reflectedPropertyResolver = reflectedPropertyResolver;
            _customAttributeProviderManager = customAttributeProviderManager;
            _reflectedPropertyOperationsResolver = reflectedPropertyOperationsResolver;
            _propertyTypeManager = propertyTypeManager;
        }

        /// <summary>
        /// Builds a collection of ReflectedProperty classes and adds them to the ReflectedClass.
        /// </summary>
        /// <param name="reflectedClass"></param>
        /// <param name="type"></param>
        public void BuildReflectedProperties(IReflectedClass reflectedClass, Type type)
        {
            foreach (var propertyInfo in type.GetProperties())
            {
                var reflectedProperty = _reflectedPropertyResolver();

                reflectedProperty.Name = propertyInfo.Name;
                _customAttributeProviderManager.GetAttributes(propertyInfo).ForEach(a => reflectedProperty.Attributes.Add(a));
                reflectedProperty.PropertyTypeName = propertyInfo.PropertyType.Name;
                reflectedProperty.IsSimple = _propertyTypeManager.IsSimple(propertyInfo.PropertyType);
                if (reflectedProperty.IsSimple)
                {
                    reflectedProperty.SimpleTypeParser = _propertyTypeManager.GetSimpleTypeParser(propertyInfo.PropertyType);
                    reflectedProperty.SimpleTypeEnum = reflectedProperty.SimpleTypeParser.SimpleTypeEnum();
                    reflectedProperty.IsNullable = _propertyTypeManager.IsNullableType(propertyInfo.PropertyType);
                }
                else if (_propertyTypeManager.IsCollection(propertyInfo.PropertyType))
                {
                    reflectedProperty.IsCollection = true;
                    reflectedProperty.CollectionItemTypeName =
                        _propertyTypeManager.CollectionItemType(propertyInfo.PropertyType).Name;
                    reflectedProperty.IsNullable = true;
                }
                else
                {
                    reflectedProperty.IsComplex = true;
                    reflectedProperty.IsNullable = true;
                }
        
                reflectedProperty.ReflectedPropertyOperations = _reflectedPropertyOperationsResolver();
                reflectedProperty.ReflectedPropertyOperations.PropertyInfo = propertyInfo;
                reflectedProperty.ReflectedPropertyOperations.GetValueFunction = propertyInfo.GetValue;
                reflectedProperty.ReflectedPropertyOperations.SetValueAction = propertyInfo.SetValue;

                reflectedClass.ReflectedProperties.Add(reflectedProperty);
                reflectedProperty.ReflectedClass = reflectedClass;
            }
        }
    }
}
