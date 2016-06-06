using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DynamicMVC.UI.DynamicMVC.ViewModels.DynamicEditorViewModels
{
    public class DynamicEditorDropDownViewModel
    {
        public DynamicEditorDropDownViewModel()
        {

        }

        public DynamicEditorDropDownViewModel(Type type, string dataTextField, string dataValueField)
            : this()
        {
            Type = type;
            DataTextField = dataTextField;
            DataValueField = dataValueField;
        }
        public Type Type { get; set; }
        public string DataTextField { get; set; }
        public string DataValueField { get; set; }
        public List<SelectListItem> SelectListItems { get; set; }
    }
}
