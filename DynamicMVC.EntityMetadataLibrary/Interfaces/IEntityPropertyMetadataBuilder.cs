using System.Collections.Generic;
using DynamicMVC.ApplicationMetadataLibrary.Models;
using DynamicMVC.EntityMetadataLibrary.Models;

namespace DynamicMVC.EntityMetadataLibrary.Interfaces
{
    public interface IEntityPropertyMetadataBuilder
    {
        IEnumerable<EntityPropertyMetadata> Build(ApplicationEntity applicationEntity);
    }
}