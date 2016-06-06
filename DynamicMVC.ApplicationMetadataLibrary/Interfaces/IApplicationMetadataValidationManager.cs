using DynamicMVC.ApplicationMetadataLibrary.Models;

namespace DynamicMVC.ApplicationMetadataLibrary.Interfaces
{
    public interface IApplicationMetadataValidationManager
    {
        void ValidateApplicationMetadataProvider();
        void ValidateApplicationSummary(ApplicationMetadataSummary applicationMetadataSummary);
    }
}