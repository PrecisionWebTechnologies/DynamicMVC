using DynamicMVC.DynamicEntityMetadataLibrary.Models;
using DynamicMVC.UI.DynamicMVC.ViewModels.DynamicPropertyViewModels;

namespace DynamicMVC.UI.DynamicMVC.Interfaces
{
    public interface IDynamicDisplayPartialModelBuilder
    {
        string DynamicDisplayPartialName();
        void Build(DynamicPropertyMetadata dynamicPropertyMetadata, DynamicPropertyIndexViewModel dynamicPropertyIndexViewModel, dynamic item);
    }
}