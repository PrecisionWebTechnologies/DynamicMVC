using System;
using System.Collections.Generic;
using System.Linq;
using DynamicMVC.Annotations;

namespace DynamicMVC.ApplicationMetadataLibrary.Models
{
    /// <summary>
    /// Holds information about a class that has the DynamicEntity attribute on it.  This class is used to gather attributes about itself or any class it assigns itself to using the DynamicEntity.EntityType property.
    /// </summary>
    public class ApplicationEntityMetadata
    {
        public ApplicationEntityMetadata()
        {
            Attributes = new HashSet<Attribute>();
            ApplicationEntityMetadataProperty = new HashSet<ApplicationEntityMetadataProperty>();
        }
        public ApplicationEntityMetadata(IEnumerable<Attribute> attributes, Type type) : this()
        {
            Attributes = attributes.ToList();
            //if entitytype is specified on DynamicAttribute, do not overwrite it
            if (EntityType == null)
                EntityType = type;
        }

        public DynamicEntityAttribute DynamicEntityAttribute()
        {
            return (DynamicEntityAttribute)Attributes.Single(x => x is DynamicEntityAttribute);
        }

        public Type EntityType
        {
            get { return DynamicEntityAttribute().EntityType; }
            set { DynamicEntityAttribute().EntityType = value; }
        }
        public ICollection<Attribute> Attributes { get; set; }
        public ICollection<ApplicationEntityMetadataProperty> ApplicationEntityMetadataProperty { get; set; }
        public ApplicationEntity ApplicationEntity { get; set; }
    }
}
