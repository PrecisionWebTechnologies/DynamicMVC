using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DynamicMVC.ApplicationMetadataLibrary.Models;

namespace DynamicMVC.ApplicationMetadataLibrary.Interfaces
{
    public interface IApplicationMetadataSummaryValidator
    {
        IEnumerable<ValidationResult> Validate(ApplicationMetadataSummary applicationMetadataSummary);
    }
}
