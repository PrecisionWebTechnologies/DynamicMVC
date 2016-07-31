using System;
using System.Linq;
using ReflectionLibrary.Interfaces;

namespace ReflectionLibrary.Managers
{
    /// <summary>
    /// Determines information about a property type
    /// </summary>
    public class PropertyTypeManager : IPropertyTypeManager
    {
        private readonly ISimpleTypeParser[] _simpleTypeParsers;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="simpleTypeParsers"></param>
        public PropertyTypeManager(ISimpleTypeParser[] simpleTypeParsers)
        {
            _simpleTypeParsers = simpleTypeParsers;
        }

        /// <summary>
        /// Determines if the type is simple.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool IsSimple(Type type)
        {
            return _simpleTypeParsers.Any(x => x.GetSimpleType() == type);
        }

        /// <summary>
        /// Determines how to parse a simple type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public ISimpleTypeParser GetSimpleTypeParser(Type type)
        {
            return _simpleTypeParsers.SingleOrDefault(x => x.GetSimpleType() == type);
        }
        
        /// <summary>
        /// Does the property implement a generic IEnumerable
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool IsCollection(Type type)
        {
            return type.GetInterface("IEnumerable`1") != null && type.GetGenericArguments().Any();
        }

        /// <summary>
        /// The type of class the collection contains.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public Type CollectionItemType(Type type)
        {
            if (type.GetGenericArguments().Any())
            {
                return type.GetGenericArguments()[0];
            }
            throw new NullReferenceException(type.Name + " could not find a collection type");
        }

        /// <summary>
        /// Determies if a Simple Type is nullable
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool IsNullableType(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }
    }
}
