using System.Collections.Generic;
using DynamicMVC.DynamicEntityMetadataLibrary.Models;
using DynamicMVC.UI.DynamicMVC.ViewModels.DynamicPropertyViewModels;

namespace DynamicMVC.UI.DynamicMVC.Interfaces
{
    public interface IDynamicFilterManager
    {
        IEnumerable<DynamicFilterViewModel> GetFilterPropertyViewModels(DynamicEntityMetadata dynamicEntityMetadata, RouteValueDictionaryWrapper routeValueDictionaryWrapper);
        string GetFilterMessage(DynamicEntityMetadata dynamicEntityMetadata, RouteValueDictionaryWrapper routeValueDictionaryWrapper);
        IEnumerable<IDynamicFilter> GetDynamicFilters(DynamicEntityMetadata dynamicEntityMetadata, RouteValueDictionaryWrapper routeValueDictionaryWrapper);
    }
}