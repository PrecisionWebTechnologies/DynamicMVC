namespace DynamicMVC.Annotations
{
    public class DynamicFilterDateTimeAttribute : DynamicFilterUIHintAttribute
    {
        public DynamicFilterDateTimeAttribute(string labelText, string queryStringName, int addDays)
            :base("DynamicFilterDateTime", "DynamicFilterDateTimeViewModel", "LabelText", labelText, "QueryStringName", queryStringName, "AddDays", addDays)
        {
            
        }

        public DynamicFilterDateTimeAttribute(int addDays)
            : base("DynamicFilterDateTime", "DynamicFilterDateTimeViewModel", "AddDays", addDays)
        {

        }

        public DynamicFilterDateTimeAttribute(string labelText, int addDays)
            : base("DynamicFilterDateTime", "DynamicFilterDateTimeViewModel", "LabelText", labelText, "AddDays", addDays)
        {

        }

    }
}
