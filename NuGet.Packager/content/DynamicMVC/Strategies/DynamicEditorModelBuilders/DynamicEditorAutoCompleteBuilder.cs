using System;
using System.Linq;
using DynamicMVC.DynamicEntityMetadataLibrary.Models;
using DynamicMVC.UI.DynamicMVC.Interfaces;
using DynamicMVC.UI.DynamicMVC.ViewModels.DynamicEditorViewModels;
using DynamicMVC.UI.DynamicMVC.ViewModels.DynamicPropertyViewModels;

#pragma warning disable 1591

namespace DynamicMVC.UI.DynamicMVC.Strategies.DynamicEditorModelBuilders
{
    public class DynamicEditorAutoCompleteBuilder : IDynamicEditorModelBuilder
    {
        private readonly IDynamicMvcManager _dynamicMVCManager;

        public DynamicEditorAutoCompleteBuilder(IDynamicMvcManager dynamicMvcManager)
        {
            _dynamicMVCManager = dynamicMvcManager;
        }

        public string DynamicEditorName()
        {
            return "DynamicEditorAutoComplete";
        }

        public void Build(DynamicPropertyMetadata dynamicPropertyMetadata, DynamicPropertyEditorViewModel dynamicPropertyViewModel, dynamic item)
        {
            var dynamicForiegnKeyPropertyMetadata = ((DynamicForiegnKeyPropertyMetadata)dynamicPropertyMetadata);
            var dynamicEditorAutoCompleteViewModel = new DynamicEditorAutoCompleteViewModel();
            var type = dynamicForiegnKeyPropertyMetadata.ComplexDynamicEntityMetadata.EntityType;
            var dataTextField = dynamicForiegnKeyPropertyMetadata.ComplexDynamicEntityMetadata.DefaultProperty.PropertyName;
            dynamicEditorAutoCompleteViewModel.TypeName = type.Name;

            var value = dynamicPropertyMetadata.ReflectedProperty.ReflectedPropertyOperations.GetValueFunction(item);
            dynamicEditorAutoCompleteViewModel.SelectedText = GetSelectItemText(type, value, dataTextField);
            dynamicPropertyViewModel.DynamicEditorAutoCompleteViewModel = dynamicEditorAutoCompleteViewModel;

            dynamicPropertyViewModel.DisplayName = dynamicForiegnKeyPropertyMetadata.ComplexEntityPropertyMetadata.PropertyName;
        }

        public string GetSelectItemText(Type type, dynamic value, string textFieldName)
        {
            var textProperty = type.GetProperties().Single(x => x.Name == textFieldName);
            var v = value.ToString();
            var item = _dynamicMVCManager.GetItemByTypeAndKeyFunction(type, v);
            if (item == null)
                return "";
            return textProperty.GetValue(item).ToString();
        }
    }
}
