using DynamicMVC.DynamicEntityMetadataLibrary.Models;
using ReflectionLibrary.Interfaces;

namespace DynamicMVC.DynamicEntityMetadataLibrary.Interfaces
{
    public interface INavigationPropertyManager
    {
        DynamicForiegnKeyPropertyMetadata GetDynamicForiegnKeyPropertyMetadata(DynamicEntityMetadata dynamicEntityMetadata, DynamicComplexPropertyMetadata dynamicComplexPropertyMetadata);
        DynamicPropertyMetadata GetCollectionProperty(DynamicEntityMetadata dynamicEntityMetadata, DynamicPropertyMetadata dynamicPropertyMetadata);
        string GetForiegnKeyNameByCollectionProperty(DynamicEntityMetadata entityMetadata, string typeName, DynamicCollectionEntityPropertyMetadata dynamicCollectionEntityPropertyMetadata);
        bool IsForeignKey(IReflectedProperty reflectedProperty, IReflectedClass reflectedClass);
    }
}