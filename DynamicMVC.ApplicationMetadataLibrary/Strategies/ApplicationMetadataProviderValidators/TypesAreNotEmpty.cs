using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using DynamicMVC.ApplicationMetadataLibrary.Interfaces;
using DynamicMVC.Shared.Interfaces;

namespace DynamicMVC.ApplicationMetadataLibrary.Strategies.ApplicationMetadataProviderValidators
{
    public class TypesAreNotEmpty : IApplicationMetadataProviderValidator
    {
        public IEnumerable<ValidationResult> Validate(IApplicationMetadataProvider applicationMetadataProvider)
        {
            var validationResults = new List<ValidationResult>();
            if (!applicationMetadataProvider.MetadataAssemblyTypes.Any())
                validationResults.Add(new ValidationResult("Metadata assembly must contain at least one type."));
            if (!applicationMetadataProvider.EntityAssemblyTypes.Any())
                validationResults.Add(new ValidationResult("Entity assembly must contain at least one type."));
            if (!applicationMetadataProvider.MvcAssemblyTypes.Any())
                validationResults.Add(new ValidationResult("Mvc assembly must contain at least one type."));

            return validationResults;
        }
    }
}
