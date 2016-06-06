using System.Collections.Generic;
using DynamicMVC.ApplicationMetadataLibrary.Models;

namespace DynamicMVC.ApplicationMetadataLibrary.Interfaces
{
    public interface IApplicationControllerMetadataBuilder
    {
        IEnumerable<ApplicationControllerMetadata> Build();
    }
}