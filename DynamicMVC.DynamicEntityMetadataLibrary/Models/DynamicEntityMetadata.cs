using System;
using System.Collections.Generic;
using System.Linq;
using DynamicMVC.Annotations;
using DynamicMVC.Annotations.Enums;
using DynamicMVC.DynamicEntityMetadataLibrary.Interfaces;
using DynamicMVC.Shared.Extensions;
using DynamicMVC.Shared.Interfaces;
using ReflectionLibrary.Extensions;
using ReflectionLibrary.Interfaces;

namespace DynamicMVC.DynamicEntityMetadataLibrary.Models
{
    /// <summary>
    /// Holds Metadata for a DynamicEntity after it has been parsed by dynamic mvc
    /// </summary>
    public class DynamicEntityMetadata
    {
        private readonly IDynamicOperationManager _dynamicOperationManager;
        private readonly IDynamicMethodManager _dynamicMethodManager;
        private readonly IPropertyFilterManager _propertyFilterManager;
        private readonly INamingConventionManager _namingConventionManager;

        public DynamicEntityMetadata(IDynamicOperationManager dynamicOperationManager, IDynamicMethodManager dynamicMethodManager, IPropertyFilterManager propertyFilterManager, INamingConventionManager namingConventionManager)
        {
            DynamicPropertyMetadatas = new HashSet<DynamicPropertyMetadata>();

            _dynamicOperationManager = dynamicOperationManager;
            _dynamicMethodManager = dynamicMethodManager;
            _propertyFilterManager = propertyFilterManager;
            _namingConventionManager = namingConventionManager;
        }

        public override string ToString()
        {
            return ReflectedClass.Name;
        }
        public IReflectedDynamicClass ReflectedClass { get; set; }
        public ICollection<IReflectedClass> ReflectedClasses { get; set; }

        public ICollection<DynamicPropertyMetadata> DynamicPropertyMetadatas { get; set; }

        /// <summary>
        /// Returns all properties that are complex entity types by default.
        /// </summary>
        /// 
        public ICollection<string> ListIncludes()
        {
            var dynamicEntityAttribute = ReflectedClass.GetAttribute<DynamicEntityAttribute>();
            if (dynamicEntityAttribute.ListIncludes != null)
                return dynamicEntityAttribute.ListIncludes.SplitAndTrim().ToList();
            return DynamicPropertyMetadatas.Where(x => x.IsDynamicEntity()).Select(x => x.ReflectedProperty.Name).ToList();

        }

        public ICollection<string> InstanceIncludes()
        {
            var dynamicEntityAttribute = ReflectedClass.GetAttribute<DynamicEntityAttribute>();
            if (dynamicEntityAttribute.InstanceIncludes != null)
                return dynamicEntityAttribute.InstanceIncludes.SplitAndTrim().ToList();
            return DynamicPropertyMetadatas.Where(x => x.IsDynamicEntity()).Select(x => x.ReflectedProperty.Name).ToList();
        }

        public DynamicPropertyMetadata DefaultProperty()
        {
            var propertyNames = DynamicPropertyMetadatas.Select(x => x.ReflectedProperty.Name).ToList();
            var defaultPropertyName = _namingConventionManager.FindDefaultPropertyName(ReflectedClass.Name, propertyNames);
            return DynamicPropertyMetadatas.Single(x => x.ReflectedProperty.Name == defaultPropertyName);

        }
        public DynamicPropertyMetadata KeyProperty()
        {
            var keyPropertyName = ReflectedClass.GetAttribute<DynamicEntityAttribute>().Key;
            var keyProperty = DynamicPropertyMetadatas.SingleOrDefault(x => x.ReflectedProperty.Name == keyPropertyName);
            if (keyProperty == null)
                throw new Exception("Could not find KeyValue for " + keyPropertyName + " and type " + TypeName());
            return keyProperty;
        }

        public ICollection<DynamicPropertyMetadata> ScaffoldProperties()
        {
            return DynamicPropertyMetadatas.Where(x => x.Scaffold()).ToList();
        }
        public ICollection<DynamicPropertyMetadata> ScaffoldCreateProperties()
        {
            if (!string.IsNullOrWhiteSpace(CreateProperties()))
                return _propertyFilterManager.FilterAndOrderProperties(ScaffoldProperties(), CreateProperties()).ToList();
            else
                return ScaffoldProperties();

        }
        public ICollection<DynamicPropertyMetadata> ScaffoldEditProperties()
        {
            if (!string.IsNullOrWhiteSpace(EditProperties()))
                return _propertyFilterManager.FilterAndOrderProperties(ScaffoldProperties(), EditProperties()).ToList();
            else
                return ScaffoldProperties();
        }
        public ICollection<DynamicPropertyMetadata> ScaffoldDetailsProperties()
        {
            if (!string.IsNullOrWhiteSpace(DetailsProperties()))
                return _propertyFilterManager.FilterAndOrderProperties(ScaffoldProperties(), DetailsProperties()).ToList();
            else
                return ScaffoldProperties();
        }
        public ICollection<DynamicPropertyMetadata> ScaffoldIndexProperties()
        {
            if (!string.IsNullOrWhiteSpace(IndexProperties()))
                return _propertyFilterManager.FilterAndOrderProperties(ScaffoldProperties(), IndexProperties()).ToList();
            else
                return ScaffoldProperties();
        }

