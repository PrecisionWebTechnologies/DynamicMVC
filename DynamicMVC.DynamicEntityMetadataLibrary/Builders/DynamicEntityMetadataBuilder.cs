using System;
using System.Collections.Generic;
using System.Linq;
using DynamicMVC.DynamicEntityMetadataLibrary.Interfaces;
using DynamicMVC.DynamicEntityMetadataLibrary.Models;
using ReflectionLibrary.Interfaces;

// ReSharper disable PossibleMultipleEnumeration

namespace DynamicMVC.DynamicEntityMetadataLibrary.Builders
{
    public class DynamicEntityMetadataBuilder : IDynamicEntityMetadataBuilder
    {
        private readonly IDynamicPropertyMetadataBuilder _dynamicPropertyMetadataBuilder;
        private readonly IDynamicEntityMetadataPropertyFixup[] _dynamicEntityMetadataFixups;
        private readonly Func<DynamicEntityMetadata> _dynamicEntityMetadataResolver;

        public DynamicEntityMetadataBuilder(IDynamicPropertyMetadataBuilder dynamicPropertyMetadataBuilder, IDynamicEntityMetadataPropertyFixup[] dynamicEntityMetadataFixups, Func<DynamicEntityMetadata> dynamicEntityMetadataResolver)
        {
            _dynamicPropertyMetadataBuilder = dynamicPropertyMetadataBuilder;
            _dynamicEntityMetadataFixups = dynamicEntityMetadataFixups;
            _dynamicEntityMetadataResolver = dynamicEntityMetadataResolver;
        }

        public IEnumerable<DynamicEntityMetadata> Build(IEnumerable<IReflectedDynamicClass> reflectedClasses)
        {
            var dynamicEntityMetadatas = new List<DynamicEntityMetadata>();
            foreach (var reflectedClass in reflectedClasses)
            {
                var dynamicEntityMetadata = GetDynamicEntityMetadata(reflectedClasses, reflectedClass.Name);
                dynamicEntityMetadatas.Add(dynamicEntityMetadata);
            }
            //execute fixup extensions in specific order
            foreach (var dynamicEntityMetadataFixup in _dynamicEntityMetadataFixups.OrderBy(x => x.Order()))
            {
                dynamicEntityMetadataFixup.Fixup(dynamicEntityMetadatas);
            }
            return dynamicEntityMetadatas;
        }

        private DynamicEntityMetadata GetDynamicEntityMetadata(IEnumerable<IReflectedDynamicClass> reflectedClasses, string typeName)
        {
            var reflectedClass = reflectedClasses.Single(x => x.Name == typeName);

            var dynamicEntityMetadata = _dynamicEntityMetadataResolver();
            dynamicEntityMetadata.ReflectedClass = reflectedClass;
            dynamicEntityMetadata.ReflectedClasses = reflectedClasses.Select(x => (IReflectedClass)x).ToList();

            var dynamicPropertyMetadatas = _dynamicPropertyMetadataBuilder.Build(reflectedClass, reflectedClasses);
            foreach (var dynamicPropertyMetadata in dynamicPropertyMetadatas)
            {
                dynamicEntityMetadata.DynamicPropertyMetadatas.Add(dynamicPropertyMetadata);
                dynamicPropertyMetadata.DynamicEntityMetadata = dynamicEntityMetadata;
            }
            return dynamicEntityMetadata;
        }

    }
}
