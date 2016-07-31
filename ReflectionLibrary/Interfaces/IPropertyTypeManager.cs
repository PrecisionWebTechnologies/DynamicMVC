using System;

namespace ReflectionLibrary.Interfaces
{
    /// <summary>
    /// Determines information about a property type
    /// </summary>
    public interface IPropertyTypeManager
    {
        /// <summary>
        /// The type of class the collection contains.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        Type CollectionItemType(Type type);

        /// <summary>
        /// Does the property implement a generic IEnumerable
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        bool IsCollection(Type type);

        /// <summary>
        /// Determines if the type is simple.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        bool IsSimple(Type type);

        /// <summary>
        /// Determines how to parse a simple type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        ISimpleTypeParser GetSimpleTypeParser(Type type);

        /// <summary>
        /// Determies if a Simple Type is nullable
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        bool IsNullableType(Type type);
    }
}