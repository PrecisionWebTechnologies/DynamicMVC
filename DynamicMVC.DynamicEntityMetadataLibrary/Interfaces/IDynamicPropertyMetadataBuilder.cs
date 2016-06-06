using System.Collections.Generic;
using DynamicMVC.DynamicEntityMetadataLibrary.Models;
using DynamicMVC.EntityMetadataLibrary.Models;

namespace DynamicMVC.DynamicEntityMetadataLibrary.Interfaces
{
    public interface IDynamicPropertyMetadataBuilder
    {
        IEnumerable<DynamicPropertyMetadata> Build(EntityMetadata entityMetadata);
    }
}