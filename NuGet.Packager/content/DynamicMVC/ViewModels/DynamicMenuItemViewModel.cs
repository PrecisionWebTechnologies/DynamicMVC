using System.Collections.Generic;

namespace DynamicMVC.UI.DynamicMVC.ViewModels
{
    public class DynamicMenuItemViewModel
    {
        public DynamicMenuItemViewModel()
        {
            DynamicMenuItemViewModels = new List<DynamicMenuItemViewModel>();
        }
        public DynamicMenuItemViewModel(string displayName)
            : this()
        {
            DisplayName = displayName;
        }
        public DynamicMenuItemViewModel(DynamicEntityMetadataLibrary.Models.DynamicEntityMetadata dynamicEntityMetadata)
            : this()
        {
            DynamicEntityMetadata = dynamicEntityMetadata;
        }
        public DynamicMenuItemViewModel(DynamicEntityMetadataLibrary.Models.DynamicEntityMetadata dynamicEntityMetadata, string displayName)
            : this(dynamicEntityMetadata)
        {
            DisplayName = displayName;
        }

        public DynamicEntityMetadataLibrary.Models.DynamicEntityMetadata DynamicEntityMetadata { get; set; }
        public string DisplayName { get; set; }

        public List<DynamicMenuItemViewModel> DynamicMenuItemViewModels { get; set; }
    }
}
