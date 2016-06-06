using System;
using System.Collections.Generic;
using System.Linq;
using DynamicMVC.Annotations.Enums;
using DynamicMVC.DynamicEntityMetadataLibrary.Interfaces;
using DynamicMVC.Shared.Enums;

namespace DynamicMVC.DynamicEntityMetadataLibrary.Models
{
    /// <summary>
    /// Holds Metadata for a DynamicEntity after it has been parsed by dynamic mvc
    /// </summary>
    public class DynamicEntityMetadata
    {
        private readonly IDynamicOperationManager _dynamicOperationManager;
        private readonly IDynamicMethodManager _dynamicMethodManager;

        public DynamicEntityMetadata(IDynamicOperationManager dynamicOperationManager, IDynamicMethodManager dynamicMethodManager)
        {
            DynamicPropertyMetadatas = new HashSet<DynamicPropertyMetadata>();
            DynamicMethods = new HashSet<DynamicMethod>();
            _dynamicOperationManager = dynamicOperationManager;
            _dynamicMethodManager = dynamicMethodManager;
        }

        public override string ToString()
        {
            return TypeName;
        }

        public ICollection<DynamicPropertyMetadata> DynamicPropertyMetadatas { get; set; }
        /// <summary>
        /// Returns all properties that are complex entity types by default.
        /// </summary>
        /// 
        public ICollection<string> ListIncludes { get; set; }
        public ICollection<string> InstanceIncludes { get; set; }

        public DynamicPropertyMetadata DefaultProperty { get; set; }
        public DynamicPropertyMetadata KeyProperty { get; set; }

        //ToDo: Add Post Materializer Validators  -These properties cannot be null.
        public ICollection<DynamicPropertyMetadata> ScaffoldProperties { get; set; }
        public ICollection<DynamicPropertyMetadata> ScaffoldCreateProperties { get; set; }
        public ICollection<DynamicPropertyMetadata> ScaffoldEditProperties { get; set; }
        public ICollection<DynamicPropertyMetadata> ScaffoldDetailsProperties { get; set; }
        public ICollection<DynamicPropertyMetadata> ScaffoldIndexProperties { get; set; }

        public DynamicMenuInfo DynamicMenuInfo { get; set; }

        public string CreateProperties { get; set; }
        public string CreateHeader { get; set; }
        public bool ShowCreate { get; set; }
        public Func<object> CreateNewObject { get; set; }

        public string DetailsProperties { get; set; }
        public string DetailsHeader { get; set; }
        public bool ShowDetails { get; set; }

        public string EditProperties { get; set; }
        public string EditHeader { get; set; }
        public bool ShowEdit { get; set; }

        public string IndexHeader { get; set; }
        public string IndexProperties { get; set; }

        public bool ShowDelete { get; set; }
        public string DeleteHeader { get; set; }

        public ICollection<DynamicMethod> DynamicMethods { get; set; }
        public DynamicOperation GetDynamicOperation(TemplateTypeEnum templateTypeEnum, string submitValue)
        {
            return _dynamicOperationManager.GetDynamicOperation(templateTypeEnum, submitValue, DynamicMethods);
        }

        public IEnumerable<DynamicMethod> GetDynamicMethods(TemplateTypeEnum templateTypeEnum)
        {
            return _dynamicMethodManager.GetDynamicMethods(templateTypeEnum, DynamicMethods);
        }

        public string TypeName { get; set; }

        public Type EntityType { get; set; }

        public bool ControllerExists { get; set; }


    }
}
