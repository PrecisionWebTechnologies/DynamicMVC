using System;
using System.Collections.Generic;

namespace DynamicMVC.ApplicationMetadataLibrary.Models
{
    /// <summary>
    /// Holds information about a property of a class being reflected for a given ApplicationEntityMetadata
    /// </summary>
    public class ApplicationEntityMetadataProperty
    {
        public ApplicationEntityMetadataProperty()
        {
            Attributes = new HashSet<Attribute>();
        }

        public ApplicationEntityMetadataProperty(string propertyName)
            : this()
        {
            PropertyName = propertyName;
        }

        public string PropertyName { get; set; }

        public ICollection<Attribute> Attributes { get; set; }
    }
}
