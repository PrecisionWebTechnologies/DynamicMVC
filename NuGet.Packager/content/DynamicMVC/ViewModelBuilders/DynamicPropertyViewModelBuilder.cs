using System.ComponentModel.DataAnnotations;
using DynamicMVC.Data.Interfaces;
using DynamicMVC.DynamicEntityMetadataLibrary.Models;
using DynamicMVC.Shared.Enums;
using DynamicMVC.UI.DynamicMVC.Interfaces;
using DynamicMVC.UI.DynamicMVC.ViewModels.DynamicPropertyViewModels;
using ReflectionLibrary.Enums;

namespace DynamicMVC.UI.DynamicMVC.ViewModelBuilders
{
    public class DynamicPropertyViewModelBuilder : IDynamicPropertyViewModelBuilder
    {
        private readonly IDynamicMvcManager _dynamicMvcManager;
        private readonly IDynamicRepository _dynamicRepository;

        public DynamicPropertyViewModelBuilder(IDynamicMvcManager dynamicMvcManager, IDynamicRepository dynamicRepository)
        {
            _dynamicMvcManager = dynamicMvcManager;
            _dynamicRepository = dynamicRepository;
        }

        public DynamicPropertyIndexViewModel BuildDynamicPropertyIndexViewModel(DynamicPropertyMetadata dynamicPropertyMetadata)
        {
            var dynamicPropertyIndexViewModel = new DynamicPropertyIndexViewModel(dynamicPropertyMetadata);
            if (dynamicPropertyMetadata.DataType == DataType.Password)
                return null;
            dynamicPropertyIndexViewModel.AllowSort = dynamicPropertyMetadata.AllowSort;

            if (dynamicPropertyMetadata is DynamicForiegnKeyPropertyMetadata)
            {
                dynamicPropertyIndexViewModel.ViewModelPropertyName = "Item." + dynamicPropertyMetadata.PropertyName;
                //ToDo:  This logic could probably be pushed back to a lower level
                if (dynamicPropertyMetadata.DisplayName == dynamicPropertyMetadata.PropertyName)
                    dynamicPropertyIndexViewModel.DisplayName = ((DynamicForiegnKeyPropertyMetadata)dynamicPropertyMetadata).ComplexEntityPropertyMetadata.PropertyName;

                dynamicPropertyIndexViewModel.SortExpression = ((DynamicForiegnKeyPropertyMetadata)dynamicPropertyMetadata).ComplexEntityPropertyMetadata.PropertyName + "." + ((DynamicForiegnKeyPropertyMetadata)dynamicPropertyMetadata).ComplexDynamicEntityMetadata.DefaultProperty.PropertyName;
                dynamicPropertyIndexViewModel.PartialViewName = "_DynamicDisplayHyperlink";
            }
            else if (dynamicPropertyMetadata.IsSimple)
            {
                dynamicPropertyIndexViewModel.ViewModelPropertyName = "Item." + dynamicPropertyMetadata.PropertyName;
                dynamicPropertyIndexViewModel.PartialViewName = "_DynamicDisplay";
            }
            else if (dynamicPropertyMetadata.IsDynamicCollection)
            {
                dynamicPropertyIndexViewModel.ViewModelPropertyName = "Item." + dynamicPropertyMetadata.PropertyName;
                dynamicPropertyIndexViewModel.PartialViewName = "_DynamicDisplayHyperlink";
            }
            return dynamicPropertyIndexViewModel;
        }

        public DynamicPropertyEditorViewModel BuildDynamicPropertyEditorViewModelForEdit(DynamicPropertyMetadata dynamicPropertyMetadata)
        {
            var dynamicPropertyViewModel = new DynamicPropertyEditorViewModel(dynamicPropertyMetadata);
            dynamicPropertyViewModel.ViewModelPropertyName = "Item." + dynamicPropertyMetadata.PropertyName;

            //assign editor
            if (!string.IsNullOrWhiteSpace(dynamicPropertyMetadata.UIHint))
                dynamicPropertyViewModel.DynamicEditorName = dynamicPropertyMetadata.UIHint;
            else if (dynamicPropertyMetadata.DataType == DataType.MultilineText)
                dynamicPropertyViewModel.DynamicEditorName = "DynamicEditorMultiLine";
            else if (dynamicPropertyMetadata.DataType == DataType.Password)
                dynamicPropertyViewModel.DynamicEditorName = "DynamicEditorPassword";
            else if (dynamicPropertyMetadata is DynamicForiegnKeyPropertyMetadata)
            {
                var dynamicForiegnKeyPropertyMetadata = ((DynamicForiegnKeyPropertyMetadata)dynamicPropertyMetadata);
                var dropdownType = dynamicForiegnKeyPropertyMetadata.ComplexDynamicEntityMetadata.EntityType;
                if (_dynamicRepository.GetItemsCount(dropdownType) > _dynamicMvcManager.Options.DynamicDropDownRecordLimit)
                    dynamicPropertyViewModel.DynamicEditorName = "DynamicEditorAutoComplete";
                else
                    dynamicPropertyViewModel.DynamicEditorName = "DynamicEditorDropDown";
            }
            else if (dynamicPropertyMetadata.SimpleTypeEnum == SimpleTypeEnum.DateTime)
                dynamicPropertyViewModel.DynamicEditorName = "DynamicEditorDateTime";
            else if (dynamicPropertyMetadata.SimpleTypeEnum == SimpleTypeEnum.DateTimeNullable)
                dynamicPropertyViewModel.DynamicEditorName = "DynamicEditorDateTime";
            else if (dynamicPropertyMetadata.SimpleTypeEnum == SimpleTypeEnum.Bool)
                dynamicPropertyViewModel.DynamicEditorName = "DynamicEditorBool";
            else if (dynamicPropertyMetadata.SimpleTypeEnum == SimpleTypeEnum.BoolNullable)
                dynamicPropertyViewModel.DynamicEditorName = "DynamicEditorBoolNullable";
            else
                dynamicPropertyViewModel.DynamicEditorName = "DynamicEditor";

            return dynamicPropertyViewModel;
        }

