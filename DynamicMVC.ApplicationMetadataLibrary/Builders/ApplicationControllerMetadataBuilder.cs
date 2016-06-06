using System.Collections.Generic;
using System.Linq;
using DynamicMVC.ApplicationMetadataLibrary.Interfaces;
using DynamicMVC.ApplicationMetadataLibrary.Models;
using DynamicMVC.Shared.Extensions;
using DynamicMVC.Shared.Interfaces;

namespace DynamicMVC.ApplicationMetadataLibrary.Builders
{
    public class ApplicationControllerMetadataBuilder : IApplicationControllerMetadataBuilder
    {
        private readonly IApplicationMetadataProvider _applicationMetadataProvider;
        private readonly IApplicationControllerMethodMetadataBuilder _applicationControllerMethodMetadataBuilder;
        private readonly INamingConventionManager _namingConventionManager;
        
        public ApplicationControllerMetadataBuilder(IApplicationMetadataProvider applicationMetadataProvider, IApplicationControllerMethodMetadataBuilder applicationControllerMethodMetadataBuilder, INamingConventionManager namingConventionManager)
        {
            _applicationMetadataProvider = applicationMetadataProvider;
            _applicationControllerMethodMetadataBuilder = applicationControllerMethodMetadataBuilder;
            _namingConventionManager = namingConventionManager;    
        }

        public IEnumerable<ApplicationControllerMetadata> Build()
        {
            var applicationControllerMetadatas = new List<ApplicationControllerMetadata>();
            foreach (var type in _applicationMetadataProvider.MvcAssemblyTypes.Where(x => _namingConventionManager.IsController(x)))
            {
                var applicationControllerMetadata = new ApplicationControllerMetadata(type.Name);
                applicationControllerMetadatas.Add(applicationControllerMetadata);
                var applicationControllerMethodMetadatas = _applicationControllerMethodMetadataBuilder.Build(applicationControllerMetadata, type);
                applicationControllerMetadata.ApplicationControllerMethods.AddRange(applicationControllerMethodMetadatas);
            }

            return applicationControllerMetadatas;
        }
    }
}
