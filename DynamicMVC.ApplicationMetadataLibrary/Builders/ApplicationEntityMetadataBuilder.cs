using System;
using System.Collections.Generic;
using System.Linq;
using DynamicMVC.Annotations;
using DynamicMVC.ApplicationMetadataLibrary.Interfaces;
using DynamicMVC.ApplicationMetadataLibrary.Models;
using DynamicMVC.Shared.Extensions;
using DynamicMVC.Shared.Interfaces;

namespace DynamicMVC.ApplicationMetadataLibrary.Builders
{
    public class ApplicationEntityMetadataBuilder : IApplicationEntityMetadataBuilder
    {
        private readonly IApplicationMetadataProvider _applicationMetadataProvider;
        private readonly IApplicationEntityMetadataPropertyBuilder _applicationEntityMetadataPropertyBuilder;
        
        public ApplicationEntityMetadataBuilder(IApplicationMetadataProvider applicationMetadataProvider, IApplicationEntityMetadataPropertyBuilder applicationEntityMetadataPropertyBuilder)
        {
            _applicationMetadataProvider = applicationMetadataProvider;
            _applicationEntityMetadataPropertyBuilder = applicationEntityMetadataPropertyBuilder;
        }

        public IEnumerable<ApplicationEntityMetadata> Build()
        {
            var applicationEntityMetadatas = new List<ApplicationEntityMetadata>();
            foreach (var type in _applicationMetadataProvider.MetadataAssemblyTypes.GetTypesWithAttribute<DynamicEntityAttribute>())
            {
                var attributes = type.GetCustomAttributes(true).ToList().Select(x => (Attribute)x);
                var applictionEntityMetadata = new ApplicationEntityMetadata(attributes, type);
                applicationEntityMetadatas.Add(applictionEntityMetadata);
                var applicationEntityMetadataProperties = _applicationEntityMetadataPropertyBuilder.Build(type);
                applictionEntityMetadata.ApplicationEntityMetadataProperty.AddRange(applicationEntityMetadataProperties);
            }
            return applicationEntityMetadatas;
        }
    }
}
