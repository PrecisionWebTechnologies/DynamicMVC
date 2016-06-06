using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using DynamicMVC.Annotations;
using DynamicMVC.EntityMetadataLibrary.Interfaces;
using DynamicMVC.Shared;
using DynamicMVC.Shared.Interfaces;
using DynamicMVC.Shared.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DynamicMVC.EntityMetadataLibraryTest
{
    #region UsedForTesting
    [DynamicEntity]
    public class Hello
    {
        public Hello()
        {
            Worlds = new HashSet<World>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }

        public ICollection<World> Worlds { get; set; }
    }

    [Serializable]
    [DynamicEntity]
    public class World
    {
        [ScaffoldColumn(true)]
        public int Id { get; set; }

        public int HelloId { get; set; }
        public Hello Hello { get; set; }
    }

    public class Test
    {
        public string TestProperty { get; set; }
        public string TestProperty2 { get; set; }
        public ICollection<World> Worlds { get; set; }
    }

    [DynamicEntity]
    public class SimplePropertyTest
    {
        public int NormalInt { get; set; }
        public long NormalLong { get; set; }
        public Guid NormalGuid { get; set; }
        public DateTime NormalDateTime { get; set; }
        public bool NormalBool { get; set; }
        public decimal NormalDecimal { get; set; }
        public float NormalFloat { get; set; }

        public double NormalDouble { get; set; }


        public int? NullableInt { get; set; }

        public long? NullableLong { get; set; }

        public Guid? NullableGuid { get; set; }

        public DateTime? NullableDateTime { get; set; }

        public bool? NullableBool { get; set; }

        public decimal? NullableDecimal { get; set; }

        public float? NullableFloat { get; set; }
        public double? NullableDouble { get; set; }
    }

    [DynamicEntity]
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int? FavoriteColorId { get; set; }
        public int Age { get; set; }
        public bool? Minor { get; set; }
        public FavoriteColor FavoriteColor { get; set; }
    }

    [DynamicEntity]
    public class FavoriteColor
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Person> People { get; set; }
    }
    #endregion

    /// <summary>
    /// Summary description for ApplicationMetadataProviderTest
    /// </summary>
    [TestClass]
    public class ApplicationMetadataProviderTest
    {
        [TestInitialize]
        public void TestInitialize()
        {
            var types = new List<Type>();
            types.Add(typeof(Hello));
            types.Add(typeof(World));
            types.Add(typeof(Test));
            types.Add(typeof(SimplePropertyTest));
            Types = types;

            Container.EagerLoad();
            var container = Container.GetConfiguredContainer();
            DynamicMVC.EntityMetadataLibrary.UnityConfig.RegisterTypes(container);
            var applicationMetadataProvider = new ApplicationMetadataProvider(Types, Types, Types);
            Container.RegisterInstance<IApplicationMetadataProvider>(applicationMetadataProvider);
        }

        public List<Type> Types { get; set; }

        [TestMethod]
        public void EntityMetadata_TwoDynamicEntites_ReturnsCorrectTypeNames()
        {
            //Arrange
            var entityMetadataManager = Container.Resolve<IEntityMetadataManager>();

            //Act
            var entityMetadatas = entityMetadataManager.GetEntityMetadatas().ToList();

            //Assert
            Assert.IsTrue(entityMetadatas.Any(x => x.TypeName == "Hello"));
            Assert.IsTrue(entityMetadatas.Any(x => x.TypeName == "World"));
        }

        [TestMethod]
        public void EntityMetadata_DynamicEntityWithNullableProperty_SetsNullToTrue()
        {
            //Arrange
            Types.Clear();
            Types.Add(typeof(Person));
            Types.Add(typeof(FavoriteColor));
            var applicationMetadataProvider = new ApplicationMetadataProvider(Types, Types, Types);
            Container.RegisterInstance<IApplicationMetadataProvider>(applicationMetadataProvider);
            var entityMetadataManager = Container.Resolve<IEntityMetadataManager>();

            //Act
            var entityMetadatas = entityMetadataManager.GetEntityMetadatas().ToList();

            //Assert
            var person = entityMetadatas.Single(x => x.TypeName == "Person");
            var favoriteColorId = person.EntityPropertyMetadata.Single(x => x.PropertyName == "FavoriteColorId");
            var age = person.EntityPropertyMetadata.Single(x => x.PropertyName == "Age");
            var minor = person.EntityPropertyMetadata.Single(x => x.PropertyName == "Minor");
            Assert.IsTrue(favoriteColorId.IsNullableType);
            Assert.IsTrue(age.IsNullableType == false);
            Assert.IsTrue(minor.IsNullableType);
        }


        [TestMethod]
        public void EntityMetadata_WithOneDynamicModelAttribute_SetsAttributeCorrectly()
        {
            //Arrange
            var entityMetadataManager = Container.Resolve<IEntityMetadataManager>();

            //Act
            var entityMetadatas = entityMetadataManager.GetEntityMetadatas().ToList();

            //Assert
            var helloEntityMetadata = entityMetadatas.Single(x => x.TypeName == "Hello");
            var worldEntityMetadata = entityMetadatas.Single(x => x.TypeName == "World");
            Assert.IsTrue(worldEntityMetadata.EntityAttributes.Any(x => x is SerializableAttribute));
            Assert.IsTrue(helloEntityMetadata.EntityAttributes.Count == 1);
            Assert.IsTrue(worldEntityMetadata.EntityAttributes.Count == 2);
        }

        [TestMethod]
        public void EntityMetadata_PrimitivePropertis_ReturnsPrimitiveEntityPropertyMetadata()
        {
            //Arrange
            var entityMetadataManager = Container.Resolve<IEntityMetadataManager>();

            //Act
            var entityMetadatas = entityMetadataManager.GetEntityMetadatas().ToList();

            //Assert
            var helloEntityMetadata = entityMetadatas.Single(x => x.TypeName == "SimplePropertyTest");
            Assert.IsTrue(helloEntityMetadata.EntityPropertyMetadata.Single(x => x.PropertyName == "NormalInt").IsSimple);
            Assert.IsTrue(helloEntityMetadata.EntityPropertyMetadata.Single(x => x.PropertyName == "NormalLong").IsSimple);
            Assert.IsTrue(helloEntityMetadata.EntityPropertyMetadata.Single(x => x.PropertyName == "NormalFloat").IsSimple);
            Assert.IsTrue(helloEntityMetadata.EntityPropertyMetadata.Single(x => x.PropertyName == "NormalGuid").IsSimple);
            Assert.IsTrue(helloEntityMetadata.EntityPropertyMetadata.Single(x => x.PropertyName == "NormalDateTime").IsSimple);
            Assert.IsTrue(helloEntityMetadata.EntityPropertyMetadata.Single(x => x.PropertyName == "NormalDecimal").IsSimple);
            Assert.IsTrue(helloEntityMetadata.EntityPropertyMetadata.Single(x => x.PropertyName == "NormalDouble").IsSimple);
            Assert.IsTrue(helloEntityMetadata.EntityPropertyMetadata.Single(x => x.PropertyName == "NullableInt").IsSimple);
            Assert.IsTrue(helloEntityMetadata.EntityPropertyMetadata.Single(x => x.PropertyName == "NullableLong").IsSimple);
            Assert.IsTrue(helloEntityMetadata.EntityPropertyMetadata.Single(x => x.PropertyName == "NullableFloat").IsSimple);
            Assert.IsTrue(helloEntityMetadata.EntityPropertyMetadata.Single(x => x.PropertyName == "NullableGuid").IsSimple);
            Assert.IsTrue(helloEntityMetadata.EntityPropertyMetadata.Single(x => x.PropertyName == "NullableDateTime").IsSimple);
            Assert.IsTrue(helloEntityMetadata.EntityPropertyMetadata.Single(x => x.PropertyName == "NullableDecimal").IsSimple);
            Assert.IsTrue(helloEntityMetadata.EntityPropertyMetadata.Single(x => x.PropertyName == "NullableDouble").IsSimple);

        }

        [TestMethod]
        public void EntityMetadata_CollectionProperties_ReturnsCollectionEntityPropertyMetadata()
        {
            //Arrange
            var entityMetadataManager = Container.Resolve<IEntityMetadataManager>();

            //Act
            var entityMetadatas = entityMetadataManager.GetEntityMetadatas().ToList();
            
            //Assert
            var helloEntityMetadata = entityMetadatas.Single(x => x.TypeName == "Hello");
            Assert.IsTrue(helloEntityMetadata.EntityPropertyMetadata.Single(x => x.PropertyName == "Worlds").IsCollection);
        }

        [TestMethod]
        public void EntityMetadata_ComplextEntityProperties_ReturnsComplexEntityPropertyMetadata()
        {
            //Arrange
            var entityMetadataManager = Container.Resolve<IEntityMetadataManager>();

            //Act
            var entityMetadatas = entityMetadataManager.GetEntityMetadatas().ToList();
            
            var helloEntityMetadata = entityMetadatas.Single(x => x.TypeName == "World");
            Assert.IsTrue(helloEntityMetadata.EntityPropertyMetadata.Single(x => x.PropertyName == "Hello").IsComplexEntity);
        }
    }
}
