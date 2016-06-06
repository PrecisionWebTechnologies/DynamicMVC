using System.Collections.Generic;
using System.Linq;
using DynamicMVC.DynamicEntityMetadataLibrary.Interfaces;
using DynamicMVC.DynamicEntityMetadataLibrary.Models;
using DynamicMVC.EntityMetadataLibrary.Models;
using DynamicMVC.Shared.Extensions;

namespace DynamicMVC.DynamicEntityMetadataLibrary.Strategies.DynamicEntityMetadataBuilderHelper
{
    /// <summary>
    /// Sets ListIncludes and InstanceIncludes
    /// </summary>
    public class IncludesBuilderHelper : IDynamicEntityMetadataBuilderHelper
    {
        public void Build(DynamicEntityMetadata dynamicEntityMetadata, EntityMetadata entityMetadata)
        {
            dynamicEntityMetadata.ListIncludes = GetListIncludes(dynamicEntityMetadata, entityMetadata).ToList();
            dynamicEntityMetadata.InstanceIncludes = GetInstanceIncludes(dynamicEntityMetadata, entityMetadata).ToList();
        }

        private IEnumerable<string> GetListIncludes(DynamicEntityMetadata dynamicEntityMetadata, EntityMetadata entityMetadata)
        {
            if (entityMetadata.DynamicEntityAttribute().ListIncludes != null)
                return entityMetadata.DynamicEntityAttribute().ListIncludes.SplitAndTrim();
            return dynamicEntityMetadata.DynamicPropertyMetadatas.Where(x => x.IsComplexEntity).Select(x => x.PropertyName);

        }

        private IEnumerable<string> GetInstanceIncludes(DynamicEntityMetadata dynamicEntityMetadata, EntityMetadata entityMetadata)
        {
            if (entityMetadata.DynamicEntityAttribute().InstanceIncludes != null)
                return entityMetadata.DynamicEntityAttribute().InstanceIncludes.SplitAndTrim();
            return dynamicEntityMetadata.DynamicPropertyMetadatas.Where(x => x.IsComplexEntity).Select(x => x.PropertyName);
        }
    }
}
