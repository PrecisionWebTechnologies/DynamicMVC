using System;
using System.Collections.Generic;
using System.Linq;
using DynamicLinqExtensions;
using DynamicMVC.DynamicEntityMetadataLibrary.Models;

namespace DynamicMVC.UI.DynamicMVC.ViewModels.DynamicFilterViewModels
{
    public class DynamicFilterDateRangeViewModel : DynamicFilterBaseViewModel
    {
        public override IQueryable Filter(IQueryable qry)
        {
            if (StartFilterValue.HasValue && EndFilterValue.HasValue)
                return qry.DynamicWhereGreaterThanEqualTo(PropertyName, StartFilterValue.Value)
                    .DynamicWhereLessThanEqualTo(PropertyName, EndFilterValue.Value);
            return qry;
        }

        public override void ViewModelCreated(DynamicPropertyMetadata dynamicPropertyMetadata, IDictionary<string, object> controlParameters)
        {
            base.ViewModelCreated(dynamicPropertyMetadata, controlParameters);
            StartLabelText = controlParameters["StartLabel"].ToString();
            EndLabelText = controlParameters["EndLabel"].ToString();
            StartQueryStringName = controlParameters["StartQueryStringName"].ToString();
            EndQueryStringName = controlParameters["EndQueryStringName"].ToString();

            //set filtervalue if it exists in the route
            if (RouteValueDictionaryWrapper.ContainsKey(StartQueryStringName))
            {
                DateTime result;
                if (DateTime.TryParse(RouteValueDictionaryWrapper.GetValue(StartQueryStringName).ToString(), out result))
                {
                    StartFilterValue = result;
                }
            }
            //set filtervalue to default value if it does not exist in the route
            if (!StartFilterValue.HasValue && controlParameters.ContainsKey("AddDays") && controlParameters["AddDays"] is int)
            {
                var addDays = (int)controlParameters["AddDays"];
                StartFilterValue = DateTime.Now.AddDays(addDays);
            }

            //set filtervalue if it exists in the route
            if (RouteValueDictionaryWrapper.ContainsKey(EndQueryStringName))
            {
                DateTime result;
                if (DateTime.TryParse(RouteValueDictionaryWrapper.GetValue(EndQueryStringName).ToString(), out result))
                {
                    EndFilterValue = result;
                }
            }
            if (!EndFilterValue.HasValue)
                EndFilterValue = DateTime.Now;
        }

        public override string DynamicFilterViewName()
        {
            return "DynamicFilterDateRange";
        }

        public override bool FilterIsApplied()
        {
            return RouteValueDictionaryWrapper.ContainsKey(StartQueryStringName) && RouteValueDictionaryWrapper.ContainsKey(EndQueryStringName);
        }

        public DateTime? StartFilterValue { get; set; }
        public DateTime? EndFilterValue { get; set; }
        public string StartLabelText { get; set; }
        public string EndLabelText { get; set; }
        public string StartQueryStringName { get; set; }
        public string EndQueryStringName { get; set; }

    }
}
