using System;

namespace DynamicMVC.Annotations
{
    public class DynamicMenuItemAttribute : Attribute
    {
        public DynamicMenuItemAttribute(string displayName, string categoryName)
        {
            DisplayName = displayName;
            CategoryName = categoryName;
        }

        public string DisplayName { get; set; }
        public string CategoryName { get; set; }
    }
}
