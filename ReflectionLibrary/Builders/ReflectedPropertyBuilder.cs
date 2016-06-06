using System;
using ReflectionLibrary.Interfaces;
using Microsoft.Practices.ObjectBuilder2;

#pragma warning disable 1591

namespace ReflectionLibrary.Builders
{
    public class ReflectedPropertyBuilder : IReflectedPropertyBuilder
    {
        private readonly Func<IReflectedProperty> _reflectedPropertyFunction;
        private readonly ICustomAttributeProviderManager _customAttributeProviderManager;

        public ReflectedPropertyBuilder(Func<IReflectedProperty> reflectedPropertyFunction, ICustomAttributeProviderManager customAttributeProviderManager)
        {
            _reflectedPropertyFunction = reflectedPropertyFunction;
            _customAttributeProviderManager = customAttributeProviderManager;
        }

        public void BuildReflectedProperties(IReflectedClass reflectedClass, Type type)
        {
            foreach (var propertyInfo in type.GetProperties())
            {
                var reflectedProperty = _reflectedPropertyFunction();

                reflectedProperty.PropertyName = propertyInfo.Name;
                _customAttributeProviderManager.GetAttributes(propertyInfo).ForEach(a => reflectedProperty.Attributes.Add(a));
                reflectedProperty.PropertyType = propertyInfo.PropertyType.Name;
                reflectedProperty.IsNullable = propertyInfo.PropertyType.IsGenericType && propertyInfo.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>);

                reflectedClass.ReflectedProperties.Add(reflectedProperty);
                reflectedProperty.ReflectedClass = reflectedClass;
            }
        }
    }
}
