using System.Collections.Generic;
using System.Linq;
using DynamicMVC.DynamicEntityMetadataLibrary.Interfaces;
using DynamicMVC.DynamicEntityMetadataLibrary.Models;
// ReSharper disable PossibleMultipleEnumeration

namespace DynamicMVC.DynamicEntityMetadataLibrary.Strategies.DynamicEntityMetadataFixups
{
    public class DynamicCollectionEntityPropertyMetadataFixup : IDynamicEntityMetadataPropertyFixup
    {
        private readonly INavigationPropertyManager _navigationPropertyManager;

        public DynamicCollectionEntityPropertyMetadataFixup(INavigationPropertyManager navigationPropertyManager)
        {
            _navigationPropertyManager = navigationPropertyManager;
        }

        public void Fixup(IEnumerable<DynamicEntityMetadata> dynamicEntityMetadatas)
        {
            foreach (var dynamicEntityMetadata in dynamicEntityMetadatas)
            {
                //assign DynamicComplexPropertyMetadata property for each DynamicCollectionPropertyMetadata
                foreach (var dynamicPropertyMetadata in dynamicEntityMetadata.DynamicPropertyMetadatas)
                {
                    if (dynamicPropertyMetadata.GetType() == typeof(DynamicCollectionEntityPropertyMetadata))
                    {
                        var dynamicCollectionEntityPropertyMetadata = (DynamicCollectionEntityPropertyMetadata)dynamicPropertyMetadata;
                        var collectionDynamicEntityMetadata = dynamicEntityMetadatas.Single(x => x.TypeName == dynamicCollectionEntityPropertyMetadata.TypeName);

                        //collectionEntityMetadata should have complex property of dynamicentity type or inverse property name
                        var foreignKeyName = _navigationPropertyManager.GetForiegnKeyNameByCollectionProperty(collectionDynamicEntityMetadata, dynamicEntityMetadata.TypeName, dynamicCollectionEntityPropertyMetadata);
                        dynamicCollectionEntityPropertyMetadata.ForiegnKeyPropertyName = foreignKeyName;
                    }
                }
            }
           
        }

        public int Order()
        {
            return 3;
        }
    }
}
