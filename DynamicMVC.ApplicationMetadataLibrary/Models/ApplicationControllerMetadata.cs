using System.Collections.Generic;

namespace DynamicMVC.ApplicationMetadataLibrary.Models
{
    /// <summary>
    /// Holds information about the controllers exactly as they exist in the application
    /// </summary>
    public class ApplicationControllerMetadata
    {
        public ApplicationControllerMetadata()
        {
            ApplicationControllerMethods = new HashSet<ApplicationControllerMethodMetadata>();
        }

        public ApplicationControllerMetadata(string name) : this()
        {
            Name = name;
        }

        public string Name { get; set; }
        public ICollection<ApplicationControllerMethodMetadata> ApplicationControllerMethods { get; set; }
    }
}
