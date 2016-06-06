using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using DynamicMVC.DynamicEntityMetadataLibrary.Interfaces;
using DynamicMVC.DynamicEntityMetadataLibrary.Models;
using DynamicMVC.EntityMetadataLibrary.Models;
using DynamicMVC.Shared.Extensions;

namespace DynamicMVC.DynamicEntityMetadataLibrary.Strategies.DynamicPropertyMetadataBuilderHelper
{
    public class SetDisplayName : IDynamicPropertyMetadataBuilderHelper
    {
        public void Build(EntityMetadata entityMetadata, EntityPropertyMetadata entityPropertyMetadata, DynamicPropertyMetadata dynamicPropertyMetadata)
        {
            var displayNameAttribute = entityPropertyMetadata.EntityPropertyAttributes.GetFirstAttribute<DisplayNameAttribute>();
            var displayAttribute = entityPropertyMetadata.EntityPropertyAttributes.GetFirstAttribute<DisplayAttribute>();
            if (displayNameAttribute != null)
            {
                dynamicPropertyMetadata.DisplayName = displayNameAttribute.DisplayName;
            }
            else if (displayAttribute != null)
            {
                dynamicPropertyMetadata.DisplayName = displayAttribute.Name;
            }
            else
            {
                dynamicPropertyMetadata.DisplayName = entityPropertyMetadata.PropertyName;       
            }
        }
    }
}
