using System.Collections.Generic;
using System.Linq;
using DynamicMVC.UI.DynamicMVC.ViewModels.DynamicPropertyViewModels;

#pragma warning disable 1591

namespace DynamicMVC.UI.DynamicMVC.ViewModels.Partials
{
    public class DynamicIndexFiltersViewModel
    {
        public DynamicIndexFiltersViewModel()
        {
            FilterTitle = "Filter";
            SearchButtonText = "Search";
            FilterPropertyViewModels = new HashSet<DynamicFilterViewModel>();
        }

        public DynamicIndexFiltersViewModel(string typeName, RouteValueDictionaryWrapper routeValueDictionaryWrapper, IEnumerable<DynamicFilterViewModel> dynamicFilterViewModels) : this()
        {
            TypeName = typeName;
            RouteValueDictionaryWrapper = routeValueDictionaryWrapper;
            FilterPropertyViewModels = dynamicFilterViewModels.ToList();
            ShowSearch = FilterPropertyViewModels.Any();
        }

        public bool ShowSearch { get; set; }
        public string SearchButtonText { get; set; }
        public RouteValueDictionaryWrapper RouteValueDictionaryWrapper { get; set; }
        public string FilterTitle { get; set; }
        public string TypeName { get; set; }

        public ICollection<DynamicFilterViewModel> FilterPropertyViewModels { get; set; }
    }
}
