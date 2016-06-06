using System;

namespace DynamicMVC.Annotations
{
    public class DynamicEntityAttribute : Attribute
    {
        public DynamicEntityAttribute()
        {
            Key = "Id";
            ShowCreate = true;
            ShowDetails = true;
            ShowEdit = true;
            ShowDelete = true;
        }

        public DynamicEntityAttribute(string key, bool showCreate, bool showDetails, bool showEdit, bool showDelete)
            : this()
        {
            Key = key;
            ShowCreate = showCreate;
            ShowDetails = showDetails;
            ShowEdit = showEdit;
            ShowDelete = showDelete;
        }

        public string Key { get; set; }
        public bool ShowCreate { get; set; }
        public bool ShowDetails { get; set; }
        public bool ShowEdit { get; set; }
        public bool ShowDelete { get; set; }
        public string CreateProperties { get; set; }
        public string EditProperties { get; set; }
        public string DetailsProperties { get; set; }
        public string IndexProperties { get; set; }
        public string InstanceIncludes { get; set; }
        public string ListIncludes { get; set; }
        public Type EntityType { get; set; }
    }
}
