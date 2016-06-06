using System;

namespace DynamicMVC.Annotations
{
    public class DynamicFilterAutoCompleteAttribute : DynamicFilterUIHintAttribute
    {
        public DynamicFilterAutoCompleteAttribute(string labelText, string queryStringName, Type type, string valueMember, string displayMember)
            : base("DynamicFilterAutoComplete", "DynamicFilterAutoCompleteViewModel"
            , "LabelText", labelText
            , "QueryStringName", queryStringName
            , "Type", type
            , "ValueMember", valueMember
            , "DisplayMember", displayMember)
        {
            
        }
    }
}
