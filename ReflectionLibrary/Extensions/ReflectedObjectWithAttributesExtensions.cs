using System;
using System.Linq;
using ReflectionLibrary.Interfaces;

namespace ReflectionLibrary.Extensions
{
    /// <summary>
    /// Extension methods used on a Reflected Class that implements IEntityWithAttributes.
    /// </summary>
    public static class ReflectedObjectWithAttributesExtensions
    {
        /// <summary>
        /// Check if Reflected Object has attribute.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entityWithAttributes"></param>
        /// <returns></returns>
        public static bool HasAttribute<T>(this IReflectedObjectWithAttributes entityWithAttributes) where T : Attribute
        {
            return entityWithAttributes.Attributes.Any(x => x.GetType() == typeof(T));
        }

        /// <summary>
        /// Returns the attribute for the Reflected Object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entityWithAttributes"></param>
        /// <returns></returns>
        public static T GetAttribute<T>(this IReflectedObjectWithAttributes entityWithAttributes) where T : Attribute
        {
            return (T)entityWithAttributes.Attributes.SingleOrDefault(x => x.GetType() == typeof(T));
        }
    }
}
