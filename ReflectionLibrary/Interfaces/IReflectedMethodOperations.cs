using System;
using System.Reflection;

namespace ReflectionLibrary.Interfaces
{
    public interface IReflectedMethodOperations
    {
        Func<object, object[], object> InvokeFunction { get; set; }
        MethodInfo MethodInfo { get; set; }
        object Invoke(object obj, object[] paramaters);
    }
}
