using System;
using System.Collections.Generic;
using DynamicMVC.ApplicationMetadataLibrary.Models;

namespace DynamicMVC.ApplicationMetadataLibrary.Interfaces
{
    public interface IApplicationEntityMetadataPropertyBuilder
    {
        IEnumerable<ApplicationEntityMetadataProperty> Build(Type type);
    }
}