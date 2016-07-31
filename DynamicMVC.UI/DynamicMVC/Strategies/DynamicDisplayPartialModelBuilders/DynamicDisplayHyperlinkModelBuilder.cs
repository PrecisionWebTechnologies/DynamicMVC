using System;
using DynamicMVC.DynamicEntityMetadataLibrary.Models;
using DynamicMVC.UI.DynamicMVC.Interfaces;
using DynamicMVC.UI.DynamicMVC.ViewModels.DynamicEditorViewModels;
using DynamicMVC.UI.DynamicMVC.ViewModels.DynamicPropertyViewModels;

namespace DynamicMVC.UI.DynamicMVC.Strategies.DynamicDisplayPartialModelBuilders
{
    public class DynamicDisplayHyperlinkModelBuilder : IDynamicDisplayPartialModelBuilder
    {
        public string DynamicDisplayPartialName()
        {
            return "_DynamicDisplayHyperlink";
        }

        public void Build(DynamicPropertyMetadata dynamicPropertyMetadata, DynamicPropertyIndexViewModel dynamicPropertyIndexViewModel, dynamic item)
        {
            if (dynamicPropertyMetadata.IsDynamicCollection())
            {
                //view model instructions for collection with dynamicdisplayhyperlink
                var dynamicHyperlinkViewModel = new DynamicEditorHyperlinkViewModel("View " + dynamicPropertyMetadata.DisplayName(), dynamicPropertyMetadata.CollectionItemTypeName(), "Index");
                ////ToDo: Need to review this.  It assumes FK follows a certain naming convention.  FK must have name of related type
                var fkPropertyName = ((DynamicCollectionEntityPropertyMetadata)dynamicPropertyMetadata).ForiegnKeyPropertyName;
                dynamicHyperlinkViewModel.RouteValueDictionaryWrapper.SetValue(fkPropertyName, dynamicPropertyMetadata.DynamicEntityMetadata.KeyProperty().GetValueFunction()(item));
                dynamicPropertyIndexViewModel.DynamicEditorHyperlinkViewModel = dynamicHyperlinkViewModel;
            }
            else if (dynamicPropertyMetadata is DynamicForiegnKeyPropertyMetadata)
            {
                var dataIsNull = false;
                var dynamicForiegnKeyPropertyMetadata = ((DynamicForiegnKeyPropertyMetadata)dynamicPropertyMetadata);
                var dynamicEntity = dynamicForiegnKeyPropertyMetadata.ComplexEntityPropertyMetadata.GetValueFunction()(item);
                if (dynamicEntity == null)
                {
                    if (dynamicForiegnKeyPropertyMetadata.IsNullableType())
                    {
                        dataIsNull = true;
                    }
                    else
                    {
                        throw new Exception("DynamicMVC cannot display the default value for " + dynamicPropertyMetadata.PropertyName() + " because the DynamicEntity is not loaded.  Please make sure you are loading the " + dynamicPropertyMetadata.PropertyName() + " in your query.");
                    }
                }
                if (dataIsNull)
                {
                    dynamicPropertyIndexViewModel.DynamicEditorHyperlinkViewModel = null;
                }
                else
                {
                    var displayValue = ((DynamicForiegnKeyPropertyMetadata)dynamicPropertyMetadata).ComplexDynamicEntityMetadata.DefaultProperty().GetValueFunction()(dynamicEntity);
                    var dynamicHyperlinkViewModel = new DynamicEditorHyperlinkViewModel(displayValue.ToString(), ((DynamicForiegnKeyPropertyMetadata)dynamicPropertyMetadata).ComplexDynamicEntityMetadata.TypeName(), "Details");
                    dynamicHyperlinkViewModel.RouteValueDictionaryWrapper.SetValue("Id", ((DynamicForiegnKeyPropertyMetadata)dynamicPropertyMetadata).ComplexDynamicEntityMetadata.KeyProperty().GetValueFunction()(dynamicEntity));
                    dynamicPropertyIndexViewModel.DynamicEditorHyperlinkViewModel = dynamicHyperlinkViewModel;
                }


                dynamicPropertyIndexViewModel.DisplayName = ((DynamicForiegnKeyPropertyMetadata)dynamicPropertyMetadata).ComplexEntityPropertyMetadata.PropertyName();
            }
        }
    }
}