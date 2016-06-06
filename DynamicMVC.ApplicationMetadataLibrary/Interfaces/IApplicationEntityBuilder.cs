using System.Collections.Generic;
using DynamicMVC.ApplicationMetadataLibrary.Models;

namespace DynamicMVC.ApplicationMetadataLibrary.Interfaces
{
    public interface IApplicationEntityBuilder
    {
        IEnumerable<ApplicationEntity> Build(IEnumerable<ApplicationEntityMetadata> applicationEntityMetadatas);
    }
}