using System;
using ReflectionLibrary.Interfaces;
using Microsoft.Practices.ObjectBuilder2;

namespace ReflectionLibrary.Builders
{
    /// <summary>
    /// This class is responsible for constructing a Reflected Class.
    /// </summary>
    public class ReflectedClassBuilder : IReflectedClassBuilder
    {
        private readonly IReflectedMethodBuilder _reflectedMethodBuilder;
        private readonly Func<IReflectedClass> _reflectedClassResolver;
        private readonly IReflectedPropertyBuilder _reflectedPropertyBuilder;
        private readonly ICustomAttributeProviderManager _customAttributeProviderManager;
        private readonly Func<IReflectedClassOperations> _reflectedClassOperationsResolver;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="reflectedMethodBuilder">Object used to build a Reflected Method class</param>
        /// <param name="reflectedClassResolver">This is used to construct a reflected class using IOC container</param>
        /// <param name="reflectedClassOperationsResolver">This is used to construct a ReflectedClassOperations using an IOC container</param>
        /// <param name="reflectedPropertyBuilder">ReflectedPropertyBuilder is used to construct a ReflectedPropertyClass</param>
        /// <param name="customAttributeProviderManager">Custom Attribute Provider Manager defines how to query .net types for attributes</param>
        public ReflectedClassBuilder(IReflectedMethodBuilder reflectedMethodBuilder, Func<IReflectedClass> reflectedClassResolver, Func<IReflectedClassOperations> reflectedClassOperationsResolver, IReflectedPropertyBuilder reflectedPropertyBuilder, ICustomAttributeProviderManager customAttributeProviderManager)
        {
            _reflectedMethodBuilder = reflectedMethodBuilder;
            _reflectedClassResolver = reflectedClassResolver;
            _reflectedPropertyBuilder = reflectedPropertyBuilder;
            _customAttributeProviderManager = customAttributeProviderManager;
            _reflectedClassOperationsResolver = reflectedClassOperationsResolver;
        }

        /// <summary>
        /// Build a reflected class by providing a type
        /// </summary>
        /// <param name="type">Type to reflect</param>
        /// <returns>Reflected Class</returns>
        public IReflectedClass BuildReflectedClass(Type type)
        {
            var reflectedClass = _reflectedClassResolver();

            reflectedClass.Name = type.Name;
            reflectedClass.ReflectedClassOperations = _reflectedClassOperationsResolver();
            reflectedClass.ReflectedClassOperations.GetReflectedType = () => type;
            reflectedClass.ReflectedClassOperations.CreateNewObject = () => Activator.CreateInstance(type);
            _customAttributeProviderManager.GetAttributes(type).ForEach(a => reflectedClass.Attributes.Add(a));

            _reflectedMethodBuilder.BuildReflectedMethods(reflectedClass, type);
            _reflectedPropertyBuilder.BuildReflectedProperties(reflectedClass, type);

            return reflectedClass;
        }
    }
}
