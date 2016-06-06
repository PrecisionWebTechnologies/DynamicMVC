using System.Web.Mvc;
using DynamicMVC.DynamicEntityMetadataLibrary.Models;
using DynamicMVC.UI.DynamicMVC.ViewModels.DynamicEditorViewModels;

namespace DynamicMVC.UI.DynamicMVC.ViewModels.DynamicPropertyViewModels
{
    public class DynamicPropertyIndexViewModel : DynamicPropertyViewModel
    {
        public DynamicPropertyIndexViewModel(DynamicPropertyMetadata dynamicPropertyMetadata) : base(dynamicPropertyMetadata)
        {
            
        }

        //These are used in mobile page only
        public bool AllowSort { get; set; }
        public string SortExpression { get; set; }

        public string PartialViewName { get; set; }
        public DynamicEditorHyperlinkViewModel DynamicEditorHyperlinkViewModel { get; set; }
        public ViewDataDictionary ToViewDataDictionary()
        {
            var vdd = new ViewDataDictionary();
            vdd.Add("DynamicPropertyViewModel", this);
            return vdd;
        }
    }
}