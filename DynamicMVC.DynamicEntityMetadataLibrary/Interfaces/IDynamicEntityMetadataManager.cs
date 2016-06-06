using System.Collections.Generic;

namespace DynamicMVC.DynamicEntityMetadataLibrary.Interfaces
{
    public interface IDynamicEntityMetadataManager
    {
        IEnumerable<Models.DynamicEntityMetadata> GetDynamicEntityMetadatas();
        IEnumerable<Models.DynamicEntityMetadata> GetDynamicEntityMetadatas(IEnumerable<EntityMetadataLibrary.Models.EntityMetadata> entityMetadatas);
    }
}