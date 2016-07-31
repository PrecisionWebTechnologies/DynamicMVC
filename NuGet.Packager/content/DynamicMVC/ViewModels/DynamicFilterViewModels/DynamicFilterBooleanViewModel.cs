using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DynamicLinqExtensions;
using DynamicMVC.DynamicEntityMetadataLibrary.Models;
using DynamicMVC.UI.DynamicMVC.Interfaces;

namespace DynamicMVC.UI.DynamicMVC.ViewModels.DynamicFilterViewModels
{
    public class DynamicFilterBooleanViewModel : DynamicFilterBaseViewModel
    {
        private readonly ISelectListItemManager _selectListItemManager;

        public DynamicFilterBooleanViewModel(ISelectListItemManager selectListItemManager)
        {
            _selectListItemManager = selectListItemManager;

        }

        public List<SelectListItem> SelectList { get; set; }

        public override string DynamicFilterViewName()
        {
            return "DynamicFilterBoolean";
        }

        public override IQueryable Filter(IQueryable qry)
        {
            if (FilterValue.HasValue)
                return qry.DynamicWhere(PropertyName, FilterValue);
            else
                return qry;
        }

        public override void ViewModelCreated(DynamicPropertyMetadata dynamicPropertyMetadata, IDictionary<string, object> controlParameters)
        {
            base.ViewModelCreated(dynamicPropertyMetadata, controlParameters);
            var nullText = "Select...";
            if (controlParameters.ContainsKey("NullText"))
            {
                nullText = controlParameters["NullText"].ToString();
            }
            if (RouteValueDictionaryWrapper.ContainsKey(QueryStringName))
            {
                var origonalValue = RouteValueDictionaryWrapper.GetValue(QueryStringName).ToString();
                //There is an issue with html.checkbox.  It sends down true,false when checked.
                if (origonalValue == "true,false")
                {
                    origonalValue = "true";
                }
                FilterValue = bool.Parse(origonalValue);
            }
            SelectList = _selectListItemManager.GetSelectListItemForBooleanDropDown(FilterValue, nullText);

        }

        public bool? FilterValue { get; set; }
    }
}