        public DynamicPropertyEditorViewModel BuildDynamicPropertyEditorViewModelForCreate(DynamicPropertyMetadata dynamicPropertyMetadata)
        {
            var dynamicPropertyEditorViewModel = new DynamicPropertyEditorViewModel(dynamicPropertyMetadata);
            dynamicPropertyEditorViewModel.ViewModelPropertyName = "Item." + dynamicPropertyMetadata.PropertyName;

            //assign editor
            if (!string.IsNullOrWhiteSpace(dynamicPropertyMetadata.UIHint))
                dynamicPropertyEditorViewModel.DynamicEditorName = dynamicPropertyMetadata.UIHint;
            else if (dynamicPropertyMetadata.DataType == DataType.MultilineText)
                dynamicPropertyEditorViewModel.DynamicEditorName = "DynamicEditorMultiLine";
            else if (dynamicPropertyMetadata.DataType == DataType.Password)
                dynamicPropertyEditorViewModel.DynamicEditorName = "DynamicEditorPassword";
            else if (dynamicPropertyMetadata is DynamicForiegnKeyPropertyMetadata)
            {
                var dynamicForiegnKeyPropertyMetadata = ((DynamicForiegnKeyPropertyMetadata)dynamicPropertyMetadata);
                var dropdownType = dynamicForiegnKeyPropertyMetadata.ComplexDynamicEntityMetadata.EntityType;
                if (_dynamicRepository.GetItemsCount(dropdownType) > _dynamicMvcManager.Options.DynamicDropDownRecordLimit)
                    dynamicPropertyEditorViewModel.DynamicEditorName = "DynamicEditorAutoComplete";
                else
                    dynamicPropertyEditorViewModel.DynamicEditorName = "DynamicEditorDropDown";
            }
            else if (dynamicPropertyMetadata.SimpleTypeEnum == SimpleTypeEnum.DateTime)
                dynamicPropertyEditorViewModel.DynamicEditorName = "DynamicEditorDateTime";
            else if (dynamicPropertyMetadata.SimpleTypeEnum == SimpleTypeEnum.DateTimeNullable)
                dynamicPropertyEditorViewModel.DynamicEditorName = "DynamicEditorDateTime";
            else if (dynamicPropertyMetadata.SimpleTypeEnum == SimpleTypeEnum.Bool)
                dynamicPropertyEditorViewModel.DynamicEditorName = "DynamicEditorBool";
            else if (dynamicPropertyMetadata.SimpleTypeEnum == SimpleTypeEnum.BoolNullable)
                dynamicPropertyEditorViewModel.DynamicEditorName = "DynamicEditorBoolNullable";
            else
                dynamicPropertyEditorViewModel.DynamicEditorName = "DynamicEditor";

            return dynamicPropertyEditorViewModel;
        }

        public DynamicPropertyEditorViewModel BuildDynamicPropertyEditorViewModelForDetails(DynamicPropertyMetadata dynamicPropertyMetadata)
        {
            var dynamicPropertyEditorViewModel = new DynamicPropertyEditorViewModel(dynamicPropertyMetadata);
            dynamicPropertyEditorViewModel.ViewModelPropertyName = "Item." + dynamicPropertyMetadata.PropertyName;

            //assign editor
            if (!string.IsNullOrWhiteSpace(dynamicPropertyMetadata.UIHint))
                dynamicPropertyEditorViewModel.DynamicEditorName = dynamicPropertyMetadata.UIHint;
            else if (dynamicPropertyMetadata.DataType == DataType.MultilineText)
                dynamicPropertyEditorViewModel.DynamicEditorName = "DynamicEditorMultiLineReadonly";
            else if (dynamicPropertyMetadata.DataType == DataType.Password)
                return null;
            else if (dynamicPropertyMetadata is DynamicForiegnKeyPropertyMetadata)
                dynamicPropertyEditorViewModel.DynamicEditorName = "DynamicEditorHyperlink";
            else if (dynamicPropertyMetadata.IsSimple)
                dynamicPropertyEditorViewModel.DynamicEditorName = "DynamicEditorReadOnly";
            else if (dynamicPropertyMetadata.IsDynamicCollection)
                dynamicPropertyEditorViewModel.DynamicEditorName = "DynamicEditorHyperlink";
            else
                dynamicPropertyEditorViewModel.DynamicEditorName = "DynamicEditor";

            return dynamicPropertyEditorViewModel;
        }
    }
}