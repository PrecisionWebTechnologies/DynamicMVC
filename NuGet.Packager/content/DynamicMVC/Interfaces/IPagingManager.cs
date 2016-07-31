using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DynamicMVC.DynamicEntityMetadataLibrary.Models;

namespace DynamicMVC.UI.DynamicMVC.Interfaces
{
    public interface IPagingManager
    {
        string PreviousClassName(RouteValueDictionaryWrapper routeValueDictionaryWrapper);
        string NextClassName(RouteValueDictionaryWrapper routeValueDictionaryWrapper);
        string PagingMessage(RouteValueDictionaryWrapper routeValueDictionaryWrapper);
        void SetFilters(DynamicEntityMetadata dynamicEntityMetadata, IEnumerable<Func<IQueryable, IQueryable>> filters);
        void ValidatePagingParameters(RouteValueDictionaryWrapper routeValueDictionaryWrapper);
        IEnumerable GetItems(DynamicEntityMetadata dynamicEntityMetadata, RouteValueDictionaryWrapper routeValueDictionaryWrapper);
    }
}