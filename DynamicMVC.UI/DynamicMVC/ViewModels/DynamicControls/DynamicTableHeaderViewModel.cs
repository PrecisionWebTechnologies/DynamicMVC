using System.Web.Routing;
using DynamicMVC.UI.DynamicMVC.Interfaces;
using DynamicMVC.UI.DynamicMVC.ViewModels.DynamicPropertyViewModels;

#pragma warning disable 1591

namespace DynamicMVC.UI.DynamicMVC.ViewModels.DynamicControls
{
    public class DynamicTableHeaderViewModel : IDynamicDisplayName
    {
        public DynamicTableHeaderViewModel(DynamicPropertyIndexViewModel dynamicPropertyIndexViewModel, RouteValueDictionaryWrapper routeValueDictionaryWrapper, string typeName)
        {
            DisplayName = dynamicPropertyIndexViewModel.DisplayName;
            SortExpression = dynamicPropertyIndexViewModel.SortExpression;
            AllowSort = dynamicPropertyIndexViewModel.AllowSort;
            TypeName = typeName;
            RouteValueDictionaryWrapper = routeValueDictionaryWrapper;
            ViewModelPropertyName = dynamicPropertyIndexViewModel.ViewModelPropertyName;
            PropertyName = dynamicPropertyIndexViewModel.PropertyName;

        }
        public string DisplayName { get; set; }
        public string SortExpression { get; set; }
        public bool AllowSort { get; set; }
        public string TypeName { get; set; }
        public RouteValueDictionaryWrapper RouteValueDictionaryWrapper { get; set; }
        public string ViewModelPropertyName { get; set; }
        public string PropertyName { get; set; }
    }
}
