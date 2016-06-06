using DynamicMVC.DynamicEntityMetadataLibrary.Models;
using DynamicMVC.EntityMetadataLibrary.Models;

namespace DynamicMVC.DynamicEntityMetadataLibrary.Interfaces
{
    public interface IDynamicEntityMetadataBuilderHelper
    {
        void Build(DynamicEntityMetadata dynamicEntityMetadata, EntityMetadata entityMetadata);
    }
}
