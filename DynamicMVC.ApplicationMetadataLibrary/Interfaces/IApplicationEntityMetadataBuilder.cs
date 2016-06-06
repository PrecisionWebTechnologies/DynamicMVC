using System.Collections.Generic;
using DynamicMVC.ApplicationMetadataLibrary.Models;

namespace DynamicMVC.ApplicationMetadataLibrary.Interfaces
{
    public interface IApplicationEntityMetadataBuilder
    {
        IEnumerable<ApplicationEntityMetadata> Build();
    }
}