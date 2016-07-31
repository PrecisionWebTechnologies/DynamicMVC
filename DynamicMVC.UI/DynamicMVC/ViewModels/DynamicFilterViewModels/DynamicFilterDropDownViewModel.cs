using System;
using System.Collections.Generic;
using System.Web.Mvc;
using DynamicMVC.DynamicEntityMetadataLibrary.Models;
using DynamicMVC.UI.DynamicMVC.Interfaces;

namespace DynamicMVC.UI.DynamicMVC.ViewModels.DynamicFilterViewModels
{
    public class DynamicFilterDropDownViewModel : DynamicFilterBaseViewModel
    {
        private readonly ISelectListItemManager _selectListItemManager;

        public DynamicFilterDropDownViewModel(ISelectListItemManager selectListItemManager)
        {
            _selectListItemManager = selectListItemManager;
        }
        public Type Type { get; set; }
        public string DataTextField { get; set; }
        public string DataValueField { get; set; }
        public string SelectedValue { get; set; }
        public string NullText { get; set; }
        public List<SelectListItem> SelectList { get; set; }

        public override void ViewModelCreated(DynamicPropertyMetadata dynamicPropertyMetadata, IDictionary<string, object> controlParameters)
        {
            base.ViewModelCreated(dynamicPropertyMetadata, controlParameters);
            var dynamicForiegnKeyPropertyMetadata = (DynamicForiegnKeyPropertyMetadata)dynamicPropertyMetadata;

            string selectedValue = "";
            if (RouteValueDictionaryWrapper.ContainsKey(dynamicPropertyMetadata.PropertyName()))
                selectedValue = RouteValueDictionaryWrapper.GetValue(dynamicPropertyMetadata.PropertyName()).ToString();

            if (LabelText == PropertyName)
                LabelText = dynamicForiegnKeyPropertyMetadata.ComplexEntityPropertyMetadata.PropertyName();

            Type = dynamicForiegnKeyPropertyMetadata.ComplexDynamicEntityMetadata.EntityTypeFunction()();
            DataTextField = dynamicForiegnKeyPropertyMetadata.ComplexDynamicEntityMetadata.DefaultProperty().PropertyName();
            DataValueField = dynamicForiegnKeyPropertyMetadata.ComplexDynamicEntityMetadata.KeyProperty().PropertyName();
            if (controlParameters.ContainsKey("NullText"))
                NullText = controlParameters["NullText"].ToString();
            else
                NullText = "Select Item";
            SelectedValue = selectedValue;

            SelectList = _selectListItemManager.GetSelectListItems(Type, DataValueField, DataTextField, SelectedValue, NullText);
        }

        public override string DynamicFilterViewName()
        {
            return "DynamicFilterDropDown";
        }
    }
}
