using System;
using System.Collections.Generic;

#pragma warning disable 1591

namespace DynamicMVC.Annotations
{
// ReSharper disable once InconsistentNaming
    public class DynamicFilterUIHintAttribute : Attribute
    {
        public DynamicFilterUIHintAttribute()
        {
            ControlParameters = new Dictionary<string, object>();
        }

        public DynamicFilterUIHintAttribute(string dynamicFilterViewName, string dynamicFilterViewModelTypeName,
            params object[] controlParameters) : this()
        {
            DynamicFilterViewName = dynamicFilterViewName;
            DynamicFilterViewModelTypeName = dynamicFilterViewModelTypeName;
            var currentParameter = 0;
            while (currentParameter + 2 <= controlParameters.Length)
            {
                var key = controlParameters[currentParameter].ToString();
                ControlParameters[key] = controlParameters[currentParameter + 1];
                currentParameter += 2;
            }
        }
        public string DynamicFilterViewName { get; set; }
        public string DynamicFilterViewModelTypeName { get; set; }

        public IDictionary<string, object> ControlParameters { get; set; }
        public int Order { get; set; }
    }
}
