using System;
using System.Collections.Generic;
using System.Linq;
using DynamicMVC.DynamicEntityMetadataLibrary.Interfaces;
using DynamicMVC.DynamicEntityMetadataLibrary.Models;
using ReflectionLibrary.Interfaces;

namespace DynamicMVC.DynamicEntityMetadataLibrary
{
    /// <summary>
    /// This class manages code access to the higher layers.
    /// </summary>
    public class DynamicEntityMetadataManager : IDynamicEntityMetadataManager
    {
        private readonly IDynamicEntityMetadataBuilder _dynamicEntityMetadataBuilder;
        private readonly IDynamicEntityMetadataValidator[] _dynamicEntityMetadataValidators;
        private readonly IReflectionManager _reflectionManager;

        public DynamicEntityMetadataManager(IDynamicEntityMetadataBuilder dynamicEntityMetadataBuilder, IDynamicEntityMetadataValidator[] dynamicEntityMetadataValidators, IReflectionManager reflectionManager)
        {
            _reflectionManager = reflectionManager;
            _dynamicEntityMetadataBuilder = dynamicEntityMetadataBuilder;
            _dynamicEntityMetadataValidators = dynamicEntityMetadataValidators;
        }

        public IEnumerable<DynamicEntityMetadata> GetDynamicEntityMetadatas()
        {
            var reflectedClasses = _reflectionManager.GetReflectedDynamicClasses();
            var results = _dynamicEntityMetadataBuilder.Build(reflectedClasses).ToList();
            Validate(results);
            return results;
        }

        public IEnumerable<DynamicEntityMetadata> GetDynamicEntityMetadatas(IEnumerable<IReflectedDynamicClass> reflectedClasses)
        {
            var results = _dynamicEntityMetadataBuilder.Build(reflectedClasses).ToList();
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
