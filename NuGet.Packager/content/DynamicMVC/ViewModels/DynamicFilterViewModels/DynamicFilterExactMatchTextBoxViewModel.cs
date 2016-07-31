using System.Collections.Generic;
using System.Linq;
using DynamicLinqExtensions;
using DynamicMVC.DynamicEntityMetadataLibrary.Models;

namespace DynamicMVC.UI.DynamicMVC.ViewModels.DynamicFilterViewModels
{
    public class DynamicFilterExactMatchTextBoxViewModel : DynamicFilterBaseViewModel
    {
        public override IQueryable Filter(IQueryable qry)
        {
            if (!string.IsNullOrWhiteSpace(FilterValue))
            {
                return qry.DynamicWhere(PropertyName, FilterValue);
            }
            return qry;
        }

        public override void ViewModelCreated(DynamicPropertyMetadata dynamicPropertyMetadata, IDictionary<string, object> controlParameters)
        {
            base.ViewModelCreated(dynamicPropertyMetadata, controlParameters);
            if (RouteValueDictionaryWrapper.ContainsKey(QueryStringName))
                FilterValue = RouteValueDictionaryWrapper.GetValue(QueryStringName).ToString();
        }

        public override string DynamicFilterViewName()
        {
            return "DynamicFilterExactMatchTextBox";
        }

        public string FilterValue { get; set; }
    }
}
