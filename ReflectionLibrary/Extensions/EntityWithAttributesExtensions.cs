using System;
using System.Linq;
using ReflectionLibrary.Interfaces;

namespace ReflectionLibrary.Extensions
{
    public static class EntityWithAttributesExtensions
    {
        public static bool HasAttribute<T>(this IEntityWithAttributes entityWithAttributes) where T : Attribute
        {
            return entityWithAttributes.Attributes.Any(x => x.GetType() == typeof(T));
        }

        public static T GetAttribute<T>(this IEntityWithAttributes entityWithAttributes) where T : Attribute
        {
            return (T)entityWithAttributes.Attributes.SingleOrDefault(x => x.GetType() == typeof(T));
        }
    }
}
