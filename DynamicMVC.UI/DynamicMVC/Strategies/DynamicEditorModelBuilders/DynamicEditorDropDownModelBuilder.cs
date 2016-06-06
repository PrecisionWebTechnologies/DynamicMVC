using DynamicMVC.DynamicEntityMetadataLibrary.Models;
using DynamicMVC.UI.DynamicMVC.Interfaces;
using DynamicMVC.UI.DynamicMVC.ViewModels.DynamicEditorViewModels;
using DynamicMVC.UI.DynamicMVC.ViewModels.DynamicPropertyViewModels;

namespace DynamicMVC.UI.DynamicMVC.Strategies.DynamicEditorModelBuilders
{
    public class DynamicEditorDropDownModelBuilder : IDynamicEditorModelBuilder
    {
        private readonly ISelectListItemManager _selectListItemManager;

        public DynamicEditorDropDownModelBuilder(ISelectListItemManager selectListItemManager)
        {
            _selectListItemManager = selectListItemManager;
        }

        public string DynamicEditorName()
        {
            return "DynamicEditorDropDown";
        }

        public void Build(DynamicPropertyMetadata dynamicPropertyMetadata, DynamicPropertyEditorViewModel dynamicPropertyViewModel, dynamic item)
        {
            var dynamicForiegnKeyPropertyMetadata = ((DynamicForiegnKeyPropertyMetadata)dynamicPropertyMetadata);
            var type = dynamicForiegnKeyPropertyMetadata.ComplexDynamicEntityMetadata.EntityType;
            var dataTextField = dynamicForiegnKeyPropertyMetadata.ComplexDynamicEntityMetadata.DefaultProperty.PropertyName;
            var dataValueField = dynamicForiegnKeyPropertyMetadata.ComplexDynamicEntityMetadata.KeyProperty.PropertyName;
            var dynamicEditorDropDownViewModel = new DynamicEditorDropDownViewModel(dynamicForiegnKeyPropertyMetadata.ComplexDynamicEntityMetadata.EntityType, dynamicForiegnKeyPropertyMetadata.ComplexDynamicEntityMetadata.DefaultProperty.PropertyName, dynamicForiegnKeyPropertyMetadata.ComplexDynamicEntityMetadata.KeyProperty.PropertyName);
            var value = dynamicPropertyMetadata.GetValueFunction(item);
            
            dynamicEditorDropDownViewModel.SelectListItems = _selectListItemManager.GetSelectListItems(type, dataValueField, dataTextField, value);

            dynamicPropertyViewModel.DynamicEditorDropDownViewModel = dynamicEditorDropDownViewModel;

            dynamicPropertyViewModel.DisplayName = dynamicForiegnKeyPropertyMetadata.ComplexEntityPropertyMetadata.PropertyName;
        }

       
    }
}