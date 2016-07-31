using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;
using DynamicMVC.Data.Interfaces;
using DynamicMVC.DynamicEntityMetadataLibrary.Models;
using DynamicMVC.UI.DynamicMVC.Interfaces;
using DynamicMVC.UI.DynamicMVC.ViewModels.DynamicFilterViewModels;
using DynamicMVC.UI.DynamicMVC.ViewModels.DynamicPropertyViewModels;

namespace DynamicMVC.UI.DynamicMVC.Managers
{
    public class DynamicFilterManager : IDynamicFilterManager
    {
        private readonly IDynamicFilterFactory _dynamicFilterFactory;
        private readonly IDynamicRepository _dynamicRepository;
        private readonly IDynamicMvcManager _dynamicMvcManager;

        public DynamicFilterManager(IDynamicFilterFactory dynamicFilterFactory, IDynamicRepository dynamicRepository, IDynamicMvcManager dynamicMvcManager)
        {
            _dynamicFilterFactory = dynamicFilterFactory;
            _dynamicRepository = dynamicRepository;
            _dynamicMvcManager = dynamicMvcManager;
        }

        /// <summary>
        /// Only returns filters that are currently applied for the given routevalueDictionary
        /// </summary>
        /// <param name="dynamicEntityMetadata"></param>
        /// <param name="routeValueDictionaryWrapper"></param>
        /// <returns></returns>
        public IEnumerable<IDynamicFilter> GetDynamicFilters(DynamicEntityMetadata dynamicEntityMetadata, RouteValueDictionaryWrapper routeValueDictionaryWrapper)
        {
            var result = new List<IDynamicFilter>();
            var dynamicFilterViewModels = GetFilterPropertyViewModels(dynamicEntityMetadata, routeValueDictionaryWrapper);
            foreach (var dynamicPropertyViewModel in dynamicFilterViewModels)
            {
                var dynamicFilter = dynamicPropertyViewModel.FilterModel;
                //if (dynamicFilter.FilterIsApplied())
                    result.Add(dynamicFilter);
            }
            return result;
        }

        public IEnumerable<DynamicFilterViewModel> GetFilterPropertyViewModels(DynamicEntityMetadata dynamicEntityMetadata, RouteValueDictionaryWrapper routeValueDictionaryWrapper)
        {
            var dynamicFilterViewModels = new List<DynamicFilterViewModel>();
            //add default filters
            foreach (var dynamicPropertyMetadata in dynamicEntityMetadata.DynamicPropertyMetadatas.Where(x => !x.IsDynamicCollection))
            {
                if (dynamicPropertyMetadata.HasDynamicFilterUIAttribute)
                    continue; //explicit filters will be added later
                if (dynamicPropertyMetadata is DynamicForiegnKeyPropertyMetadata && !dynamicPropertyMetadata.ListFilterIndexHide)
                {
                    var dynamicPropertyViewModel = new DynamicFilterViewModel(dynamicPropertyMetadata);
                    var dynamicForiegnKeyPropertyMetadata = ((DynamicForiegnKeyPropertyMetadata)dynamicPropertyMetadata);
                    var dropdownType = dynamicForiegnKeyPropertyMetadata.ComplexDynamicEntityMetadata.EntityType;
                    if (_dynamicRepository.GetItemsCount(dropdownType) > _dynamicMvcManager.Options.DynamicDropDownRecordLimit)
                        dynamicPropertyViewModel.DynamicFilterViewName = "DynamicFilterAutoComplete";
                    else
                        dynamicPropertyViewModel.DynamicFilterViewName = "DynamicFilterDropDown";

                    dynamicFilterViewModels.Add(dynamicPropertyViewModel);
                }
            }
            //add explicit filters
            foreach (var dynamicPropertyMetadata in dynamicEntityMetadata.DynamicPropertyMetadatas.Where(x => x.HasDynamicFilterUIAttribute))
            {
                var dynamicFilterViewModel = new DynamicFilterViewModel(dynamicPropertyMetadata);
                var dynamicFilterUIHintAttribute = dynamicPropertyMetadata.GetDynamicFilterUIHintAttribute();
                dynamicFilterViewModel.DynamicFilterViewName = dynamicFilterUIHintAttribute.DynamicFilterViewName;
                dynamicFilterViewModels.Add(dynamicFilterViewModel);
            }
            //add models for any filters            
            foreach (var dynamicFilterViewModel in dynamicFilterViewModels)
            {
                var dynamicPropertyMetadata = dynamicEntityMetadata.DynamicPropertyMetadatas.Single(x=>x.PropertyName == dynamicFilterViewModel.PropertyName);
                var dynamicFilter = _dynamicFilterFactory.GetDynamicFilter(dynamicFilterViewModel.DynamicFilterViewName, dynamicPropertyMetadata, routeValueDictionaryWrapper);
                dynamicFilterViewModel.FilterModel = dynamicFilter;
                if (dynamicPropertyMetadata.HasDynamicFilterUIAttribute)
                {
                    dynamicFilterViewModel.FilterModel.Order = dynamicPropertyMetadata.GetDynamicFilterUIHintAttribute().Order;
                }
            }

            dynamicFilterViewModels = dynamicFilterViewModels.OrderBy(x => ((DynamicFilterBaseViewModel)x.FilterModel).Order).ToList();
            return dynamicFilterViewModels;
        }

        
        public string GetFilterMessage(DynamicEntityMetadata dynamicEntityMetadata, RouteValueDictionaryWrapper routeValueDictionaryWrapper)
        {
            var sb = new StringBuilder();
            foreach (var filter in GetDynamicFilters(dynamicEntityMetadata, routeValueDictionaryWrapper))
            {
                if (routeValueDictionaryWrapper.ContainsKey(filter.QueryStringName))
                {
                    if (sb.Length == 0)
                        sb.Append("Filtered By " + filter.PropertyName);
                    else
                    {
                        sb.Append(", " + filter.PropertyName);
                    }
                }
            }
            return sb.ToString();
        }
    
    }
}