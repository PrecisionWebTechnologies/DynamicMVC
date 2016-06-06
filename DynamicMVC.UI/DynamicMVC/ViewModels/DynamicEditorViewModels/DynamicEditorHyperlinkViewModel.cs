namespace DynamicMVC.UI.DynamicMVC.ViewModels.DynamicEditorViewModels
{
    public class DynamicEditorHyperlinkViewModel
    {
        public DynamicEditorHyperlinkViewModel()
        {
            RouteValueDictionaryWrapper = new RouteValueDictionaryWrapper();
        }
        public DynamicEditorHyperlinkViewModel(string displayName, string controllerName, string actionName)
            : this()
        {
            DisplayName = displayName;
            ControllerName = controllerName;
            ActionName = actionName;
        }
        public string DisplayName { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }

        public RouteValueDictionaryWrapper RouteValueDictionaryWrapper { get; set; }

    }
}
