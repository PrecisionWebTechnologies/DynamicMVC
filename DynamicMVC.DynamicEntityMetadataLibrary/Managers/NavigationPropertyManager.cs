using System;
using System.Linq;
using DynamicMVC.DynamicEntityMetadataLibrary.Interfaces;
using DynamicMVC.DynamicEntityMetadataLibrary.Models;
using DynamicMVC.EntityMetadataLibrary.Models;
using DynamicMVC.Shared.Interfaces;

namespace DynamicMVC.DynamicEntityMetadataLibrary.Managers
{
    public class NavigationPropertyManager : INavigationPropertyManager
    {
        private readonly INamingConventionManager _namingConventionManager;

        public NavigationPropertyManager(INamingConventionManager namingConventionManager)
        {
            _namingConventionManager = namingConventionManager;
        }

        public DynamicForiegnKeyPropertyMetadata GetDynamicForiegnKeyPropertyMetadata(DynamicEntityMetadata dynamicEntityMetadata, DynamicComplexPropertyMetadata dynamicComplexPropertyMetadata)
        {
            var dynamicForiegnKeyPropertyMetadatas = dynamicEntityMetadata.DynamicPropertyMetadatas.Where(x => x.GetType() == typeof(DynamicForiegnKeyPropertyMetadata)).Select(x => (DynamicForiegnKeyPropertyMetadata)x).ToList();
            dynamicForiegnKeyPropertyMetadatas = dynamicForiegnKeyPropertyMetadatas
                .Where(x => x.ComplexEntityPropertyMetadata.TypeName == dynamicComplexPropertyMetadata.TypeName).ToList();
            if (dynamicForiegnKeyPropertyMetadatas.Count == 0)
                throw new Exception("Dynamic Foriegn key cannot be found in entity " + dynamicEntityMetadata.TypeName + " for property " + dynamicComplexPropertyMetadata.TypeName);

            if (dynamicForiegnKeyPropertyMetadatas.Count == 1)
                return dynamicForiegnKeyPropertyMetadatas.First();

            dynamicForiegnKeyPropertyMetadatas = dynamicForiegnKeyPropertyMetadatas.Where(x =>
                x.PropertyName.Contains(
                    dynamicComplexPropertyMetadata.PropertyName)).ToList();

            if (dynamicForiegnKeyPropertyMetadatas.Count == 1)
                return dynamicForiegnKeyPropertyMetadatas.First();

            throw new Exception("Dynamic Foriegn key cannot be found in entity " + dynamicEntityMetadata.TypeName + " for property " + dynamicComplexPropertyMetadata.TypeName + " because key property name does not contain complex property name");

        }

        public DynamicPropertyMetadata GetCollectionProperty(DynamicEntityMetadata dynamicEntityMetadata, DynamicPropertyMetadata dynamicPropertyMetadata)
        {
            var collectionProperties = dynamicEntityMetadata.DynamicPropertyMetadatas.Where(x => x.IsCollection
                        && x.TypeName == dynamicEntityMetadata.TypeName).Select(x => (DynamicCollectionEntityPropertyMetadata)x).ToList();

            if (collectionProperties.Count == 0)
                return null;

            if (collectionProperties.Count == 1)
                return collectionProperties.First();

            collectionProperties = collectionProperties.Where(x => x.InverseProperty != null && x.InverseProperty == dynamicPropertyMetadata.PropertyName).ToList();

            if (collectionProperties.Count == 1)
                return collectionProperties.First();

            if (collectionProperties.Count == 0)
                return null;

            throw new Exception("Collection cannot be found in entity " + dynamicEntityMetadata.TypeName + " for property " + dynamicPropertyMetadata.TypeName);

        }

        public string GetForiegnKeyNameByCollectionProperty(DynamicEntityMetadata dynamicEntityMetadata, string typeName, DynamicCollectionEntityPropertyMetadata dynamicCollectionEntityPropertyMetadata)
        {
            if (dynamicCollectionEntityPropertyMetadata.InverseProperty == null)
            {
                var complexProperties =
                    dynamicEntityMetadata.DynamicPropertyMetadatas.Where(x => x.TypeName == typeName).ToList();
                if (complexProperties.Count == 1)
                    return _namingConventionManager.GetForiegnKeyByComplexProperty(dynamicEntityMetadata.TypeName, complexProperties.First().PropertyName);
                if (complexProperties.Count == 0)
                    throw new Exception("GetForiegnKeyNameByCollectionProperty could not find complex property for " +
                                        dynamicCollectionEntityPropertyMetadata.PropertyName +
                                        " inside " + typeName);
                else
                    throw new Exception("GetForiegnKeyNameByCollectionProperty could not find complex property for " +
                                        dynamicCollectionEntityPropertyMetadata.PropertyName +
                                        " because two or more  " + typeName + " exists");
            }
            else
            {
                var fkName = _namingConventionManager.GetForiegnKeyByComplexProperty(dynamicEntityMetadata.TypeName,
                    dynamicCollectionEntityPropertyMetadata.InverseProperty);
                if (dynamicEntityMetadata.DynamicPropertyMetadatas.All(x => x.PropertyName != fkName))
                {
                    throw new Exception("GetForiegnKeyNameByCollectionProperty could not find inverse property for " +
                                        dynamicCollectionEntityPropertyMetadata.InverseProperty);
                }
                return fkName;
            }
        }

        public bool IsForeignKey(EntityPropertyMetadata entityPropertyMetadata)
        {
            var potentialForeignKeyNames = entityPropertyMetadata.EntityMetadata.EntityPropertyMetadata.Where(x => x.IsComplexEntity).Select(x => _namingConventionManager.GetForiegnKeyByComplexProperty(entityPropertyMetadata.EntityMetadata.TypeName, x.PropertyName)).ToList();
            var foreignKeyNames = entityPropertyMetadata.EntityMetadata.EntityPropertyMetadata.Where(x => potentialForeignKeyNames.Contains(x.PropertyName)).Select(x => x.PropertyName).ToList();
            return foreignKeyNames.Contains(entityPropertyMetadata.PropertyName);
        }
    }
}