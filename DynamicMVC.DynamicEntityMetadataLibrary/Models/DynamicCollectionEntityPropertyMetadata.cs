using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ReflectionLibrary.Extensions;
using ReflectionLibrary.Interfaces;

namespace DynamicMVC.DynamicEntityMetadataLibrary.Models
{
    public class DynamicCollectionEntityPropertyMetadata : DynamicPropertyMetadata
    {
        public DynamicCollectionEntityPropertyMetadata(IReflectedProperty reflectedProperty, IEnumerable<IReflectedClass> reflectedClasses)
            : base(reflectedProperty, reflectedClasses)
        {
            var inversePropertyAttribute = reflectedProperty.GetAttribute<InversePropertyAttribute>();
            if (inversePropertyAttribute != null)
                InverseProperty = inversePropertyAttribute.Property;
        }

        public string ForiegnKeyPropertyName { get; set; }
        public string InverseProperty { get; set; }
    }
}