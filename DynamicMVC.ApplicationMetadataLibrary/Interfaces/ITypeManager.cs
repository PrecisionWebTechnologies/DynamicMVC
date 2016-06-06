using System;
using DynamicMVC.Shared.Interfaces;

namespace DynamicMVC.ApplicationMetadataLibrary.Interfaces
{
    public interface ITypeManager
    {
        bool IsSimple(Type type);
        bool IsCollection(Type type);
        Type CollectionType(Type type);
        bool IsNullableType(Type type);
        ISimpleTypeParser GetSimpleTypeParser(Type type);
    }
}