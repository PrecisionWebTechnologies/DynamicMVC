using System;
using ReflectionLibrary.Interfaces;

namespace ReflectionLibrary.Models
{
    /// <summary>
    /// Provides access to methods that require the underlying reflected class to be defined.
    /// </summary>
    public class ReflectedClassOperations : IReflectedClassOperations
    {
        /// <summary>
        /// Returns origonal type reflected.
        /// </summary>
        public Func<Type> GetReflectedType { get; set; }
        /// <summary>
        /// Creates a new object
        /// </summary>
        public Func<object> CreateNewObject { get; set; }

    }
}
