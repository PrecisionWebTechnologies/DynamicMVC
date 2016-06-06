using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DynamicMVC.ApplicationMetadataLibrary.Interfaces;

namespace DynamicMVC.ApplicationMetadataLibrary.Strategies.ApplicationMetadataSummaryValidators
{
    public class EveryMetadataHasMatchingEntity : IApplicationMetadataSummaryValidator
    {
        public IEnumerable<ValidationResult> Validate(Models.ApplicationMetadataSummary applicationMetadataSummary)
        {
            var validationResults = new List<ValidationResult>();
            foreach (var applicationMetadata in applicationMetadataSummary.ApplicationEntityMetadatas)
            {
                if (applicationMetadata.ApplicationEntity == null)
                {
                    validationResults.Add(new ValidationResult(applicationMetadata.EntityType.Name + " does not have a matching ApplicationEntity type."));
                }
            }
            return validationResults;
        }
    }
}
