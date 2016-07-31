using System.Collections.Generic;
using ReflectionLibrary.Interfaces;

namespace DynamicMVC.Shared.Interfaces
{
    public interface IApplicationReflectionProvider
    {
        IEnumerable<IReflectedClass> ReflectedDynamicEntities { get; set; }
    }
}
