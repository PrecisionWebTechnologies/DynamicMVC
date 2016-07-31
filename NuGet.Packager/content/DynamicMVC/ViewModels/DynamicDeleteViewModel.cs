namespace DynamicMVC.UI.DynamicMVC.ViewModels
{
    public class DynamicDeleteViewModel
    {
        public DynamicDeleteViewModel()
        {
            ButtonText = "Delete";
        }

        public string Header { get; set; }
        public string TypeName { get; set; }
        public string ButtonText { get; set; }
        public string ReturnUrl { get; set; }
    }
}
