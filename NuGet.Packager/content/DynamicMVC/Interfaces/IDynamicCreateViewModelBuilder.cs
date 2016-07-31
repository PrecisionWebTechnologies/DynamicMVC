using DynamicMVC.DynamicEntityMetadataLibrary.Models;
using DynamicMVC.UI.DynamicMVC.ViewModels;

namespace DynamicMVC.UI.DynamicMVC.Interfaces
{
    public interface IDynamicCreateViewModelBuilder
    {
        DynamicCreateViewModel Build(DynamicEntityMetadata dynamicEntityMetadata, dynamic createModel, string returnUrl);
    }
}