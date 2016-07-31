using System;
using System.Collections.Generic;
using System.Linq;

namespace DynamicMVC.Shared.Extensions
{
    public static class AttributeExtensions
    {
        public static bool AttributeIsOfType<T>(this Attribute type)
        {
            return type is T;
        }

        public static IEnumerable<T> GetAttributes<T>(this IEnumerable<Attribute> attributes) where T : Attribute
        {
            return attributes.Where(x => x.AttributeIsOfType<T>()).Select(x => (T)x).ToList();
        }

        public static T GetAttribute<T>(this IEnumerable<Attribute> attributes) where T : Attribute
        {
            var filteredattributes = attributes.Where(x => x.AttributeIsOfType<T>()).ToList();
            if (!filteredattributes.Any())
                return null;
            return filteredattributes.Select(x => (T)x).First();
        }
    }
}
