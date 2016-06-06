using System.Linq;
using DynamicMVC.ApplicationMetadataLibrary.Interfaces;
using DynamicMVC.ApplicationMetadataLibrary.Models;
using DynamicMVC.Shared.Extensions;
using ReflectionLibrary.Interfaces;

namespace DynamicMVC.ApplicationMetadataLibrary
{
    /// <summary>
    /// This class manages code access for higher layers.
    /// </summary>
    public class ApplicationMetadataManager : IApplicationMetadataManager
    {
        private readonly IApplicationControllerMetadataBuilder _applicationControllerMetadataBuilder;
        private readonly IApplicationEntityMetadataBuilder _applicationEntityMetadataBuilder;
        private readonly IApplicationEntityBuilder _applicationEntityBuilder;
        private readonly IApplicationMetadataValidationManager _applicationMetadataValidationManager;
        private readonly ApplicationMetadataSummary _applicationMetadataSummary;
        private readonly IApplicationMetadataSummaryPreValidateProcess[] _applicationMetadataSummaryPreValidates;
        private readonly IReflectedClassesBuilder _reflectedClassesBuilder;

        public ApplicationMetadataManager(IApplicationControllerMetadataBuilder applicationControllerMetadataBuilder, IApplicationEntityMetadataBuilder applicationEntityMetadataBuilder, IApplicationEntityBuilder applicationEntityBuilder, IApplicationMetadataValidationManager applicationMetadataValidationManager, ApplicationMetadataSummary applicationMetadataSummary, IApplicationMetadataSummaryPreValidateProcess[] applicationMetadataSummaryPreValidates, IReflectedClassesBuilder reflectedClassesBuilder)
        {
            _applicationControllerMetadataBuilder = applicationControllerMetadataBuilder;
            _applicationEntityMetadataBuilder = applicationEntityMetadataBuilder;
            _applicationEntityBuilder = applicationEntityBuilder;
            _applicationMetadataValidationManager = applicationMetadataValidationManager;
            _applicationMetadataSummary = applicationMetadataSummary;
            _applicationMetadataSummaryPreValidates = applicationMetadataSummaryPreValidates;
            _reflectedClassesBuilder = reflectedClassesBuilder;
        }

        public ApplicationMetadataSummary GetApplicationMetadataSummary()
        {
            _applicationMetadataValidationManager.ValidateApplicationMetadataProvider();
            _applicationMetadataSummary.ApplicationControllers.AddRange(_applicationControllerMetadataBuilder.Build());
            _applicationMetadataSummary.ApplicationEntityMetadatas.AddRange(_applicationEntityMetadataBuilder.Build());
            _applicationMetadataSummary.ApplicationEntities.AddRange(_applicationEntityBuilder.Build(_applicationMetadataSummary.ApplicationEntityMetadatas));

            _applicationMetadataSummary.ApplicationEntityMetadataReflectedClasses.AddRange(_reflectedClassesBuilder.BuildApplicationEntityMetadataReflectedClasses());
            _applicationMetadataSummary.ApplicationEntityReflectedClasses.AddRange(_reflectedClassesBuilder.BuildApplicationEntityReflectedClasses(_applicationMetadataSummary.ApplicationEntityMetadatas.Select(x=>x.EntityType)));

            foreach (var applicationMetadataSummaryPreValidate in _applicationMetadataSummaryPreValidates)
            {
                applicationMetadataSummaryPreValidate.Process(_applicationMetadataSummary);
            }
            _applicationMetadataValidationManager.ValidateApplicationSummary(_applicationMetadataSummary);
            return _applicationMetadataSummary;
        }
    }
}
