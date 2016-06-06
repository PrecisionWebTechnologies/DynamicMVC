namespace DynamicMVC.Annotations
{
    public class DynamicFilterBooleanAttribute : DynamicFilterUIHintAttribute
    {
        public DynamicFilterBooleanAttribute() : base("DynamicFilterBoolean", "DynamicFilterBooleanViewModel")
        {
            
        }

        public DynamicFilterBooleanAttribute(string labelText, string queryStringName, string nullText)
            : base("DynamicFilterBoolean", "DynamicFilterBooleanViewModel"
            , "LabelText", labelText
            , "QueryStringName", queryStringName
            , "NullText", nullText)
        {
            
        }



    }
}
