using System.Collections.Generic;
using DynamicMVC.DynamicEntityMetadataLibrary.Models;
using DynamicMVC.UI.DynamicMVC.ViewModels.DynamicControls;

#pragma warning disable 1591

namespace DynamicMVC.UI.DynamicMVC.ViewModels
{
    public class DynamicCreateViewModel
    {
        public DynamicCreateViewModel()
        {
            DynamicEditorViewModels = new HashSet<DynamicEditorViewModel>();
            Title = "Create";
            ButtonText = "Save";
            DynamicUIMethods= new HashSet<DynamicMethod>();
        }

        public string Header { get; set; }
        public string TypeName { get; set; }
        public string ReturnUrl { get; set; }
        public string Title { get; set; }
        public string ButtonText { get; set; }
        public dynamic Item { get; set; }
        public ICollection<DynamicEditorViewModel> DynamicEditorViewModels { get; set; }
        public ICollection<DynamicMethod> DynamicUIMethods { get; set; }
        
    }
}
