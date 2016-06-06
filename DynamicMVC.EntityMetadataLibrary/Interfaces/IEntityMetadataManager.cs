using System.Collections.Generic;
using DynamicMVC.EntityMetadataLibrary.Models;

namespace DynamicMVC.EntityMetadataLibrary.Interfaces
{
    public interface IEntityMetadataManager
    {
        IEnumerable<EntityMetadata> GetEntityMetadatas();
    }
}