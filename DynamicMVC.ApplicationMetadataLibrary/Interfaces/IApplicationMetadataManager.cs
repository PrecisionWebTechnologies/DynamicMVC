using DynamicMVC.ApplicationMetadataLibrary.Models;

namespace DynamicMVC.ApplicationMetadataLibrary.Interfaces
{
    public interface IApplicationMetadataManager
    {
        ApplicationMetadataSummary GetApplicationMetadataSummary();
    }
}