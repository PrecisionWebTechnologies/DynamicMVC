namespace DynamicMVC.Annotations
{
    public class DynamicFilterExactMatchTextBoxAttribute : DynamicFilterUIHintAttribute
    {
        public DynamicFilterExactMatchTextBoxAttribute(string labelText, string queryStringName)
            : base("DynamicFilterExactMatchTextBox", "DynamicFilterExactMatchTextBoxViewModel"
            , "LabelText", labelText
            , "QueryStringName", queryStringName)
        {
            
        }
    }
}
