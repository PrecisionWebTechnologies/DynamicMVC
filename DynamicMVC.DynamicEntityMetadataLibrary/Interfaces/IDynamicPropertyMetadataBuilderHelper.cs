using DynamicMVC.DynamicEntityMetadataLibrary.Models;
using DynamicMVC.EntityMetadataLibrary.Models;

namespace DynamicMVC.DynamicEntityMetadataLibrary.Interfaces
{
    public interface IDynamicPropertyMetadataBuilderHelper
    {
        void Build(EntityMetadata entityMetadata, EntityPropertyMetadata entityPropertyMetadata, DynamicPropertyMetadata dynamicPropertyMetadata);
    }
}
