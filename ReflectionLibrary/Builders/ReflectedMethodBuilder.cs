using System;
using System.Collections.Generic;
using System.Linq;
using ReflectionLibrary.Interfaces;
using System.Reflection;
using Microsoft.Practices.ObjectBuilder2;

namespace ReflectionLibrary.Builders
{
    /// <summary>
    /// Class used to define how to construct a ReflectedMethod class
    /// </summary>
    public class ReflectedMethodBuilder : IReflectedMethodBuilder
    {
        private readonly Func<IReflectedMethod> _reflectedMethodFunction;
        private readonly ICustomAttributeProviderManager _customAttributeProviderManager;
        private readonly Func<IReflectedMethodOperations> _reflectedMethodOperationsResolver;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="reflectedMethodFunction"></param>
        /// <param name="customAttributeProviderManager"></param>
        /// <param name="reflectedMethodOperationsResolver"></param>
        public ReflectedMethodBuilder(Func<IReflectedMethod> reflectedMethodFunction, ICustomAttributeProviderManager customAttributeProviderManager, Func<IReflectedMethodOperations> reflectedMethodOperationsResolver)
        {
            _reflectedMethodFunction = reflectedMethodFunction;
            _customAttributeProviderManager = customAttributeProviderManager;
            _reflectedMethodOperationsResolver = reflectedMethodOperationsResolver;
        }

        /// <summary>
        /// Builds a ReflectedMethod class
        /// </summary>
        /// <param name="reflectedClass"></param>
        /// <param name="type"></param>
        public void BuildReflectedMethods(IReflectedClass reflectedClass, Type type)
        {
            foreach (var methodInfo in type
    .GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
    .Where(m => !m.IsSpecialName))
            {
                var reflectedMethod = _reflectedMethodFunction();

                reflectedMethod.Name = methodInfo.Name;
                methodInfo.GetCustomAttributes().ToList().ForEach(a => reflectedMethod.Attributes.Add(a));
                reflectedClass.ReflectedMethods.Add(reflectedMethod);
                reflectedMethod.ReflectedClass = reflectedClass;

                reflectedMethod.ReflectedMethodOperations = _reflectedMethodOperationsResolver();
                reflectedMethod.ReflectedMethodOperations.InvokeFunction = methodInfo.Invoke;
                reflectedMethod.ReflectedMethodOperations.MethodInfo = methodInfo;
            }
        }

        /// <summary>
        /// Builds a Reflected Method Class
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public ICollection<IReflectedMethod> BuildReflectedMethods(Type type)
        {
            var reflectedMethods = new List<IReflectedMethod>();

            foreach (var methodInfo in type.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
    .Where(m => !m.IsSpecialName))
            {
                var reflectedMethod = _reflectedMethodFunction();

                reflectedMethod.Name = methodInfo.Name;
                reflectedMethod.ReflectedMethodOperations = _reflectedMethodOperationsResolver();
                reflectedMethod.ReflectedMethodOperations.InvokeFunction = methodInfo.Invoke;
                reflectedMethod.ReflectedMethodOperations.MethodInfo = methodInfo;

                _customAttributeProviderManager.GetAttributes(methodInfo).ForEach(a => reflectedMethod.Attributes.Add(a));

                reflectedMethods.Add(reflectedMethod);
            }

            return reflectedMethods;
        }

    }
}
