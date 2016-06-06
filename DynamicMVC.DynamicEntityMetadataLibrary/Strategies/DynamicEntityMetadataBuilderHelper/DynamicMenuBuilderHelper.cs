using System.Linq;
using DynamicMVC.Annotations;
using DynamicMVC.DynamicEntityMetadataLibrary.Interfaces;
using DynamicMVC.DynamicEntityMetadataLibrary.Models;
using DynamicMVC.EntityMetadataLibrary.Models;
using DynamicMVC.Shared.Interfaces;

namespace DynamicMVC.DynamicEntityMetadataLibrary.Strategies.DynamicEntityMetadataBuilderHelper
{
    /// <summary>
    /// Sets any properties related to dynamic menus
    /// </summary>
    public class DynamicMenuBuilderHelper : IDynamicEntityMetadataBuilderHelper
    {
        private readonly INamingConventionManager _namingConventionManager;

        public DynamicMenuBuilderHelper(INamingConventionManager namingConventionManager)
        {
            _namingConventionManager = namingConventionManager;
        }

        public void Build(DynamicEntityMetadata dynamicEntityMetadata, EntityMetadata entityMetadata)
        {
            dynamicEntityMetadata.DynamicMenuInfo = new DynamicMenuInfo();
            dynamicEntityMetadata.DynamicMenuInfo.HasMenuItem = !entityMetadata.EntityAttributes.OfType<DynamicMenuItemExcludeAttribute>().Any();
            dynamicEntityMetadata.DynamicMenuInfo.MenuItemCategory = MenuItemCategory(entityMetadata);
            dynamicEntityMetadata.DynamicMenuInfo.MenuItemDisplayName = MenuItemDisplayName(entityMetadata);
        }

        private string MenuItemCategory(EntityMetadata entityMetadata)
        {
            var dynamicMenuItemAttribute = entityMetadata.EntityAttributes.OfType<DynamicMenuItemAttribute>().SingleOrDefault();
            return dynamicMenuItemAttribute == null ? _namingConventionManager.DynamicMenuCategory() : dynamicMenuItemAttribute.CategoryName;
        }

        private string MenuItemDisplayName(EntityMetadata entityMetadata)
        {
            var dynamicMenuItemAttribute = entityMetadata.EntityAttributes.OfType<DynamicMenuItemAttribute>().FirstOrDefault();
            return dynamicMenuItemAttribute != null ? dynamicMenuItemAttribute.DisplayName : entityMetadata.TypeName;
        }
    }
}
