using System;
using System.Collections.Generic;
using ReflectionLibrary.Interfaces;

namespace ReflectionLibrary.Models
{
    public class ReflectedClass : IReflectedClass
    {
        private readonly IAttributeMergeManager _attributeMergeManager;

        public ReflectedClass(IAttributeMergeManager attributeMergeManager)
        {
            Attributes = new HashSet<Attribute>();
            ReflectedProperties = new HashSet<IReflectedProperty>();
            ReflectedMethods = new HashSet<IReflectedMethod>();
            _attributeMergeManager = attributeMergeManager;
        }

        public string Name { get; set; }

        public ICollection<Attribute> Attributes { get; set; }
        public ICollection<IReflectedProperty> ReflectedProperties { get; set; }
        public ICollection<IReflectedMethod> ReflectedMethods { get; set; }

        public override string ToString()
        {
            return String.Format("{0} - {1} Attributes, {2} Methods, {3} Properties", Name, Attributes.Count, ReflectedMethods.Count, ReflectedProperties.Count);
        }

        public void MergeAttributes(IReflectedClass reflectedClass)
        {
            _attributeMergeManager.MergeAttributes(reflectedClass,this);
        }
    }
}
