using System.Collections.Generic;
using DynamicMVC.UI.DynamicMVC.ViewModels.DynamicControls;
using DynamicMVC.UI.DynamicMVC.ViewModels.DynamicPropertyViewModels;

namespace DynamicMVC.UI.DynamicMVC.ViewModels
{
    public class DynamicIndexPageViewModel
    {
        public DynamicIndexPageViewModel()
        {
            CreateNewLinkText = "Create New";
            DynamicIndexItemViewModels = new List<DynamicIndexItemViewModel>();
            DynamicIndexMobileItemViewModels = new List<DynamicIndexMobileItemViewModel>();
            DynamicTableHeaderViewModels = new List<DynamicTableHeaderViewModel>();
        }

        public bool ShowCreate { get; set; }
        public string TypeName { get; set; }
        public RouteValueDictionaryWrapper RouteValueDictionaryWrapper { get; set; }
        public bool AllowSort { get; set; }
        public string DefaultPropertyName { get; set; }
        public string DefaultPropertySortExpression { get; set; }

        public string CreateNewLinkText { get; set; }
        public string NextClassName { get; set; }
        public string PreviousClassName { get; set; }
        public string PagingMessage { get; set; }
        public bool ShowEdit { get; set; }
        public bool ShowDelete { get; set; }
        public bool ShowDetails { get; set; }

        public List<DynamicPropertyIndexViewModel> DynamicPropertyIndexViewModels { get; set; }
        public List<DynamicIndexItemViewModel> DynamicIndexItemViewModels { get; set; }
        public List<DynamicIndexMobileItemViewModel> DynamicIndexMobileItemViewModels { get; set; }
        public List<DynamicTableHeaderViewModel> DynamicTableHeaderViewModels { get; set; }
    }
}
