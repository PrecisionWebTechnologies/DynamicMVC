using System;
using System.Collections.Generic;
using System.Linq;
using DynamicMVC.ApplicationMetadataLibrary.Interfaces;
using DynamicMVC.ApplicationMetadataLibrary.Models;

namespace DynamicMVC.ApplicationMetadataLibrary.Builders
{
    public class ApplicationEntityBuilder : IApplicationEntityBuilder
    {
        public IEnumerable<ApplicationEntity> Build(IEnumerable<ApplicationEntityMetadata> applicationEntityMetadatas)
        {
            var applicationEntities = new List<ApplicationEntity>();
            // ReSharper disable once PossibleMultipleEnumeration
            var types = applicationEntityMetadatas.Select(x => x.EntityType).ToList();
            foreach (var type in types)
            {
                // ReSharper disable once PossibleMultipleEnumeration
                var applicationEntityMetadata = applicationEntityMetadatas.Single(x => x.EntityType == type);
                var applicationEntity = new ApplicationEntity();
                applicationEntity.EntityType = type;
                applicationEntity.Attributes = type.GetCustomAttributes(true).Select(x => (Attribute) x).ToList();
                foreach (var propertyInfo in type.GetProperties())
                {
                    applicationEntity.ApplicationEntityProperties.Add(new ApplicationEntityProperty(propertyInfo, types));
                }
                applicationEntityMetadata.ApplicationEntity = applicationEntity;
                applicationEntity.ApplicationEntityMetadata = applicationEntityMetadata;
                applicationEntities.Add(applicationEntity);
            }
        
            return applicationEntities;
        }
        
    }
}
