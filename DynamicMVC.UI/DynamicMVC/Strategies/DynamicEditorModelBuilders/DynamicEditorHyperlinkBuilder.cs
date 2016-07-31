using System;
using DynamicMVC.DynamicEntityMetadataLibrary.Models;
using DynamicMVC.UI.DynamicMVC.Interfaces;
using DynamicMVC.UI.DynamicMVC.ViewModels.DynamicEditorViewModels;
using DynamicMVC.UI.DynamicMVC.ViewModels.DynamicPropertyViewModels;

namespace DynamicMVC.UI.DynamicMVC.Strategies.DynamicEditorModelBuilders
{
    public class DynamicEditorHyperlinkBuilder : IDynamicEditorModelBuilder
    {
        public string DynamicEditorName()
        {
            return "DynamicEditorHyperlink";
        }

        public void Build(DynamicPropertyMetadata dynamicPropertyMetadata, DynamicPropertyEditorViewModel dynamicPropertyViewModel, dynamic item)
        {
            if (dynamicPropertyMetadata is DynamicForiegnKeyPropertyMetadata)
            {
                var dynamicEntity = ((DynamicForiegnKeyPropertyMetadata)dynamicPropertyMetadata).ComplexEntityPropertyMetadata.GetValueFunction()(item);
                var showHyperlink = true;
                if (dynamicEntity == null)
                {
                    if (dynamicPropertyMetadata.IsNullableType())
                    {
                        showHyperlink = false;
                    }
                    else
                    {
                        throw new Exception("DynamicMVC cannot display the default value for " + dynamicPropertyMetadata.PropertyName() + " because the DynamicEntity is not loaded.  Please make sure you are loading the " + dynamicPropertyMetadata.PropertyName() + " in your query.");
                    }
                }

                if (showHyperlink)
                {
                    var displayValue = ((DynamicForiegnKeyPropertyMetadata)dynamicPropertyMetadata).ComplexDynamicEntityMetadata.DefaultProperty().GetValueFunction()(dynamicEntity);
                    var dynamicHyperlinkViewModel = new DynamicEditorHyperlinkViewModel(displayValue.ToString(),
                        ((DynamicForiegnKeyPropertyMetadata)dynamicPropertyMetadata).ComplexEntityPropertyMetadata.TypeName(), "Details");
                    dynamicPropertyViewModel.DynamicEditorHyperlinkViewModel = dynamicHyperlinkViewModel;
                    //ToDo: Need to review this.  It assumes FK follows a certain naming convention.  FK must have name of related type
                    dynamicHyperlinkViewModel.RouteValueDictionaryWrapper.SetValue("Id", ((DynamicForiegnKeyPropertyMetadata)dynamicPropertyMetadata).ComplexDynamicEntityMetadata.KeyProperty().GetValueFunction()(dynamicEntity));
                }

                dynamicPropertyViewModel.DisplayName = ((DynamicForiegnKeyPropertyMetadata)dynamicPropertyMetadata).ComplexEntityPropertyMetadata.PropertyName();

            }
            else if (dynamicPropertyMetadata.IsDynamicCollection())
            {
                var dynamicHyperlinkViewModel = new DynamicEditorHyperlinkViewModel("View " + dynamicPropertyViewModel.PropertyName, dynamicPropertyMetadata.CollectionItemTypeName(), "Index");

                var fkPropertyName = ((DynamicCollectionEntityPropertyMetadata)dynamicPropertyMetadata).ForiegnKeyPropertyName;
                dynamicHyperlinkViewModel.RouteValueDictionaryWrapper.SetValue(fkPropertyName, dynamicPropertyMetadata.DynamicEntityMetadata.KeyProperty().GetValueFunction()(item));
                dynamicPropertyViewModel.DynamicEditorHyperlinkViewModel = dynamicHyperlinkViewModel;
            }
            else
            {
                throw new Exception("DynamicEditorHyperlinkMaterializer was called on a non DynamicForiegnKeyPropertyMetadata");

            }
        }
    }
}
