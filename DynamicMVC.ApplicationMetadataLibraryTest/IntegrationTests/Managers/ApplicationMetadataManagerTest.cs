using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using DynamicMVC.ApplicationMetadataLibrary.Interfaces;
using DynamicMVC.Shared;
using DynamicMVC.Shared.Interfaces;
using DynamicMVC.Shared.Models;
using DynamicMVCTest.UnitTestHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DynamicMVC.ApplicationMetadataLibraryTest.IntegrationTests.Managers
{
    [TestClass]
    public class ApplicationMetadataManagerTest
    {
        [TestInitialize]
        public void TestIntialize()
        {
            var result = new List<Type>();
            result.Add(typeof(Hello));
            result.Add(typeof(World));
            result.Add(typeof(World2));
            Types = result;

            Container.EagerLoad();
            var container = Container.GetConfiguredContainer();
            ApplicationMetadataLibrary.UnityConfig.RegisterTypes(container);
            var applicationMetadataProvider = new ApplicationMetadataProvider(Types, Types, Types);
            Container.RegisterInstance<IApplicationMetadataProvider>(applicationMetadataProvider);
        }

        public List<Type> Types { get; set; }

        [TestMethod]
        public void DynamicEntityWithoutTypeSpecified_BuildsCorrectly()
        {
            //Arrange
            var applicationMetadatwManager = Container.Resolve<IApplicationMetadataManager>();
            
            //Act
            var summary = applicationMetadatwManager.GetApplicationMetadataSummary();
            var results = summary.ApplicationEntities.ToList();

            //Assert
            Assert.IsTrue(results.Count() == 2);
            var worldEntity = results.Single(x => x.EntityType == typeof(World));
            Assert.IsTrue(worldEntity.ApplicationEntityProperties.Count == 4);
            var nameProperty = worldEntity.ApplicationEntityProperties.Single(x => x.PropertyName == "Name");
            Assert.IsTrue(nameProperty.Attributes.Any(x => x.GetType() == typeof(RequiredAttribute)));
            var ageProperty = worldEntity.ApplicationEntityProperties.Single(x => x.PropertyName == "Age");
            Assert.IsFalse(ageProperty.Attributes.Any());
            var hellosProperty = worldEntity.ApplicationEntityProperties.Single(x => x.PropertyName == "Hellos");
            Assert.IsTrue(hellosProperty.Attributes.Any(x => x.GetType() == typeof(RequiredAttribute)));
            var worldsProperty = worldEntity.ApplicationEntityProperties.Single(x => x.PropertyName == "Worlds");
            Assert.IsTrue(hellosProperty.Attributes.Any(x => x.GetType() == typeof(RequiredAttribute)));
            Assert.IsTrue(worldsProperty.DynamicTypeName == "World2");
            var world2Entity = results.Single(x => x.EntityType == typeof(World2));
            Assert.IsFalse(world2Entity.ApplicationEntityProperties.Any());
        }

        [TestMethod]
        public void DynamicEntityWithTypeSpecified_BuildsCorrectly()
        {
            //Arrange
            var applicationMetadatwManager = Container.Resolve<IApplicationMetadataManager>();

            //Act
            var summary = applicationMetadatwManager.GetApplicationMetadataSummary();
            var results = summary.ApplicationEntities.ToList();

            //Assert
            Assert.IsTrue(results.Count() == 2);
            var worldEntity = results.Single(x => x.EntityType == typeof(World));
            Assert.IsTrue(worldEntity.ApplicationEntityProperties.Count == 4);
            var nameProperty = worldEntity.ApplicationEntityProperties.Single(x => x.PropertyName == "Name");
            Assert.IsTrue(nameProperty.Attributes.Any(x => x.GetType() == typeof(RequiredAttribute)));
            var ageProperty = worldEntity.ApplicationEntityProperties.Single(x => x.PropertyName == "Age");
            Assert.IsFalse(ageProperty.Attributes.Any());
            var hellosProperty = worldEntity.ApplicationEntityProperties.Single(x => x.PropertyName == "Hellos");
            Assert.IsTrue(hellosProperty.Attributes.Any(x => x.GetType() == typeof(RequiredAttribute)));
            var worldsProperty = worldEntity.ApplicationEntityProperties.Single(x => x.PropertyName == "Worlds");
            Assert.IsTrue(hellosProperty.Attributes.Any(x => x.GetType() == typeof(RequiredAttribute)));
            Assert.IsTrue(worldsProperty.DynamicTypeName == "World2");
            var world2Entity = results.Single(x => x.EntityType == typeof(World2));
            Assert.IsFalse(world2Entity.ApplicationEntityProperties.Any());
        }
    }
}
