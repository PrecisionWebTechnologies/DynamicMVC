using DynamicMVC.DynamicEntityMetadataLibrary.Models;
using DynamicMVC.UI.DynamicMVC.ViewModels;

namespace DynamicMVC.UI.DynamicMVC.Interfaces
{
    public interface IDynamicDetailsViewModelBuilder
    {
        DynamicDetailsViewModel Build(DynamicEntityMetadata dynamicEntityMetadata, dynamic detailModel);
    }
}