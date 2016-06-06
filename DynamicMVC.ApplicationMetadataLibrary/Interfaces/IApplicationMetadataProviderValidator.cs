using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DynamicMVC.Shared.Interfaces;

namespace DynamicMVC.ApplicationMetadataLibrary.Interfaces
{
    public interface IApplicationMetadataProviderValidator
    {
        IEnumerable<ValidationResult> Validate(IApplicationMetadataProvider applicationMetadataProvider);
    }
}
