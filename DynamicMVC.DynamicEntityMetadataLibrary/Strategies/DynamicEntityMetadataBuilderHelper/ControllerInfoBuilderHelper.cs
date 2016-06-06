using DynamicMVC.DynamicEntityMetadataLibrary.Interfaces;
using DynamicMVC.DynamicEntityMetadataLibrary.Models;
using DynamicMVC.EntityMetadataLibrary.Models;

namespace DynamicMVC.DynamicEntityMetadataLibrary.Strategies.DynamicEntityMetadataBuilderHelper
{
    public class ControllerInfoBuilderHelper : IDynamicEntityMetadataBuilderHelper
    {
        public void Build(DynamicEntityMetadata dynamicEntityMetadata, EntityMetadata entityMetadata)
        {
            dynamicEntityMetadata.ControllerExists = entityMetadata.ControllerExists;
        }
    }
}
