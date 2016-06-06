using System.ComponentModel.DataAnnotations;
using System.Linq;
using DynamicMVC.DynamicEntityMetadataLibrary.Interfaces;
using DynamicMVC.DynamicEntityMetadataLibrary.Models;
using DynamicMVC.EntityMetadataLibrary.Models;

namespace DynamicMVC.DynamicEntityMetadataLibrary.Strategies.DynamicPropertyMetadataBuilderHelper
{
    public class SetScaffoldProperty : IDynamicPropertyMetadataBuilderHelper
    {
        public void Build(EntityMetadata entityMetadata, EntityPropertyMetadata entityPropertyMetadata, DynamicPropertyMetadata dynamicPropertyMetadata)
        {
            var scaffoldAttribute = entityPropertyMetadata.EntityPropertyAttributes.OfType<ScaffoldColumnAttribute>().SingleOrDefault();

            if (dynamicPropertyMetadata.IsPrimaryKey)
            {
                dynamicPropertyMetadata.Scaffold = scaffoldAttribute != null && scaffoldAttribute.Scaffold;
            }
            else
            {
                if (scaffoldAttribute != null)
                    dynamicPropertyMetadata.Scaffold = scaffoldAttribute.Scaffold;
                else
                    dynamicPropertyMetadata.Scaffold = dynamicPropertyMetadata.IsCollection || dynamicPropertyMetadata.IsSimple || dynamicPropertyMetadata.IsComplexEntity;
            }       
        }
    }
}
