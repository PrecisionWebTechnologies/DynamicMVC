using System.Collections.Generic;
using System.Linq;
using DynamicMVC.DynamicEntityMetadataLibrary.Interfaces;
using DynamicMVC.DynamicEntityMetadataLibrary.Models;
using DynamicMVC.EntityMetadataLibrary.Models;

namespace DynamicMVC.DynamicEntityMetadataLibrary.Builders
{
    public class DynamicPropertyMetadataBuilder : IDynamicPropertyMetadataBuilder
    {
        private readonly INavigationPropertyManager _navigationPropertyManager;
        private readonly IDynamicPropertyMetadataBuilderHelper[] _dynamicPropertyMetadataBuilderHelpers;

        public DynamicPropertyMetadataBuilder(INavigationPropertyManager navigationPropertyManager, IDynamicPropertyMetadataBuilderHelper[] dynamicPropertyMetadataBuilderHelpers)
        {
            _navigationPropertyManager = navigationPropertyManager;
            _dynamicPropertyMetadataBuilderHelpers = dynamicPropertyMetadataBuilderHelpers;
        }

        public IEnumerable<DynamicPropertyMetadata> Build(EntityMetadata entityMetadata)
        {
            var dynamicPropertyMetadatas = new List<DynamicPropertyMetadata>();
            foreach (var entityPropertyMetadata in entityMetadata.EntityPropertyMetadata.ToList())
            {
                var dynamicPropertyMetadata = new DynamicPropertyMetadata(entityPropertyMetadata);
                if (_navigationPropertyManager.IsForeignKey(entityPropertyMetadata))
                {
                    dynamicPropertyMetadata = new DynamicForiegnKeyPropertyMetadata(entityPropertyMetadata);
                }
                if (entityPropertyMetadata.IsComplexEntity)
                {
                    dynamicPropertyMetadata = new DynamicComplexPropertyMetadata(entityPropertyMetadata);
                }
                if (entityPropertyMetadata.IsCollection)
                {
                    dynamicPropertyMetadata = new DynamicCollectionEntityPropertyMetadata(entityPropertyMetadata);
                }

                dynamicPropertyMetadata.ParseValue = entityPropertyMetadata.ParseValue;
                var keyName = entityMetadata.DynamicEntityAttribute().Key;
                dynamicPropertyMetadata.IsPrimaryKey = dynamicPropertyMetadata.PropertyName == keyName;

                foreach (var dynamicPropertyMetadataMaterializerHelper in _dynamicPropertyMetadataBuilderHelpers)
                {
                    dynamicPropertyMetadataMaterializerHelper.Build(entityMetadata, entityPropertyMetadata, dynamicPropertyMetadata);
                }
                dynamicPropertyMetadatas.Add(dynamicPropertyMetadata);

            }
            return dynamicPropertyMetadatas;
        }
    }
}
