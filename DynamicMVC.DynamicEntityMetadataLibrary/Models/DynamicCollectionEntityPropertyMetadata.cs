using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using DynamicMVC.EntityMetadataLibrary.Models;

namespace DynamicMVC.DynamicEntityMetadataLibrary.Models
{
    public class DynamicCollectionEntityPropertyMetadata : DynamicPropertyMetadata
    {
        public DynamicCollectionEntityPropertyMetadata(EntityPropertyMetadata entityPropertyMetadata)
            : base(entityPropertyMetadata)
        {
            var inversePropertyAttribute = entityPropertyMetadata.EntityPropertyAttributes.OfType<InversePropertyAttribute>().FirstOrDefault();
            if (inversePropertyAttribute != null)
                InverseProperty = inversePropertyAttribute.Property;
        }

        public string ForiegnKeyPropertyName { get; set; }
        public string InverseProperty { get; set; }
    }
}