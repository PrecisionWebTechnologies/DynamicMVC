using System;
using System.Reflection;
using ReflectionLibrary.Interfaces;

namespace ReflectionLibrary.Models
{
    public class ReflectedMethodOperations : IReflectedMethodOperations
    {
        public MethodInfo MethodInfo { get; set; }
        public Func<object, object[], object> InvokeFunction { get; set; }
        public object Invoke(object obj, object[] paramaters)
        {
            return InvokeFunction(obj, paramaters);
        }
    }
}
