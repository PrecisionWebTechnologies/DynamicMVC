using System.Linq;
using DynamicMVC.DynamicEntityMetadataLibrary.Interfaces;
using DynamicMVC.DynamicEntityMetadataLibrary.Models;
using DynamicMVC.EntityMetadataLibrary.Models;
using DynamicMVC.Shared.Interfaces;

namespace DynamicMVC.DynamicEntityMetadataLibrary.Strategies.DynamicEntityMetadataBuilderHelper
{
    /// <summary>
    /// Sets the Default Property for a given entity
    /// </summary>
    public class DefaultPropertyBuilderHelper : IDynamicEntityMetadataBuilderHelper
    {
        private readonly INamingConventionManager _namingConventionManager;

        public DefaultPropertyBuilderHelper(INamingConventionManager namingConventionManager)
        {
            _namingConventionManager = namingConventionManager;
        }

        public void Build(DynamicEntityMetadata dynamicEntityMetadata, EntityMetadata entityMetadata)
        { 
            var propertyNames = entityMetadata.EntityPropertyMetadata.Select(x=>x.PropertyName).ToList();
            var defaultPropertyName = _namingConventionManager.FindDefaultPropertyName(entityMetadata.TypeName, propertyNames);
            dynamicEntityMetadata.DefaultProperty=dynamicEntityMetadata.DynamicPropertyMetadatas.Single(x=>x.PropertyName==defaultPropertyName);
        }
    }
}
