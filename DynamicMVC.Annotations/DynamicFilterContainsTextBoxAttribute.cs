namespace DynamicMVC.Annotations
{
    public class DynamicFilterContainsTextBoxAttribute : DynamicFilterUIHintAttribute
    {
        public DynamicFilterContainsTextBoxAttribute(string labelText, string queryStringName)
            : base("DynamicFilterContainsTextBox", "DynamicFilterContainsTextBoxViewModel"
            , "LabelText", labelText
            , "QueryStringName", queryStringName)
        {
            
        }
    }
}
