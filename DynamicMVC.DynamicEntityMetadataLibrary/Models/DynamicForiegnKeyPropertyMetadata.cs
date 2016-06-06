using DynamicMVC.EntityMetadataLibrary.Models;

namespace DynamicMVC.DynamicEntityMetadataLibrary.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class DynamicForiegnKeyPropertyMetadata : DynamicPropertyMetadata
    {
        public DynamicForiegnKeyPropertyMetadata(EntityPropertyMetadata entityPropertyMetadata)
            : base(entityPropertyMetadata)
        {
            
        }

        public DynamicPropertyMetadata ComplexEntityPropertyMetadata { get; set; }
        public DynamicEntityMetadata ComplexDynamicEntityMetadata { get; set; }
    }
}
