using System;

namespace DynamicMVC.Annotations
{
    public class DynamicHeaderAttribute : Attribute
    {
        public DynamicHeaderAttribute()
        {
            
        }
        public DynamicHeaderAttribute(string indexHeader, string creatHeader, string editHeader, string detailsHeader,
            string deleteHeader)
        {
            IndexHeader = indexHeader;
            CreateHeader = creatHeader;
            EditHeader = editHeader;
            DetailsHeader = detailsHeader;
            DeleteHeader = deleteHeader;
        }
        public string IndexHeader { get; set; }
        public string CreateHeader { get; set; }
        public string EditHeader { get; set; }
        public string DetailsHeader { get; set; }
        public string DeleteHeader { get; set; }
    }
}
