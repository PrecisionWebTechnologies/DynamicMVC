using System.Collections.Generic;

namespace ReflectionLibrary.Interfaces
{
    /// <summary>
    /// Shows all information for a given type
    /// </summary>
    public interface IReflectedClass : IReflectedObjectWithAttributes
    {
        /// <summary>
        /// Reflects each property of the underlying class
        /// </summary>
        ICollection<IReflectedProperty> ReflectedProperties { get; set; }

        /// <summary>
        /// Reflected values for each method on the underlying class
        /// </summary>
        ICollection<IReflectedMethod> ReflectedMethods { get; set; }

        /// <summary>
        /// Operations that cannot be easily mocked in testing.  These operations require the underlying to be defined.
        /// </summary>
        IReflectedClassOperations ReflectedClassOperations { get; set; }

        /// <summary>
        /// Merge attributes from another reflected class.
        /// </summary>
        /// <param name="reflectedClass"></param>
        void MergeAttributes(IReflectedClass reflectedClass);
    }
}
