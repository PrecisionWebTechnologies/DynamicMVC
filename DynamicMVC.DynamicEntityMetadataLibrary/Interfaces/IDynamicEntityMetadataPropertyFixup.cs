using System.Collections.Generic;
using DynamicMVC.DynamicEntityMetadataLibrary.Models;

namespace DynamicMVC.DynamicEntityMetadataLibrary.Interfaces
{
    public interface IDynamicEntityMetadataPropertyFixup
    {
        void Fixup(IEnumerable<DynamicEntityMetadata> dynamicEntityMetadatas);
        int Order();
    }
}
