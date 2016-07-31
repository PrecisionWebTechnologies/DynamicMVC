using System.Collections.Generic;

namespace DynamicMVC.DynamicEntityMetadataLibrary.Interfaces
{
    public interface IReflectionManager
    {
        IEnumerable<IReflectedDynamicClass> GetReflectedDynamicClasses();
    }
}