using System;
using ReflectionLibrary.Interfaces;
using Microsoft.Practices.ObjectBuilder2;

namespace ReflectionLibrary.Builders
{
    public class ReflectedClassBuilder : IReflectedClassBuilder
    {
        private readonly IReflectedMethodBuilder _reflectedMethodBuilder;
        private readonly Func<IReflectedClass> _reflectedClassFunction;
        private readonly IReflectedPropertyBuilder _reflectedPropertyBuilder;
        private readonly ICustomAttributeProviderManager _customAttributeProviderManager;

        public ReflectedClassBuilder(IReflectedMethodBuilder reflectedMethodBuilder, Func<IReflectedClass> reflectedClassFunction, IReflectedPropertyBuilder reflectedPropertyBuilder, ICustomAttributeProviderManager customAttributeProviderManager)
        {
            _reflectedMethodBuilder = reflectedMethodBuilder;
            _reflectedClassFunction = reflectedClassFunction;
            _reflectedPropertyBuilder = reflectedPropertyBuilder;
            _customAttributeProviderManager = customAttributeProviderManager;
        }

        public IReflectedClass BuildReflectedClass(Type type)
        {
            var reflectedClass = _reflectedClassFunction();

            reflectedClass.Name = type.Name;
            _customAttributeProviderManager.GetAttributes(type).ForEach(a => reflectedClass.Attributes.Add(a));

            _reflectedMethodBuilder.BuildReflectedMethods(reflectedClass, type);
            _reflectedPropertyBuilder.BuildReflectedProperties(reflectedClass, type);

            return reflectedClass;
        }
    }
}
