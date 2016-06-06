using System.Collections.Generic;
using DynamicMVC.ApplicationMetadataLibrary.Models;
using DynamicMVC.EntityMetadataLibrary.Models;

namespace DynamicMVC.EntityMetadataLibrary.Interfaces
{
    public interface IEntityMetadataBuilder
    {
        IEnumerable<EntityMetadata> Build(ApplicationMetadataSummary applicationMetadataSummary);
    }
}