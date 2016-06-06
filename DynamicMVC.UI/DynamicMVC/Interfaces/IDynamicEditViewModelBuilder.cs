using DynamicMVC.DynamicEntityMetadataLibrary.Models;
using DynamicMVC.UI.DynamicMVC.ViewModels;

namespace DynamicMVC.UI.DynamicMVC.Interfaces
{
    public interface IDynamicEditViewModelBuilder
    {
        DynamicEditViewModel Build(DynamicEntityMetadata dynamicEntityMetadata, dynamic editModel, string returnUrl);
    }
}