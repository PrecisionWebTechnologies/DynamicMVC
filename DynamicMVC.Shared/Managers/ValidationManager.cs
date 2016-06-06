using System.ComponentModel.DataAnnotations;
using System.Linq;
using DynamicMVC.Shared.Extensions;
using DynamicMVC.Shared.Interfaces;

namespace DynamicMVC.Shared.Managers
{
    public class ValidationManager : IValidationManager
    {
        public void ValidateObject(object item)
        {
            if (item.GetType().ImplementsInterface<IValidatableObject>())
            {
                var validationResults = ((IValidatableObject) item).Validate(null).ToList();
                if (validationResults.Any())
                {
                    var validationResult = validationResults.First();
                    throw new ValidationException(validationResult.ErrorMessage);
                }
            }
        }
    }
}
