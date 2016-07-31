using System.Collections.Generic;
using ReflectionLibrary.Interfaces;

namespace DynamicMVC.DynamicEntityMetadataLibrary.Models
{
    public class DynamicComplexPropertyMetadata : DynamicPropertyMetadata
    {
        public DynamicComplexPropertyMetadata(IReflectedProperty reflectedProperty, IEnumerable<IReflectedClass> reflectedClasses)
            :base(reflectedProperty, reflectedClasses)
        {
            
        }

        public DynamicForiegnKeyPropertyMetadata DynamicForiegnKeyPropertyMetadata { get; set; }
    }
}
