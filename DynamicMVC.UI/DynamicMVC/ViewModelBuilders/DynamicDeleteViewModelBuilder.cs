using DynamicMVC.DynamicEntityMetadataLibrary.Models;
using DynamicMVC.UI.DynamicMVC.Interfaces;
using DynamicMVC.UI.DynamicMVC.ViewModels;

namespace DynamicMVC.UI.DynamicMVC.ViewModelBuilders
{
    public class DynamicDeleteViewModelBuilder : IDynamicDeleteViewModelBuilder
    {
        public DynamicDeleteViewModel Build(DynamicEntityMetadata dynamicEntityMetadata, dynamic deleteModel, string returnUrl)
        {
            var dynamicDeleteViewModel = new DynamicDeleteViewModel();
            dynamicDeleteViewModel.TypeName = dynamicEntityMetadata.TypeName();
            dynamicDeleteViewModel.Header = "Delete " + dynamicEntityMetadata.TypeName();
            dynamicDeleteViewModel.ReturnUrl = returnUrl;
            return dynamicDeleteViewModel;
        }
    }
}
