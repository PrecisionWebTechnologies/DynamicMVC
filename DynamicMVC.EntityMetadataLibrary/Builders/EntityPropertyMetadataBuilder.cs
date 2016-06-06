using System.Collections.Generic;
using System.Linq;
using DynamicMVC.ApplicationMetadataLibrary.Models;
using DynamicMVC.EntityMetadataLibrary.Interfaces;
using DynamicMVC.EntityMetadataLibrary.Models;
using DynamicMVC.Shared.Extensions;

namespace DynamicMVC.EntityMetadataLibrary.Builders
{
    public class EntityPropertyMetadataBuilder : IEntityPropertyMetadataBuilder
    {
        public IEnumerable<EntityPropertyMetadata> Build(ApplicationEntity applicationEntity)
        {
            var result = new List<EntityPropertyMetadata>();
            foreach (var applicationEntityProperty in applicationEntity.ApplicationEntityProperties)
            {
                var applicationEntityMetadataProperty = applicationEntity.ApplicationEntityMetadata.ApplicationEntityMetadataProperty.SingleOrDefault(x => x.PropertyName == applicationEntityProperty.PropertyName);

                var entityPropertyMetadata = new EntityPropertyMetadata();
                entityPropertyMetadata.PropertyName = applicationEntityProperty.PropertyName;
                entityPropertyMetadata.TypeName = applicationEntityProperty.DynamicTypeName;
                entityPropertyMetadata.GetValueFunction = applicationEntityProperty.GetValueFunction;
                entityPropertyMetadata.SetValueAction = applicationEntityProperty.SetValueAction;
                if (applicationEntityProperty.IsSimple)
                {
                    entityPropertyMetadata.ParseValue = str => applicationEntityProperty.SimpleTypeParser.Parse(str);
                    entityPropertyMetadata.SimpleTypeEnum = applicationEntityProperty.SimpleTypeParser.SimpleTypeEnum();
                }
                //copy attributes from property
                entityPropertyMetadata.EntityPropertyAttributes.AddRange(applicationEntityProperty.Attributes);
                if (applicationEntityMetadataProperty != null)
                {
                    foreach (var attribute in applicationEntityMetadataProperty.Attributes)
                    {
                        if (entityPropertyMetadata.EntityPropertyAttributes.All(x => x.GetType() != attribute.GetType()))
                        {
                            entityPropertyMetadata.EntityPropertyAttributes.Add(attribute);
                        }
                    }    
                }
                
                entityPropertyMetadata.IsSimple = applicationEntityProperty.IsSimple;
                entityPropertyMetadata.IsCollection = applicationEntityProperty.IsDynamicCollection;
                entityPropertyMetadata.IsComplexEntity = applicationEntityProperty.IsComplexEntity;
                entityPropertyMetadata.IsNullableType = applicationEntityProperty.IsNullable;
                result.Add(entityPropertyMetadata);
            }
            return result;
        }
    }
}
