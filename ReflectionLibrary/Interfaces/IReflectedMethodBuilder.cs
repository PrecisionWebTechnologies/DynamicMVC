using System;
using System.Collections.Generic;

namespace ReflectionLibrary.Interfaces
{
    public interface IReflectedMethodBuilder
    {
        void BuildReflectedMethods(IReflectedClass reflectedClass, Type type);
        ICollection<IReflectedMethod> BuildReflectedMethods(Type type);
    }
}
