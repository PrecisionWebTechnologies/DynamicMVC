using DynamicMVC.DynamicEntityMetadataLibrary.Models;
using DynamicMVC.UI.DynamicMVC.ViewModels.DynamicPropertyViewModels;

namespace DynamicMVC.UI.DynamicMVC.Interfaces
{
    public interface IDynamicPropertyViewModelBuilder
    {
        DynamicPropertyIndexViewModel BuildDynamicPropertyIndexViewModel(DynamicPropertyMetadata dynamicPropertyMetadata);
        DynamicPropertyEditorViewModel BuildDynamicPropertyEditorViewModelForEdit(DynamicPropertyMetadata dynamicPropertyMetadata);
        DynamicPropertyEditorViewModel BuildDynamicPropertyEditorViewModelForCreate(DynamicPropertyMetadata dynamicPropertyMetadata);
        DynamicPropertyEditorViewModel BuildDynamicPropertyEditorViewModelForDetails(DynamicPropertyMetadata dynamicPropertyMetadata);
    }
}