using System;
using System.Collections.Generic;

namespace DynamicMVC.ApplicationMetadataLibrary.Models
{
    /// <summary>
    /// Holds information about a class that has been flagged through the DynamicEntity.EntityType property on any other class or has the DynamicEntity attribute on itself.
    /// </summary>
    public class ApplicationEntity
    {
        public ApplicationEntity()
        {
            Attributes = new HashSet<Attribute>();
            ApplicationEntityProperties= new HashSet<ApplicationEntityProperty>();
        }

        public Type EntityType { get; set; }
        public ApplicationEntityMetadata ApplicationEntityMetadata { get; set; }
        public ICollection<Attribute> Attributes { get; set; }
        public ICollection<ApplicationEntityProperty> ApplicationEntityProperties { get; set; }

        public ApplicationControllerMetadata ApplicationControllerMetadata { get; set; }
        
    }
}
