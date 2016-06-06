using System.Collections.Generic;

namespace DynamicMVC.DynamicEntityMetadataLibrary.Interfaces
{
    public interface IDynamicEntityMetadataBuilder
    {
        IEnumerable<Models.DynamicEntityMetadata> Build(IEnumerable<EntityMetadataLibrary.Models.EntityMetadata> entityMetadatas);
    }
}