using System;
using System.Collections.Generic;
using System.Linq;
using DynamicMVC.Annotations;
using DynamicMVC.DynamicEntityMetadataLibrary.Interfaces;
using DynamicMVC.DynamicEntityMetadataLibrary.Models;
using DynamicMVC.Shared.Extensions;
using DynamicMVC.Shared.Interfaces;
using ReflectionLibrary.Interfaces;
using ReflectionLibrary.Models;

#pragma warning disable 1591

namespace DynamicMVC.DynamicEntityMetadataLibrary.Managers
{
    public class ReflectionManager : IReflectionManager
    {
        private readonly IApplicationMetadataProvider _applicationMetadataProvider;
        private readonly INamingConventionManager _namingConventionManager;
        private readonly IReflectedLibraryManager _reflectedLibraryManager;
        private readonly ICustomAttributeProviderManager _customAttributeProviderManager;

        public ReflectionManager(IApplicationMetadataProvider applicationMetadataProvider, INamingConventionManager namingConventionManager, IReflectedLibraryManager reflectedLibraryManager, ICustomAttributeProviderManager customAttributeProviderManager)
        {
            _applicationMetadataProvider = applicationMetadataProvider;
            _namingConventionManager = namingConventionManager;
            _reflectedLibraryManager = reflectedLibraryManager;
            _customAttributeProviderManager = customAttributeProviderManager;
        }

        public IEnumerable<IReflectedDynamicClass> GetReflectedDynamicClasses()
        {
            var results = new List<ReflectedDynamicClass>();
            var controllerTypes = _applicationMetadataProvider.MvcAssemblyTypes.Where(x => _namingConventionManager.IsController(x)).ToList();
            var entityMetadataTypes = _applicationMetadataProvider.MetadataAssemblyTypes.Where(x => _customAttributeProviderManager.HasAttribute<DynamicEntityAttribute>(x)).ToList();
            var entityMetadataReflectedClasses = _reflectedLibraryManager.GetReflectedClasses(entityMetadataTypes).ToList();
            foreach (var entityMetaDataReflectedClass in entityMetadataReflectedClasses)
            {
                ReflectedDynamicClass reflectedDynamicClass = null;

                var dynamicEntityAttribute = entityMetaDataReflectedClass.Attributes.GetAttribute<DynamicEntityAttribute>();

                if (dynamicEntityAttribute.EntityType == null)
                {
                    reflectedDynamicClass = (ReflectedDynamicClass) entityMetaDataReflectedClass;
                }
                else
                {
                    reflectedDynamicClass = (ReflectedDynamicClass)_reflectedLibraryManager.GetReflectedClass(dynamicEntityAttribute.EntityType);
                    reflectedDynamicClass.MergeAttributes(entityMetaDataReflectedClass);
                }

                reflectedDynamicClass.ControllerReflectedClass = (ReflectedClass) GetControllerReflectedClass(controllerTypes, reflectedDynamicClass.Name);
                results.Add(reflectedDynamicClass);
            }
            
            return results;
        }

        private IReflectedClass GetControllerReflectedClass(IEnumerable<Type> controllerTypes, string typeName)
        {
            var controllerNames = controllerTypes.Select(x => x.Name).ToList();
            var controllerName = _namingConventionManager.FindControllerName(controllerNames, typeName);
            if (controllerName != null)
            {
                var controllerType = controllerTypes.Single(x => x.Name == controllerName);
                return _reflectedLibraryManager.GetReflectedClass(controllerType);
            }
            return null;
        }
    }
}
