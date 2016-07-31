using System;
using System.Collections.Generic;
using ReflectionLibrary.Enums;
using ReflectionLibrary.Interfaces;
#pragma warning disable 1591

namespace ReflectionLibrary.Models
{
    public class ReflectedProperty : IReflectedProperty
    {
        public ReflectedProperty()
        {
            Attributes = new HashSet<Attribute>();
        }
        /// <summary>
        /// Property Name
        /// </summary>
        public string Name { get; set; }

        public string PropertyTypeName { get; set; }

        public bool IsSimple { get; set; }
        public SimpleTypeEnum SimpleTypeEnum { get; set; }
        public ISimpleTypeParser SimpleTypeParser { get; set; }

        public bool IsComplex { get; set; }
        public bool IsCollection { get; set; }
        public string CollectionItemTypeName { get; set; }
        public bool IsNullable { get; set; }

        public IReflectedPropertyOperations ReflectedPropertyOperations { get; set; }
        public IReflectedClass ReflectedClass { get; set; }
        public ICollection<Attribute> Attributes { get; set; }

        public override string ToString()
        {
            return String.Format("{0}({1}) - {2} Attributes", Name, PropertyTypeName, Attributes.Count);
        }
    }
}
