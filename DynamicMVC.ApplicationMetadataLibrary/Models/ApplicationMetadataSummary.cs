using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DynamicMVC.ApplicationMetadataLibrary.Interfaces;
using ReflectionLibrary.Interfaces;
using ReflectionLibrary.Models;

namespace DynamicMVC.ApplicationMetadataLibrary.Models
{
    /// <summary>
    /// This class holds the results returned to the application for processing
    /// </summary>
    public class ApplicationMetadataSummary : IValidatableObject
    {
        private readonly IApplicationMetadataSummaryValidator[] _applicationMetadataSummaryValidators;

        public ApplicationMetadataSummary(IApplicationMetadataSummaryValidator[] applicationMetadataSummaryValidators)
        {
            ApplicationControllers = new HashSet<ApplicationControllerMetadata>();
            ApplicationEntities = new HashSet<ApplicationEntity>();
            ApplicationEntityMetadatas = new HashSet<ApplicationEntityMetadata>();
            ApplicationEntityMetadataReflectedClasses = new HashSet<IReflectedClass>();
            ApplicationEntityReflectedClasses = new HashSet<IReflectedClass>();

            _applicationMetadataSummaryValidators = applicationMetadataSummaryValidators;
        }

        public ICollection<ApplicationControllerMetadata> ApplicationControllers { get; set; }
        public ICollection<ApplicationEntity> ApplicationEntities { get; set; }
        public ICollection<ApplicationEntityMetadata> ApplicationEntityMetadatas { get; set; }
        public ICollection<IReflectedClass> ApplicationEntityMetadataReflectedClasses { get; set; }
        public ICollection<IReflectedClass> ApplicationEntityReflectedClasses { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            foreach (var applicationMetadataSummaryValidator in _applicationMetadataSummaryValidators)
            {
                results.AddRange(applicationMetadataSummaryValidator.Validate(this));
            }
            return results;
        }
    }
}
