using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using DynamicMVC.UI.DynamicMVC.Interfaces;

namespace DynamicMVC.UI.DynamicMVC.Managers
{
    public class SelectListItemManager : ISelectListItemManager
    {
        private readonly IDynamicMvcManager _dynamicMvcManager;

        public SelectListItemManager(IDynamicMvcManager dynamicMvcManager)
        {
            _dynamicMvcManager = dynamicMvcManager;
        }

        public List<SelectListItem> GetSelectListItemForBooleanDropDown(bool? value, string nullText)
        {
            var list = new List<SelectListItem>();
            list.Add(new SelectListItem { Selected = !value.HasValue, Text = nullText, Value = string.Empty });
            list.Add(new SelectListItem { Selected = value.HasValue && value.Value==true, Text = "True", Value = "true" });
            list.Add(new SelectListItem { Selected = value.HasValue && value.Value==false, Text = "False", Value = "false" });
            return list;
        }

        public List<SelectListItem> GetSelectListItems(Type type, string valueFieldName, string textFieldName, object selectedItem = null, string nullText = null)
        {
            var selectListItems = new List<SelectListItem>();
            var valueProperty = type.GetProperties().Single(x => x.Name == valueFieldName);
            var textProperty = type.GetProperties().Single(x => x.Name == textFieldName);
            if (selectedItem == null)
            {
                if (nullText != null)
                    selectListItems.Add(new SelectListItem { Selected = true, Text = nullText, Value = string.Empty });
                foreach (var item in _dynamicMvcManager.GetItemsByTypeFunction(type))
                {
                    var currentValue = valueProperty.GetValue(item).ToString();
                    var currentText = textProperty.GetValue(item).ToString();
                    selectListItems.Add(new SelectListItem
                    {
                        Selected = false,
                        Text = currentText,
                        Value = currentValue
                    });
                }
            }
            else
            {
                if (nullText != null)
                    selectListItems.Add(new SelectListItem { Selected = false, Text = nullText, Value = string.Empty });
                foreach (var item in _dynamicMvcManager.GetItemsByTypeFunction(type))
                {
                    var currentValue = valueProperty.GetValue(item).ToString();
                    var currentText = textProperty.GetValue(item).ToString();
                    selectListItems.Add(new SelectListItem
                    {
                        Selected = currentValue.ToString(CultureInfo.InvariantCulture) == selectedItem.ToString(),
                        Text = currentText,
                        Value = currentValue
                    });
                }
            }

            return selectListItems;
        }

    }
}