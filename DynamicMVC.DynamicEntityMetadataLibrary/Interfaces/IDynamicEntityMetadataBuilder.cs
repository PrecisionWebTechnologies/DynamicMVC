using System.Collections.Generic;
using DynamicMVC.DynamicEntityMetadataLibrary.Models;
using ReflectionLibrary.Interfaces;

namespace DynamicMVC.DynamicEntityMetadataLibrary.Interfaces
{
    public interface IDynamicEntityMetadataBuilder
    {
        IEnumerable<DynamicEntityMetadata> Build(IEnumerable<IReflectedDynamicClass> reflectedClasses);
    }
}