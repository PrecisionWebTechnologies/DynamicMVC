using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using DynamicMVC.Annotations;
using DynamicMVC.ApplicationMetadataLibrary.Interfaces;
using DynamicMVC.Shared.Extensions;
using DynamicMVC.Shared.Interfaces;

namespace DynamicMVC.ApplicationMetadataLibrary.Strategies.ApplicationMetadataProviderValidators
{
    public class DynamicEntityAttributeIsNotDuplicated : IApplicationMetadataProviderValidator
    {
        public IEnumerable<ValidationResult> Validate(IApplicationMetadataProvider applicationMetadataProvider)
        {
            var validationResults = new List<ValidationResult>();
            foreach (var dynamicEntityType in applicationMetadataProvider.MetadataAssemblyTypes.GetTypesWithAttribute<DynamicEntityAttribute>())
            {
                if (dynamicEntityType.GetCustomAttributes(true).Count(x => x is DynamicEntityAttribute) > 1)
                {
                    validationResults.Add(new ValidationResult("Duplicate Entity Attribute found on type " + dynamicEntityType.Name));
                }
            }
            return validationResults;
        }
    }
}
