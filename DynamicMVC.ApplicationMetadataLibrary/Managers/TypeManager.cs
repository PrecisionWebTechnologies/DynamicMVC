using System;
using System.Linq;
using DynamicMVC.ApplicationMetadataLibrary.Interfaces;
using DynamicMVC.Shared.Interfaces;

namespace DynamicMVC.ApplicationMetadataLibrary.Managers
{
    public class TypeManager : ITypeManager
    {
        private readonly ISimpleTypeParser[] _simpleTypeParsers;

        public TypeManager(ISimpleTypeParser[] simpleTypeParsers)
        {
            _simpleTypeParsers = simpleTypeParsers;
        }

        public bool IsSimple(Type type)
        {
            return _simpleTypeParsers.Any(x => x.GetSimpleType() == type);
        }

        public ISimpleTypeParser GetSimpleTypeParser(Type type)
        {
            return _simpleTypeParsers.SingleOrDefault(x => x.GetSimpleType() == type);
        }

        public bool IsCollection(Type type)
        {
            return type.GetInterface("IEnumerable`1") != null && type.GetGenericArguments().Any();
        }

        public Type CollectionType(Type type)
        {
            if (type.GetGenericArguments().Any())
            {
                return type.GetGenericArguments()[0];
            }
            throw new NullReferenceException(type.Name + " could not find a collection type");
        }

        public bool IsNullableType(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
            //if (propertyType.IsGenericType &&
            //  propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
            //{
            //    propertyType = propertyType.GetGenericArguments()[0];
            //}
        }
    }
}
