using System;
using System.Collections.Generic;
using System.Linq;
using DynamicMVC.DynamicEntityMetadataLibrary.Interfaces;
using DynamicMVC.DynamicEntityMetadataLibrary.Models;
using DynamicMVC.EntityMetadataLibrary.Interfaces;
using DynamicMVC.EntityMetadataLibrary.Models;

namespace DynamicMVC.DynamicEntityMetadataLibrary
{
    /// <summary>
    /// This class manages code access to the higher layers.
    /// </summary>
    public class DynamicEntityMetadataManager : IDynamicEntityMetadataManager
    {
        private readonly IEntityMetadataManager _entityMetadataManager;
        private readonly IDynamicEntityMetadataBuilder _dynamicEntityMetadataBuilder;
        private readonly IDynamicEntityMetadataValidator[] _dynamicEntityMetadataValidators;

        public DynamicEntityMetadataManager(IEntityMetadataManager entityMetadataManager, IDynamicEntityMetadataBuilder dynamicEntityMetadataBuilder, IDynamicEntityMetadataValidator[] dynamicEntityMetadataValidators)
        {
            _entityMetadataManager = entityMetadataManager;
            _dynamicEntityMetadataBuilder = dynamicEntityMetadataBuilder;
            _dynamicEntityMetadataValidators = dynamicEntityMetadataValidators;
        }

        public IEnumerable<DynamicEntityMetadata> GetDynamicEntityMetadatas()
        {
            var entityMetadatas = _entityMetadataManager.GetEntityMetadatas();
            var results = _dynamicEntityMetadataBuilder.Build(entityMetadatas).ToList();
            Validate(results);
            return results;
        }

        public IEnumerable<DynamicEntityMetadata> GetDynamicEntityMetadatas(IEnumerable<EntityMetadata> entityMetadatas)
        {
            var results = _dynamicEntityMetadataBuilder.Build(entityMetadatas).ToList();
            Validate(results);
            return results;
        }

        private void Validate(IEnumerable<DynamicEntityMetadata> dynamicEntityMetadatas)
        {
            foreach (var dynamicEntityMetadata in dynamicEntityMetadatas)
            {
                foreach (var validator in _dynamicEntityMetadataValidators)
                {
                    var validationResult = validator.Validate(dynamicEntityMetadata);
                    if (!string.IsNullOrWhiteSpace(validationResult))
                    {
                        throw new Exception(validationResult);
                    }
                }
            }
        }
    }
}
