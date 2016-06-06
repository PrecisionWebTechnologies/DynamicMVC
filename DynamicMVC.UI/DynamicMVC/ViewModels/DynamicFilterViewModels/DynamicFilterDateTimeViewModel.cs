using System;
using System.Collections.Generic;
using System.Linq;
using DynamicLinqExtensions;
using DynamicMVC.DynamicEntityMetadataLibrary.Models;

namespace DynamicMVC.UI.DynamicMVC.ViewModels.DynamicFilterViewModels
{
    public class DynamicFilterDateTimeViewModel : DynamicFilterBaseViewModel
    {
        public override IQueryable Filter(IQueryable qry)
        {
            if (FilterValue.HasValue)
                return qry.DynamicWhereGreaterThanEqualTo(PropertyName, FilterValue.Value);

            return qry;
        }

        public override void ViewModelCreated(DynamicPropertyMetadata dynamicPropertyMetadata, IDictionary<string, object> controlParameters)
        {
            base.ViewModelCreated(dynamicPropertyMetadata, controlParameters);
            //set filtervalue if it exists in the route
            if (RouteValueDictionaryWrapper.ContainsKey(QueryStringName))
            {
                DateTime result;
                if (DateTime.TryParse(RouteValueDictionaryWrapper.GetValue(QueryStringName).ToString(), out result))
                {
                    FilterValue = result;
                }
            }
            //set filtervalue to default value if it does not exist in the route
            if (!FilterValue.HasValue && controlParameters.ContainsKey("AddDays") && controlParameters["AddDays"] is int)
            {
                var addDays = (int) controlParameters["AddDays"];
                FilterValue = DateTime.Now.AddDays(addDays);
            }
            

        }

        public override string DynamicFilterViewName()
        {
            return "DynamicFilterDateTime";
        }


        public DateTime? FilterValue { get; set; }

        
        
    }
}
