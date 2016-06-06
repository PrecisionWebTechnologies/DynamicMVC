using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using DynamicMVC.ApplicationMetadataLibrary.Interfaces;
using DynamicMVC.Shared.Interfaces;

namespace DynamicMVC.ApplicationMetadataLibrary.Strategies.ApplicationMetadataProviderValidators
{
    public class SimpleTypeParsersAreUnique : IApplicationMetadataProviderValidator
    {
        private readonly ISimpleTypeParser[] _simpleTypeParsers;

        public SimpleTypeParsersAreUnique(ISimpleTypeParser[] simpleTypeParsers)
        {
            _simpleTypeParsers = simpleTypeParsers;
        }

        public IEnumerable<ValidationResult> Validate(IApplicationMetadataProvider applicationMetadataProvider)
        {
            var validationResults = new List<ValidationResult>();
            foreach (var simpleType in _simpleTypeParsers.ToList())
            {
                if (_simpleTypeParsers.Count(x => x.GetSimpleType() == simpleType.GetSimpleType()) > 2)
                {
                    validationResults.Add(new ValidationResult("SimpleTypeParser of type " + simpleType.GetSimpleType().Name + " is duplicated."));
                }
            }
            return validationResults;
        }
    }
}
