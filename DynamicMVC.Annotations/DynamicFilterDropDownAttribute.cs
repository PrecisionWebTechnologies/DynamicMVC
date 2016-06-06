namespace DynamicMVC.Annotations
{
    public class DynamicFilterDropDownAttribute : DynamicFilterUIHintAttribute
    {
        public DynamicFilterDropDownAttribute(string labelText)
            : base("DynamicFilterDropDown", "DynamicFilterDropDownViewModel", "LabelText", labelText)
        {
            
        }

        public DynamicFilterDropDownAttribute(string labelText, string nullText)
            : base("DynamicFilterDropDown", "DynamicFilterDropDownViewModel", "LabelText", labelText, "NullText", nullText)
        {

        }

    }
}
