using System.ComponentModel.DataAnnotations;
using System.Linq;
using DynamicMVC.ApplicationMetadataLibrary.Interfaces;
using DynamicMVC.ApplicationMetadataLibrary.Models;
using DynamicMVC.Shared.Interfaces;

namespace DynamicMVC.ApplicationMetadataLibrary.Managers
{
    public class ApplicationMetadataValidationManager : IApplicationMetadataValidationManager
    {
        private readonly IApplicationMetadataProvider _applicationMetadataProvider;
        private readonly IValidationManager _validationManager;
        private readonly IApplicationMetadataProviderValidator[] _applicationMetadataProviderValidators;

        public ApplicationMetadataValidationManager(IValidationManager validationManager, IApplicationMetadataProvider applicationMetadataProvider, IApplicationMetadataProviderValidator[] applicationMetadataProviderValidators)
        {
            _validationManager = validationManager;
            _applicationMetadataProvider = applicationMetadataProvider;
            _applicationMetadataProviderValidators = applicationMetadataProviderValidators;
        }

        public void ValidateApplicationMetadataProvider()
        {
            foreach (var applicationMetadataProviderValidator in _applicationMetadataProviderValidators)
            {
                var validationResults = applicationMetadataProviderValidator.Validate(_applicationMetadataProvider).ToList();
                if (!validationResults.Any()) continue;
                var validationResult = validationResults.First();
                throw new ValidationException(validationResult.ErrorMessage);
            }
        }

        public void ValidateApplicationSummary(ApplicationMetadataSummary applicationMetadataSummary)
        {
            _validationManager.ValidateObject(applicationMetadataSummary);
        }
    }
}
