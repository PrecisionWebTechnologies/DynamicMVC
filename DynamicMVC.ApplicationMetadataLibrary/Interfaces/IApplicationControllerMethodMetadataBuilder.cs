using System;
using System.Collections.Generic;
using DynamicMVC.ApplicationMetadataLibrary.Models;

namespace DynamicMVC.ApplicationMetadataLibrary.Interfaces
{
    public interface IApplicationControllerMethodMetadataBuilder
    {
        IEnumerable<ApplicationControllerMethodMetadata> Build(ApplicationControllerMetadata applicationControllerMetadata, Type type);
    }
}