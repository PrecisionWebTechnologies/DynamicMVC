using System;
using System.Collections.Generic;

namespace DynamicMVC.Shared.Interfaces
{
    public interface IApplicationMetadataProvider
    {
        IEnumerable<Type> MvcAssemblyTypes { get; set; }
        IEnumerable<Type> MetadataAssemblyTypes { get; set; }
        IEnumerable<Type> EntityAssemblyTypes { get; set; }
    }
}