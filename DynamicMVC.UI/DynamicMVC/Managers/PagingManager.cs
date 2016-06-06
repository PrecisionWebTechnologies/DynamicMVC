using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using DynamicMVC.Data.Interfaces;
using DynamicMVC.DynamicEntityMetadataLibrary.Models;
using DynamicMVC.UI.DynamicMVC.Interfaces;

namespace DynamicMVC.UI.DynamicMVC.Managers
{
    public class PagingManager : IPagingManager
    {
        private readonly IDynamicRepository _dynamicRepository;
        private readonly IRequestManager _requestManager;

        public PagingManager(IDynamicRepository dynamicRepository, IRequestManager requestManager)
        {
            _dynamicRepository = dynamicRepository;
            _requestManager = requestManager;
        }

        private long RecordCount { get; set; }
        private IEnumerable<Func<IQueryable, IQueryable>> Filters { get; set; }

        public void SetFilters(DynamicEntityMetadata dynamicEntityMetadata, IEnumerable<Func<IQueryable, IQueryable>> filters)
        {
            // ReSharper disable once PossibleMultipleEnumeration
            Filters = filters.ToList();
            // ReSharper disable once PossibleMultipleEnumeration
            RecordCount = _dynamicRepository.GetRecordCount(dynamicEntityMetadata.EntityType, filters);
        }

        public void ValidatePagingParameters(RouteValueDictionaryWrapper routeValueDictionaryWrapper)
        {
            var page = int.Parse(routeValueDictionaryWrapper.GetValue("Page").ToString());
            if (RecordStart(routeValueDictionaryWrapper) > RecordEnd(routeValueDictionaryWrapper) & page > 1)
            {
                routeValueDictionaryWrapper.SetValue("Page", page - 1);
            }
        }

        public IEnumerable GetItems(DynamicEntityMetadata dynamicEntityMetadata, RouteValueDictionaryWrapper routeValueDictionaryWrapper)
        {
            var page = int.Parse(routeValueDictionaryWrapper.GetValue("Page").ToString());
            var pageSize = int.Parse(routeValueDictionaryWrapper.GetValue("PageSize").ToString());
            return _dynamicRepository.GetItems(dynamicEntityMetadata.EntityType, Filters, page, pageSize, _requestManager.OrderBy(), dynamicEntityMetadata.ListIncludes.ToArray());
        }
        public string PreviousClassName(RouteValueDictionaryWrapper routeValueDictionaryWrapper)
        {
            return int.Parse(routeValueDictionaryWrapper.GetValue("Page").ToString()) == 1 ? "previous disabled" : "previous";
        }

        public string NextClassName(RouteValueDictionaryWrapper routeValueDictionaryWrapper)
        {
            return RecordCount == RecordEnd(routeValueDictionaryWrapper) ? "next disabled" : "next";
        }

        private long RecordStart(RouteValueDictionaryWrapper routeValueDictionaryWrapper)
        {
            if (RecordCount == 0)
                return 0;
            var page = int.Parse(routeValueDictionaryWrapper.GetValue("Page").ToString());
            var pageSize = int.Parse(routeValueDictionaryWrapper.GetValue("PageSize").ToString());
            return page * pageSize - pageSize + 1;
        }

        private long RecordEnd(RouteValueDictionaryWrapper routeValueDictionaryWrapper)
        {
            var page = int.Parse(routeValueDictionaryWrapper.GetValue("Page").ToString());
            var pageSize = int.Parse(routeValueDictionaryWrapper.GetValue("PageSize").ToString());
            var result = page * (long)pageSize;
            if (result > RecordCount)
                result = RecordCount;
            return result;
        }

        public string PagingMessage(RouteValueDictionaryWrapper routeValueDictionaryWrapper)
        {
            return string.Format("Showing {0} - {1} of {2}"
                , RecordStart(routeValueDictionaryWrapper).ToString(CultureInfo.InvariantCulture)
                , RecordEnd(routeValueDictionaryWrapper).ToString(CultureInfo.InvariantCulture)
                , RecordCount.ToString(CultureInfo.InvariantCulture));
        }
    }
}