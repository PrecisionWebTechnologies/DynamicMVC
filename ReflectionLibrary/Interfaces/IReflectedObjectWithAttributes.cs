using System;
using System.Collections.Generic;

namespace ReflectionLibrary.Interfaces
{
    /// <summary>
    /// Reflected Object with Attributes
    /// </summary>
    public interface IReflectedObjectWithAttributes : IReflectedObject
    {
        /// <summary>
        /// Attributes attached to the reflected object
        /// </summary>
        ICollection<Attribute> Attributes { get; set; }
    }
}
