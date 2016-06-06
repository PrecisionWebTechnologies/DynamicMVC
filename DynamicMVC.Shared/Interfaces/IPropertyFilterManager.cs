using System.Collections.Generic;

namespace DynamicMVC.Shared.Interfaces
{
    public interface IPropertyFilterManager
    {
        IEnumerable<T> FilterAndOrderProperties<T>(IEnumerable<T> properties, string propertyList) where T : IPropertyWithPropertyName;
    }
}