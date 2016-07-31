using DynamicMVC.DynamicEntityMetadataLibrary.Interfaces;
using DynamicMVC.DynamicEntityMetadataLibrary.Models;

namespace DynamicMVC.DynamicEntityMetadataLibrary.Strategies.DynamicEntityMetadataValidators
{
    public class CRUDPropertiesValidator : IDynamicEntityMetadataValidator
    {
        public string Validate(DynamicEntityMetadata dynamicEntityMetadata)
        {
            if (dynamicEntityMetadata.ScaffoldIndexProperties() == null)
                return "ScaffoldIndexProperties should not be null for DynamicEntity " + dynamicEntityMetadata.TypeName();

            return null;
        }
    }
}
