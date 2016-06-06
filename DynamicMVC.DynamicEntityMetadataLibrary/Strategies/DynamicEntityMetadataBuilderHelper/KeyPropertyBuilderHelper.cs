using System;
using System.Linq;
using DynamicMVC.DynamicEntityMetadataLibrary.Interfaces;
using DynamicMVC.DynamicEntityMetadataLibrary.Models;
using DynamicMVC.EntityMetadataLibrary.Models;

namespace DynamicMVC.DynamicEntityMetadataLibrary.Strategies.DynamicEntityMetadataBuilderHelper
{
    /// <summary>
    /// Sets the Key Property for a given entity
    /// </summary>
    public class KeyPropertyBuilderHelper : IDynamicEntityMetadataBuilderHelper
    {
        public void Build(DynamicEntityMetadata dynamicEntityMetadata, EntityMetadata entityMetadata)
        {
            var keyPropertyName = entityMetadata.DynamicEntityAttribute().Key;
            var keyProperty = dynamicEntityMetadata.DynamicPropertyMetadatas.SingleOrDefault(x => x.PropertyName == keyPropertyName);
            if (keyProperty == null)
                throw new Exception("Could not find KeyValue for " + keyPropertyName + " and type " + dynamicEntityMetadata.TypeName);
            dynamicEntityMetadata.KeyProperty = keyProperty;
        }
    }
}
