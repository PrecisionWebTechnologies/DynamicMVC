using System;
using System.Collections.Generic;
using System.Linq;
using ReflectionLibrary.Interfaces;
using System.Reflection;
using Microsoft.Practices.ObjectBuilder2;
#pragma warning disable 1591

namespace ReflectionLibrary.Builders
{
    public class ReflectedMethodBuilder : IReflectedMethodBuilder
    {
        private readonly Func<IReflectedMethod> _reflectedMethodFunction;
        private readonly ICustomAttributeProviderManager _customAttributeProviderManager;

        public ReflectedMethodBuilder(Func<IReflectedMethod> reflectedMethodFunction, ICustomAttributeProviderManager customAttributeProviderManager)
        {
            _reflectedMethodFunction = reflectedMethodFunction;
            _customAttributeProviderManager = customAttributeProviderManager;
        }

        public void BuildReflectedMethods(IReflectedClass reflectedClass, Type type)
        {
            foreach (var methodInfo in type
    .GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
    .Where(m => !m.IsSpecialName))
            {
                var reflectedMethod = _reflectedMethodFunction();

                reflectedMethod.Name = methodInfo.Name;
                reflectedMethod.InvokeFunction = methodInfo.Invoke;

                methodInfo.GetCustomAttributes().ToList().ForEach(a => reflectedMethod.Attributes.Add(a));
                reflectedClass.ReflectedMethods.Add(reflectedMethod);
                reflectedMethod.ReflectedClass = reflectedClass;
            }
        }

        public ICollection<IReflectedMethod> BuildReflectedMethods(Type type)
        {
            var reflectedMethods = new List<IReflectedMethod>();

            foreach (var methodInfo in type.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
    .Where(m => !m.IsSpecialName))
            {
                var reflectedMethod = _reflectedMethodFunction();

                reflectedMethod.Name = methodInfo.Name;
                reflectedMethod.InvokeFunction = methodInfo.Invoke;

                _customAttributeProviderManager.GetAttributes(methodInfo).ForEach(a => reflectedMethod.Attributes.Add(a));

                reflectedMethods.Add(reflectedMethod);
            }

            return reflectedMethods;
        }

    }
}
