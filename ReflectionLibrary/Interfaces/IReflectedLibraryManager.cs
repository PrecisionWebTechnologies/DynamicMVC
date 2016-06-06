using System;
using System.Collections.Generic;

namespace ReflectionLibrary.Interfaces
{
    public interface IReflectedLibraryManager
    {
        IReflectedClass GetReflectedClass(Type type);
        ICollection<IReflectedClass> GetReflectedClasses(IEnumerable<Type> types, params Func<Type, bool>[] filters);
    }
}
