using DynamicMVC.DynamicEntityMetadataLibrary.Models;

namespace DynamicMVC.DynamicEntityMetadataLibrary.Interfaces
{
    public interface IDynamicEntityMetadataValidator
    {
        string Validate(DynamicEntityMetadata dynamicEntityMetadata);
    }
}
