using System.Linq;
using DynamicMVC.DynamicEntityMetadataLibrary.Interfaces;
using DynamicMVC.DynamicEntityMetadataLibrary.Models;
using DynamicMVC.EntityMetadataLibrary.Models;

namespace DynamicMVC.DynamicEntityMetadataLibrary.Strategies.DynamicEntityMetadataBuilderHelper
{
    /// <summary>
    /// 
    /// </summary>
    public class DynamicMethodsBuilderHelper : IDynamicEntityMetadataBuilderHelper
    {
        private readonly IDynamicMethodManager _dynamicMethodManager;

        public DynamicMethodsBuilderHelper(IDynamicMethodManager dynamicMethodManager)
        {
            _dynamicMethodManager = dynamicMethodManager;
        }

        public void Build(DynamicEntityMetadata dynamicEntityMetadata, EntityMetadata entityMetadata)
        {
            dynamicEntityMetadata.DynamicMethods = _dynamicMethodManager.GetDynamicMethods(entityMetadata.ReflectedClass).ToList();
        }
    }
}
