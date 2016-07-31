using System;
using System.Collections.Generic;
using System.Linq;
using DynamicLinqExtensions;
using DynamicMVC.DynamicEntityMetadataLibrary.Models;
using DynamicMVC.UI.DynamicMVC.Interfaces;

namespace DynamicMVC.UI.DynamicMVC.ViewModels.DynamicFilterViewModels
{
    public class DynamicFilterAutoCompleteViewModel : DynamicFilterBaseViewModel
    {
        private readonly IDynamicMvcManager _dynamicMvcManager;

        public DynamicFilterAutoCompleteViewModel(IDynamicMvcManager dynamicMvcManager)
        {
            _dynamicMvcManager = dynamicMvcManager;
        }

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
            if (!controlParameters.ContainsKey("Type") && !controlParameters.ContainsKey("DisplayMember") && !controlParameters.ContainsKey("ValueMember"))
            {
                //this view model was created from a dynamicforiegnKeyPropertyMetadata
                var dynamicForiegnKeyPropertyMetadata = (DynamicForiegnKeyPropertyMetadata) dynamicPropertyMetadata;
                Type = dynamicForiegnKeyPropertyMetadata.ComplexDynamicEntityMetadata.EntityTypeFunction()();
                DisplayMember = dynamicForiegnKeyPropertyMetadata.ComplexDynamicEntityMetadata.DefaultProperty().PropertyName();
                ValueMember = dynamicForiegnKeyPropertyMetadata.ComplexDynamicEntityMetadata.KeyProperty().PropertyName();

                if (LabelText == PropertyName)
                    LabelText = dynamicForiegnKeyPropertyMetadata.ComplexEntityPropertyMetadata.PropertyName();
            }
            else
            {
                Type = (Type)controlParameters["Type"];
                DisplayMember = controlParameters["DisplayMember"].ToString();
                ValueMember = controlParameters["ValueMember"].ToString();    
            }

            if (!string.IsNullOrEmpty(FilterValue))
            {
                FilterText = _dynamicMvcManager.GetSelectItemText(Type, FilterValue, DisplayMember);
            }
            else
            {
                FilterText = "";
            }
            
        }

        public override string DynamicFilterViewName()
        {
            return "DynamicFilterAutoComplete";
        }

        public string FilterValue { get; set; }
        public Type Type { get; set; }
        public string DisplayMember { get; set; }
        public string ValueMember { get; set; }
        public string FilterText { get; set; }
    }
}
