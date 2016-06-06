using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DynamicMVC.Shared.Interfaces;

namespace DynamicMVC.Shared.Models
{
    /// <summary>
    /// Holds information passed in from DynamicMVC related to Metadata for the application.  ApplicationMetadata can only reference Annotations.
    /// </summary>
    public class ApplicationMetadataProvider : IApplicationMetadataProvider
    {
        /// <summary>
        /// Constructor that can be used if passing in assemblies to be reflected by dynamic mvc.
        /// </summary>
        /// <param name="mvcAssembly">MVC project assembly.</param>
        /// <param name="metaDataAssembly">Project assembly that holds the metadata for the entities in the solution.</param>
        /// <param name="entityAssembly">Project assembly that holds the entities in the solution.</param>
        public ApplicationMetadataProvider(Assembly mvcAssembly, Assembly metaDataAssembly, Assembly entityAssembly)
        {
            MvcAssemblyTypes = mvcAssembly.GetTypes().ToList();
            MetadataAssemblyTypes = metaDataAssembly.GetTypes().ToList();
            EntityAssemblyTypes = entityAssembly.GetTypes().ToList();
        }

        /// <summary>
        ///  Extensability constructor
        /// </summary>
        /// <param name="mvcAssemblyTypes">Types reflected from MVC Project</param>
        /// <param name="metaDataAssemblyTypes">Types reflected from project that contains Metadata for Dynamic Entities</param>
        /// <param name="entityAssemblyTypes">Types reflected from project that contains Dynamic Entities</param>
        public ApplicationMetadataProvider(IEnumerable<Type> mvcAssemblyTypes, IEnumerable<Type> metaDataAssemblyTypes, IEnumerable<Type> entityAssemblyTypes)
        {
            MvcAssemblyTypes = mvcAssemblyTypes;
            MetadataAssemblyTypes = metaDataAssemblyTypes;
            EntityAssemblyTypes = entityAssemblyTypes;
        }

        /// <summary>
        /// Types reflected from MVC Project
        /// </summary>
        public IEnumerable<Type> MvcAssemblyTypes { get; set; }
        /// <summary>
        /// Types reflected from project that contains Metadata for Dynamic Entities
        /// </summary>
        public IEnumerable<Type> MetadataAssemblyTypes { get; set; }
        /// <summary>
        /// Types reflected from project that contains Dynamic Entities
        /// </summary>
        public IEnumerable<Type> EntityAssemblyTypes { get; set; }

      
    }
}
