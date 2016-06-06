using System;
using DynamicMVC.Annotations.Enums;

namespace DynamicMVC.Annotations
{
    public class DynamicMethodAttribute : Attribute
    {
        public DynamicMethodAttribute()
        {
            InvokerName = "EmptyDynamicMethodInvoker";
        }

        public string ButtonText { get; set; }
        public string SubmitValue { get; set; }
        public string RedirectUrl { get; set; }
        public TemplateTypeEnum TemplateTypeEnum { get; set; }
        public string InvokerName { get; set; }
    }

   
}
