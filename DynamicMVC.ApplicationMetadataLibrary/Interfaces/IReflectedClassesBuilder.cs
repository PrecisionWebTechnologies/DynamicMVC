using System;
using System.Collections.Generic;
using ReflectionLibrary.Interfaces;

namespace DynamicMVC.ApplicationMetadataLibrary.Interfaces
{
    public interface IReflectedClassesBuilder
    {
        ICollection<IReflectedClass> BuildApplicationEntityMetadataReflectedClasses();
        ICollection<IReflectedClass> BuildApplicationEntityReflectedClasses(IEnumerable<Type> types);
    }
}
