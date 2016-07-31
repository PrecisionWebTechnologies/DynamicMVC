using System;

namespace ReflectionLibrary.Interfaces
{
    /// <summary>
    /// Provides access to methods that require the underlying reflected class to be defined.
    /// </summary>
    public interface IReflectedClassOperations
    {
        /// <summary>
        /// Returns origonal type reflected.
        /// </summary>
        Func<Type> GetReflectedType { get; set; }

        /// <summary>
        /// Creates a new object
        /// </summary>
        Func<object> CreateNewObject { get; set; }
    }
}
