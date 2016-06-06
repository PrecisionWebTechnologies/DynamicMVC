using System;
using System.Collections.Generic;

namespace ReflectionLibrary.Interfaces
{
    /// <summary>
    /// Shows all information for a given type
    /// </summary>
    public interface IReflectedClass : IEntityWithAttributes
    {
        /// <summary>
        /// TypeName
        /// </summary>
        string Name { get; set; }
        ICollection<Attribute> Attributes { get; set; }
        ICollection<IReflectedProperty> ReflectedProperties { get; set; }
        ICollection<IReflectedMethod> ReflectedMethods { get; set; }
        void MergeAttributes(IReflectedClass reflectedClass);
    }
}
