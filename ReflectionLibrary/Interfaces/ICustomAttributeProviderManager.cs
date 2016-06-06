using System;
using System.Collections.Generic;
using System.Reflection;

namespace ReflectionLibrary.Interfaces
{
    public interface ICustomAttributeProviderManager
    {
        ICollection<Attribute> GetAttributes(ICustomAttributeProvider customAttributeProvider);
        bool HasAttribute<T>(ICustomAttributeProvider customAttributeProvider);
    }
}
