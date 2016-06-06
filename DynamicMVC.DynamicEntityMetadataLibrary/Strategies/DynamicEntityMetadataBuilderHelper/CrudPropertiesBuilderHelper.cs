using System.Linq;
using DynamicMVC.DynamicEntityMetadataLibrary.Interfaces;
using DynamicMVC.DynamicEntityMetadataLibrary.Models;
using DynamicMVC.EntityMetadataLibrary.Models;
using DynamicMVC.Shared.Interfaces;

namespace DynamicMVC.DynamicEntityMetadataLibrary.Strategies.DynamicEntityMetadataBuilderHelper
{
    public class CrudPropertiesBuilderHelper : IDynamicEntityMetadataBuilderHelper
    {
        private readonly IPropertyFilterManager _propertyFilterManager;

        public CrudPropertiesBuilderHelper(IPropertyFilterManager propertyFilterManager)
        {
            _propertyFilterManager = propertyFilterManager;
        }

        public void Build(DynamicEntityMetadata dynamicEntityMetadata, EntityMetadata entityMetadata)
        {
            var attribute = entityMetadata.DynamicEntityAttribute();
            dynamicEntityMetadata.CreateProperties = attribute.CreateProperties;
            dynamicEntityMetadata.IndexProperties = attribute.IndexProperties;
            dynamicEntityMetadata.EditProperties = attribute.EditProperties;
            dynamicEntityMetadata.DetailsProperties = attribute.DetailsProperties;

            dynamicEntityMetadata.ShowCreate = attribute.ShowCreate;
            dynamicEntityMetadata.ShowDetails = attribute.ShowDetails;
            dynamicEntityMetadata.ShowDelete = attribute.ShowDelete;
            dynamicEntityMetadata.ShowEdit = attribute.ShowEdit;

            dynamicEntityMetadata.ScaffoldProperties = dynamicEntityMetadata.DynamicPropertyMetadatas.Where(x => x.Scaffold).ToList();

            //set scaffoldCreateProperties
            if (!string.IsNullOrWhiteSpace(dynamicEntityMetadata.CreateProperties))
                dynamicEntityMetadata.ScaffoldCreateProperties = _propertyFilterManager.FilterAndOrderProperties(dynamicEntityMetadata.ScaffoldProperties, dynamicEntityMetadata.CreateProperties).ToList();
            else
                dynamicEntityMetadata.ScaffoldCreateProperties = dynamicEntityMetadata.ScaffoldProperties;

            //Set ScaffoldEditProperties
            if (!string.IsNullOrWhiteSpace(dynamicEntityMetadata.EditProperties))
                dynamicEntityMetadata.ScaffoldEditProperties = _propertyFilterManager.FilterAndOrderProperties(dynamicEntityMetadata.ScaffoldProperties, dynamicEntityMetadata.EditProperties).ToList();
            else
                dynamicEntityMetadata.ScaffoldEditProperties = dynamicEntityMetadata.ScaffoldProperties;

            //Set ScaffoldDetailsProperties
            if (!string.IsNullOrWhiteSpace(dynamicEntityMetadata.DetailsProperties))
                dynamicEntityMetadata.ScaffoldDetailsProperties = _propertyFilterManager.FilterAndOrderProperties(dynamicEntityMetadata.ScaffoldProperties, dynamicEntityMetadata.DetailsProperties).ToList();
            else
                dynamicEntityMetadata.ScaffoldDetailsProperties = dynamicEntityMetadata.ScaffoldProperties;

            //Set ScaffoldIndexProperties
            if (!string.IsNullOrWhiteSpace(dynamicEntityMetadata.IndexProperties))
                dynamicEntityMetadata.ScaffoldIndexProperties = _propertyFilterManager.FilterAndOrderProperties(dynamicEntityMetadata.ScaffoldProperties, dynamicEntityMetadata.IndexProperties).ToList();
            else
                dynamicEntityMetadata.ScaffoldIndexProperties = dynamicEntityMetadata.ScaffoldProperties;

        }
    }
}
