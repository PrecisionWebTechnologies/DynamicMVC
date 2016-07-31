using System;
using System.Collections.Generic;
using ReflectionLibrary.Enums;

namespace ReflectionLibrary.Interfaces
{
    public interface IReflectedProperty : IReflectedObjectWithAttributes
    {
        IReflectedPropertyOperations ReflectedPropertyOperations { get; set; }
        string Name { get; set; }
        IReflectedClass ReflectedClass { get; set; }
        ICollection<Attribute> Attributes { get; set; }
        string PropertyTypeName { get; set; }

        bool IsSimple { get; set; }
        SimpleTypeEnum SimpleTypeEnum { get; set; }
        bool IsComplex { get; set; }
        bool IsCollection { get; set; }
        string CollectionItemTypeName { get; set; }
        bool IsNullable { get; set; }
        ISimpleTypeParser SimpleTypeParser { get; set; }
    }
}
