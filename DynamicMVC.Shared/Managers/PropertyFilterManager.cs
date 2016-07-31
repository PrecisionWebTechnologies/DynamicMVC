using System.Collections.Generic;
using System.Linq;
using DynamicMVC.Shared.Extensions;
using DynamicMVC.Shared.Interfaces;

namespace DynamicMVC.Shared.Managers
{
    public class PropertyFilterManager : IPropertyFilterManager
    {
        public IEnumerable<T> FilterAndOrderProperties<T>(IEnumerable<T> properties, string propertyList) where T : IPropertyWithPropertyName
        {
            var filteredProperties = new List<T>();
            var propertiesFilter = propertyList.SplitAndTrim();
            foreach (var propertyFilter in propertiesFilter)
            {
                // ReSharper disable once PossibleMultipleEnumeration
                var property = properties.SingleOrDefault(x => x.PropertyName() == propertyFilter);
                if (property != null)
                    filteredProperties.Add(property);
            }
            return filteredProperties;
        }
    }
}
