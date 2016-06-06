using DynamicMVC.DynamicEntityMetadataLibrary.Models;
using DynamicMVC.UI.DynamicMVC.Interfaces;

namespace DynamicMVC.UI.DynamicMVC.ViewModels.DynamicPropertyViewModels
{
    public class DynamicFilterViewModel : DynamicPropertyViewModel
    {

        public DynamicFilterViewModel(DynamicPropertyMetadata dynamicPropertyMetadata) : base(dynamicPropertyMetadata)
        {
        }

        public string DynamicFilterViewName { get; set; }
        public IDynamicFilter FilterModel { get; set; }
    }
}