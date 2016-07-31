using System.Collections.Generic;
using DynamicMVC.DynamicEntityMetadataLibrary.Models;
using ReflectionLibrary.Interfaces;

namespace DynamicMVC.DynamicEntityMetadataLibrary.Interfaces
{
    public interface IDynamicPropertyMetadataBuilder
    {
        IEnumerable<DynamicPropertyMetadata> Build(IReflectedClass reflectedClass, IEnumerable<IReflectedClass> reflectedClasses);
    }
}
