using System;
using System.Collections.Generic;
using System.Reflection;

namespace DynamicMVC.ApplicationMetadataLibrary.Models
{
    /// <summary>
    /// Holdes information about the controller methods exactly as they exist in the application
    /// </summary>
    public class ApplicationControllerMethodMetadata
    {
        public ApplicationControllerMethodMetadata()
        {
            Attributes = new HashSet<Type>();
        }
        public ApplicationControllerMethodMetadata(ApplicationControllerMetadata applicationController, string name, MethodInfo methodInfo, ICollection<Type> attributes) : this()
        {
            Name = name;
            Attributes = attributes;
            ApplicationController = applicationController;
            MethodInfo = methodInfo;
        }

        public string Name { get; set; }
        public MethodInfo MethodInfo { get; set; }
        public ICollection<Type> Attributes { get; set; }
        public ApplicationControllerMetadata ApplicationController { get; set; }
    }
}
