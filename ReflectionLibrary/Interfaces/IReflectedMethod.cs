using System;
using System.Collections.Generic;

namespace ReflectionLibrary.Interfaces
{
    /// <summary>
    /// Shows All Information for MethodInfo
    /// </summary>
    public interface IReflectedMethod : IReflectedObjectWithAttributes
    {
        /// <summary>
        /// Method Name
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// Parent Class
        /// </summary>
        IReflectedClass ReflectedClass { get; set; }
        /// <summary>
        /// Attributes that are decorating this method
        /// </summary>
        ICollection<Attribute> Attributes { get; set; }

        IReflectedMethodOperations ReflectedMethodOperations { get; set; }
    }
}
