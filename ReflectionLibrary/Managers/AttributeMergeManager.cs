using System;
using System.Collections.Generic;
using System.Linq;
using ReflectionLibrary.Interfaces;

namespace ReflectionLibrary.Managers
{
    public class AttributeMergeManager : IAttributeMergeManager
    {
        public void MergeAttributes(IReflectedClass source, IReflectedClass target)
        {
            Merge(source.Attributes, target.Attributes);
            foreach (var reflectedMethod in source.ReflectedMethods)
            {
                var targetReflectedMethod = target.ReflectedMethods.SingleOrDefault(x => x.Name == reflectedMethod.Name);
                if (targetReflectedMethod != null)
                    Merge(reflectedMethod.Attributes, targetReflectedMethod.Attributes);
            }
            foreach (var reflectedProperty in target.ReflectedProperties)
            {
                var targetReflectedProperty = target.ReflectedProperties.SingleOrDefault(x => x.Name == reflectedProperty.Name);
                if (targetReflectedProperty != null)
                    Merge(reflectedProperty.Attributes, targetReflectedProperty.Attributes);
            }
        }

        private void Merge(ICollection<Attribute> source, ICollection<Attribute> destination)
        {
            foreach (var attribute in source)
            {
                if (destination.All(x => x.GetType() != attribute.GetType()))
                {
                    destination.Add(attribute);
                }
            }
        }
    }
}
