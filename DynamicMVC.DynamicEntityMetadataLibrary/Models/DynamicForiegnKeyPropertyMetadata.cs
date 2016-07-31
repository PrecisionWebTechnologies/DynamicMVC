using System.Collections.Generic;
using ReflectionLibrary.Interfaces;

namespace DynamicMVC.DynamicEntityMetadataLibrary.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class DynamicForiegnKeyPropertyMetadata : DynamicPropertyMetadata
    {
        public DynamicForiegnKeyPropertyMetadata(IReflectedProperty reflectedProperty, IEnumerable<IReflectedClass> reflectedClasses)
            : base(reflectedProperty, reflectedClasses)
        {
            
        }

        public DynamicPropertyMetadata ComplexEntityPropertyMetadata { get; set; }
        public DynamicEntityMetadata ComplexDynamicEntityMetadata { get; set; }
    }
}