        public DynamicMenuInfo DynamicMenuInfo()
        {
            var dynamicMenuInfo = new DynamicMenuInfo();
            var dynamicMenuItemExcludeAttribute = ReflectedClass.GetAttribute<DynamicMenuItemExcludeAttribute>();
            var dynamicMenuItemAttribute = ReflectedClass.GetAttribute<DynamicMenuItemAttribute>();
            dynamicMenuInfo.HasMenuItem = dynamicMenuItemExcludeAttribute == null;
            dynamicMenuInfo.MenuItemCategory = dynamicMenuItemAttribute == null ? _namingConventionManager.DynamicMenuCategory() : dynamicMenuItemAttribute.CategoryName;
            dynamicMenuInfo.MenuItemDisplayName = dynamicMenuItemAttribute != null ? dynamicMenuItemAttribute.DisplayName : ReflectedClass.Name;
            return dynamicMenuInfo;
        }

        public string CreateProperties()
        {
            return ReflectedClass.GetAttribute<DynamicEntityAttribute>().CreateProperties;
        }

        public string CreateHeader()
        {
            var dynamicHeaderAttribute = ReflectedClass.GetAttribute<DynamicHeaderAttribute>();
            return dynamicHeaderAttribute != null ?
                !string.IsNullOrWhiteSpace(dynamicHeaderAttribute.CreateHeader) ? dynamicHeaderAttribute.CreateHeader : "Create " + ReflectedClass.Name
                : "Create " + ReflectedClass.Name;
        }
        public bool ShowCreate()
        {
            return ReflectedClass.GetAttribute<DynamicEntityAttribute>().ShowCreate;
        }
        public Func<object> CreateNewObject()
        {
            return ReflectedClass.ReflectedClassOperations.CreateNewObject;
        }

        public string DetailsProperties()
        {
            return ReflectedClass.GetAttribute<DynamicEntityAttribute>().DetailsProperties;
        }

        public string DetailsHeader()
        {
            var dynamicHeaderAttribute = ReflectedClass.GetAttribute<DynamicHeaderAttribute>();
            return dynamicHeaderAttribute != null ?
                !string.IsNullOrWhiteSpace(dynamicHeaderAttribute.DetailsHeader) ? dynamicHeaderAttribute.DetailsHeader : ReflectedClass.Name + " Details"
                : ReflectedClass.Name + " Details";
        }

        public bool ShowDetails()
        {
            return ReflectedClass.GetAttribute<DynamicEntityAttribute>().ShowDetails;
        }

        public string EditProperties()
        {
            return ReflectedClass.GetAttribute<DynamicEntityAttribute>().EditProperties;
        }

        public string EditHeader()
        {
            var dynamicHeaderAttribute = ReflectedClass.GetAttribute<DynamicHeaderAttribute>();
            return dynamicHeaderAttribute != null ?
                !string.IsNullOrWhiteSpace(dynamicHeaderAttribute.EditHeader) ? dynamicHeaderAttribute.EditHeader : "Edit " + ReflectedClass.Name
                : "Edit " + ReflectedClass.Name;
        }

        public bool ShowEdit()
        {
            return ReflectedClass.GetAttribute<DynamicEntityAttribute>().ShowEdit;
        }

        public string IndexHeader()
        {
            var dynamicHeaderAttribute = ReflectedClass.GetAttribute<DynamicHeaderAttribute>();
            return dynamicHeaderAttribute != null ?
                !string.IsNullOrWhiteSpace(dynamicHeaderAttribute.IndexHeader) ? dynamicHeaderAttribute.IndexHeader : ReflectedClass.Name + " List"
                : ReflectedClass.Name + " List";
        }

        public string IndexProperties()
        {
            return ReflectedClass.GetAttribute<DynamicEntityAttribute>().IndexProperties;
        }

        public bool ShowDelete()
        {
            return ReflectedClass.GetAttribute<DynamicEntityAttribute>().ShowDelete;
        }

        public string DeleteHeader()
        {
            var dynamicHeaderAttribute = ReflectedClass.GetAttribute<DynamicHeaderAttribute>();
            return dynamicHeaderAttribute != null ?
                !string.IsNullOrWhiteSpace(dynamicHeaderAttribute.DeleteHeader) ? dynamicHeaderAttribute.DeleteHeader : "Delete " + ReflectedClass.Name
                : "Delete " + ReflectedClass.Name;
        }

        public ICollection<DynamicMethod> DynamicMethods()
        {
            return _dynamicMethodManager.GetDynamicMethods(ReflectedClass).ToList();
        }
        public DynamicOperation GetDynamicOperation(TemplateTypeEnum templateTypeEnum, string submitValue)
        {
            return _dynamicOperationManager.GetDynamicOperation(templateTypeEnum, submitValue, DynamicMethods());
        }

        public IEnumerable<DynamicMethod> GetDynamicMethods(TemplateTypeEnum templateTypeEnum)
        {
            return _dynamicMethodManager.GetDynamicMethods(templateTypeEnum, DynamicMethods());
        }

        public string TypeName()
        {
            return ReflectedClass.Name;
        }

        public Func<Type> EntityTypeFunction()
        {
            return ReflectedClass.ReflectedClassOperations.GetReflectedType;
        }

        public bool ControllerExists()
        {
            return ReflectedClass.ControllerReflectedClass != null;
        }


    }
}
