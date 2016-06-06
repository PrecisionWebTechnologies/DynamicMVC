using DynamicMVC.UI.DynamicMVC.ViewModels.Partials;

namespace DynamicMVC.UI.DynamicMVC.ViewModels
{
    public class DynamicIndexViewModel
    {
        public DynamicIndexViewModel()
        {
            Title = "Index";
        }

        public string Title { get; set; }
        public string Header { get; set; }
        public string TypeName { get; set; }
        public string FilterMessage { get; set; }
        public RouteValueDictionaryWrapper RouteValueDictionaryWrapper { get; set; }

        public DynamicIndexFiltersViewModel DynamicIndexFiltersViewModel { get; set; }
    }
}
