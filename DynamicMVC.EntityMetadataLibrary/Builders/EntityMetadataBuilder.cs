using System;
using System.Collections.Generic;
using System.Linq;
using DynamicMVC.Annotations;
using DynamicMVC.ApplicationMetadataLibrary.Models;
using DynamicMVC.EntityMetadataLibrary.Interfaces;
using DynamicMVC.EntityMetadataLibrary.Models;
using DynamicMVC.Shared.Extensions;

namespace DynamicMVC.EntityMetadataLibrary.Builders
{
    public class EntityMetadataBuilder : IEntityMetadataBuilder
    {
        private readonly IEntityPropertyMetadataBuilder _entityPropertyMetadataBuilder;

        public EntityMetadataBuilder(IEntityPropertyMetadataBuilder entityPropertyMetadataBuilder)
        {
            _entityPropertyMetadataBuilder = entityPropertyMetadataBuilder;
        }

        public IEnumerable<EntityMetadata> Build(ApplicationMetadataSummary applicationMetadataSummary)
        {
            var result = new List<EntityMetadata>();
            foreach (var applicationEntity in applicationMetadataSummary.ApplicationEntities)
            {
                var entityMetadata = new EntityMetadata();

                entityMetadata.TypeName = applicationEntity.EntityType.Name;
                entityMetadata.EntityType = applicationEntity.EntityType;
                // ReSharper disable once AccessToForEachVariableInClosure
                entityMetadata.CreateNewObject = () => Activator.CreateInstance(applicationEntity.EntityType);
                entityMetadata.EntityPropertyMetadata.AddRange(_entityPropertyMetadataBuilder.Build(applicationEntity).ToList());
                entityMetadata.ControllerExists = applicationEntity.ApplicationControllerMetadata != null;

                var attributes = applicationEntity.Attributes.ToList();
                foreach (var attribute in applicationEntity.ApplicationEntityMetadata.Attributes)
                {
                    if (attributes.All(x => x.GetType().Name != attribute.GetType().Name))
                    {
                        attributes.Add(attribute);
                    }
                }
                entityMetadata.EntityAttributes.AddRange(attributes);

                //reflectedClass
                var reflectedClass = applicationMetadataSummary.ApplicationEntityReflectedClasses.SingleOrDefault(x => x.Name == entityMetadata.TypeName);
                var reflectedMetadatClass = applicationMetadataSummary.ApplicationEntityMetadataReflectedClasses
                    .SingleOrDefault(
                        x =>
                        {
                            var dynamicEntityAttribute = (DynamicEntityAttribute)x.Attributes.SingleOrDefault(y => y.GetType() == typeof(DynamicEntityAttribute));
                            if (dynamicEntityAttribute != null && dynamicEntityAttribute.EntityType != null)
                            {
                                return dynamicEntityAttribute.EntityType.Name == entityMetadata.TypeName;
                            }
                            return false;
                        });
                if (reflectedClass != null && reflectedMetadatClass != null)
                {
                    reflectedClass.MergeAttributes(reflectedMetadatClass);
                }
                entityMetadata.ReflectedClass = reflectedClass;
                result.Add(entityMetadata);
            }

            return result;
        }
    }
}
