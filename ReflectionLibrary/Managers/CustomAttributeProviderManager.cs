using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ReflectionLibrary.Interfaces;

namespace ReflectionLibrary.Managers
{
    public class CustomAttributeProviderManager : ICustomAttributeProviderManager
    {
        public ICollection<Attribute> GetAttributes(ICustomAttributeProvider customAttributeProvider)
        {
            return customAttributeProvider.GetCustomAttributes(true).ToList().Select(x => (Attribute)x).ToList();
        }

        public bool HasAttribute<T>(ICustomAttributeProvider customAttributeProvider)
        {
            return GetAttributes(customAttributeProvider).Any(x => x.GetType() == typeof (T));
        }
    }
}
