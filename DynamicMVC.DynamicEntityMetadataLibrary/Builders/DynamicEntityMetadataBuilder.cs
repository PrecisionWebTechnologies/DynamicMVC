using System;
using System.Collections.Generic;
using System.Linq;
using DynamicMVC.DynamicEntityMetadataLibrary.Interfaces;
using DynamicMVC.DynamicEntityMetadataLibrary.Models;
using DynamicMVC.EntityMetadataLibrary.Models;

// ReSharper disable PossibleMultipleEnumeration

namespace DynamicMVC.DynamicEntityMetadataLibrary.Builders
{
    public class DynamicEntityMetadataBuilder : IDynamicEntityMetadataBuilder
    {
        private readonly IDynamicEntityMetadataBuilderHelper[] _dynamicEntityMetadataBuilderHelpers;
        private readonly IDynamicPropertyMetadataBuilder _dynamicPropertyMetadataBuilder;
        private readonly IDynamicEntityMetadataPropertyFixup[] _dynamicEntityMetadataFixups;
        private readonly Func<DynamicEntityMetadata> _dynamicEntityMetadataFunction;

        public DynamicEntityMetadataBuilder(IDynamicEntityMetadataBuilderHelper[] dynamicEntityMetadataBuilderHelpers, IDynamicPropertyMetadataBuilder dynamicPropertyMetadataBuilder, IDynamicEntityMetadataPropertyFixup[] dynamicEntityMetadataFixups, Func<DynamicEntityMetadata> dynamicEntityMetadataFunction)
        {
            _dynamicEntityMetadataBuilderHelpers = dynamicEntityMetadataBuilderHelpers;
            _dynamicPropertyMetadataBuilder = dynamicPropertyMetadataBuilder;
            _dynamicEntityMetadataFixups = dynamicEntityMetadataFixups;
            _dynamicEntityMetadataFunction = dynamicEntityMetadataFunction;
        }

        public IEnumerable<DynamicEntityMetadata> Build(IEnumerable<EntityMetadata> entityMetadatas)
        {
            var dynamicEntityMetadatas = new List<DynamicEntityMetadata>();
            foreach (var entityMetadata in entityMetadatas)
            {
                var dynamicEntityMetadata = GetDynamicEntityMetadata(entityMetadatas, entityMetadata.TypeName);
                dynamicEntityMetadatas.Add(dynamicEntityMetadata);
            }
            //execute fixup extensions in specific order
            foreach (var dynamicEntityMetadataFixup in _dynamicEntityMetadataFixups.OrderBy(x=>x.Order()))
            {
                dynamicEntityMetadataFixup.Fixup(dynamicEntityMetadatas);
            }
            return dynamicEntityMetadatas;
        }

        private DynamicEntityMetadata GetDynamicEntityMetadata(IEnumerable<EntityMetadata> entityMetadatas, string typeName)
        {
            var entityMetadata = entityMetadatas.Single(x => x.TypeName == typeName);

            var dynamicEntityMetadata = _dynamicEntityMetadataFunction();
            dynamicEntityMetadata.TypeName = entityMetadata.TypeName;
            dynamicEntityMetadata.EntityType = entityMetadata.EntityType;
            dynamicEntityMetadata.CreateNewObject = entityMetadata.CreateNewObject;

            var dynamicPropertyMetadatas = _dynamicPropertyMetadataBuilder.Build(entityMetadata);
            foreach (var dynamicPropertyMetadata in dynamicPropertyMetadatas)
            {
                dynamicEntityMetadata.DynamicPropertyMetadatas.Add(dynamicPropertyMetadata);
                dynamicPropertyMetadata.DynamicEntityMetadata = dynamicEntityMetadata;
            }
            //execute materializerhelper extensions
            foreach (var dynamicEntityMetadataMaterializerHelper in _dynamicEntityMetadataBuilderHelpers)
            {
                dynamicEntityMetadataMaterializerHelper.Build(dynamicEntityMetadata, entityMetadata);
            }
            return dynamicEntityMetadata;
        }

    }
}
