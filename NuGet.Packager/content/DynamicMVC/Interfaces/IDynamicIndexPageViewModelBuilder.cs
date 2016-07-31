using DynamicMVC.DynamicEntityMetadataLibrary.Models;
using DynamicMVC.UI.DynamicMVC.ViewModels;

namespace DynamicMVC.UI.DynamicMVC.Interfaces
{
    public interface IDynamicIndexPageViewModelBuilder
    {
        DynamicIndexPageViewModel Build(DynamicEntityMetadata dynamicEntityMetadata);
    }
}