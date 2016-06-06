using DynamicMVC.EntityMetadataLibrary.Models;

namespace DynamicMVC.DynamicEntityMetadataLibrary.Models
{
    public class DynamicComplexPropertyMetadata : DynamicPropertyMetadata
    {
        public DynamicComplexPropertyMetadata(EntityPropertyMetadata entityPropertyMetadata)
            :base(entityPropertyMetadata)
        {
            
        }

        public DynamicForiegnKeyPropertyMetadata DynamicForiegnKeyPropertyMetadata { get; set; }
    }
}
