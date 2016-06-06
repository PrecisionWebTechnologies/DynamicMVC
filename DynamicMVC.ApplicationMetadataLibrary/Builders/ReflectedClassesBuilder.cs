using System;
using System.Collections.Generic;
using System.Linq;
using DynamicMVC.Annotations;
using DynamicMVC.ApplicationMetadataLibrary.Interfaces;
using DynamicMVC.Shared.Interfaces;
using ReflectionLibrary.Interfaces;

namespace DynamicMVC.ApplicationMetadataLibrary.Builders
{
    public class ReflectedClassesBuilder : IReflectedClassesBuilder
    {
        private readonly IReflectedLibraryManager _reflectedClassManager;
        private readonly IApplicationMetadataProvider _applicationMetadataProvider;
        private readonly ICustomAttributeProviderManager _customAttributeProviderManager;

        public ReflectedClassesBuilder(IReflectedLibraryManager reflectedClassManager, IApplicationMetadataProvider applicationMetadataProvider, ICustomAttributeProviderManager customAttributeProviderManager)
        {
            _reflectedClassManager = reflectedClassManager;
            _applicationMetadataProvider = applicationMetadataProvider;
            _customAttributeProviderManager = customAttributeProviderManager;
        }

        public ICollection<IReflectedClass> BuildApplicationEntityMetadataReflectedClasses()
        {
            return _reflectedClassManager.GetReflectedClasses(_applicationMetadataProvider.MetadataAssemblyTypes,
                x => x.IsPublic,
                x => !x.IsAbstract,
                x => _customAttributeProviderManager.HasAttribute<DynamicEntityAttribute>(x)
                ).ToList();
        }

        public ICollection<IReflectedClass> BuildApplicationEntityReflectedClasses(IEnumerable<Type> types)
        {
            return _reflectedClassManager.GetReflectedClasses(_applicationMetadataProvider.MetadataAssemblyTypes,
                x=> types.Contains(x)
                ).ToList();
        }
    }
}
