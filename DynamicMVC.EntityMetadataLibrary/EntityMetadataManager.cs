using System.Collections.Generic;
using DynamicMVC.ApplicationMetadataLibrary.Interfaces;
using DynamicMVC.EntityMetadataLibrary.Interfaces;
using DynamicMVC.EntityMetadataLibrary.Models;

namespace DynamicMVC.EntityMetadataLibrary
{
    /// <summary>
    /// This class manages code access to higher layers.
    /// </summary>
    public class EntityMetadataManager : IEntityMetadataManager
    {
        private readonly IApplicationMetadataManager _applicationMetadataManager;
        private readonly IEntityMetadataBuilder _entityMetadataBuilder;

        public EntityMetadataManager(IApplicationMetadataManager applicationMetadataManager, IEntityMetadataBuilder entityMetadataBuilder)
        {
            _applicationMetadataManager = applicationMetadataManager;
            _entityMetadataBuilder = entityMetadataBuilder;
        }

        public IEnumerable<EntityMetadata> GetEntityMetadatas()
        {
            var applicationMetadataSummary = _applicationMetadataManager.GetApplicationMetadataSummary();
            return _entityMetadataBuilder.Build(applicationMetadataSummary);   
        }
    }
}
