namespace DynamicMVC.Annotations
{
    public class DynamicFilterDateRangeAttribute : DynamicFilterUIHintAttribute
    {
        public DynamicFilterDateRangeAttribute(string startLabel, string endLabel
            , string startQueryStringName, string endQueryStringName, int addDays)
            : base("DynamicFilterDateRange", "DynamicFilterDateRangeViewModel"
            , "StartLabel", startLabel
            , "EndLabel", endLabel
            , "StartQueryStringName", startQueryStringName
            , "EndQueryStringName", endQueryStringName
            , "AddDays", addDays)
        { }
    }
}
