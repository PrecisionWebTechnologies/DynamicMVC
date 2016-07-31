using System;
using System.Collections.Generic;
using ReflectionLibrary.Interfaces;

namespace ReflectionLibrary.Models
{
    /// <summary>
    /// ReflectedClass is responsible for providing access to any information that needs to be reflected from an underlying class
    /// </summary>
    public class ReflectedClass : IReflectedClass
    {
        private readonly IAttributeMergeManager _attributeMergeManager;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="attributeMergeManager">Defines logic used to Merge attributes</param>
        public ReflectedClass(IAttributeMergeManager attributeMergeManager)
        {
            Attributes = new HashSet<Attribute>();
            ReflectedProperties = new HashSet<IReflectedProperty>();
            ReflectedMethods = new HashSet<IReflectedMethod>();
            _attributeMergeManager = attributeMergeManager;
        }

        /// <summary>
        /// Name of the underlying class.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Attributes on the underlying class
        /// </summary>
        public ICollection<Attribute> Attributes { get; set; }
        /// <summary>
        /// Reflects each property of the underlying class
        /// </summary>
        public ICollection<IReflectedProperty> ReflectedProperties { get; set; }
        /// <summary>
        /// Reflected values for each method on the underlying class
        /// </summary>
        public ICollection<IReflectedMethod> ReflectedMethods { get; set; }
        /// <summary>
        /// Operations that cannot be easily mocked in testing.  These operations require the underlying to be defined.
        /// </summary>
        public IReflectedClassOperations ReflectedClassOperations { get; set; }
        /// <summary>
        /// Displays useful information for the debugger.
        /// </summary>
        /// <returns>String.Format("{0} - {1} Attributes, {2} Methods, {3} Properties", Name, Attributes.Count, ReflectedMethods.Count, ReflectedProperties.Count)</returns>
        public override string ToString()
        {
            return String.Format("{0} - {1} Attributes, {2} Methods, {3} Properties", Name, Attributes.Count, ReflectedMethods.Count, ReflectedProperties.Count);
        }

        /// <summary>
        /// Merge attributes from another reflected class.
        /// </summary>
        /// <param name="reflectedClass"></param>
        public void MergeAttributes(IReflectedClass reflectedClass)
        {
            _attributeMergeManager.MergeAttributes(reflectedClass,this);
        }
    }
}
