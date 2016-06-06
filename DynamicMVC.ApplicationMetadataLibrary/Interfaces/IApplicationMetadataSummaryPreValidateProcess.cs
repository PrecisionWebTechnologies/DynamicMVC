using DynamicMVC.ApplicationMetadataLibrary.Models;

namespace DynamicMVC.ApplicationMetadataLibrary.Interfaces
{
    public interface IApplicationMetadataSummaryPreValidateProcess
    {
        void Process(ApplicationMetadataSummary applicationMetadataSummary);
    }
}
