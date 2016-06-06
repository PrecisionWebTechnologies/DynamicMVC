using DynamicMVC.Annotations;
using DynamicMVC.DynamicEntityMetadataLibrary.Models;

namespace DynamicMVC.UI.DynamicMVC.Interfaces
{
    public interface IDynamicFilterFactory
    {
        IDynamicFilter GetDynamicFilter(string dynamicFilterViewName, DynamicPropertyMetadata dynamicPropertyMetadata, RouteValueDictionaryWrapper routeValueDictionaryWrapper);
        IDynamicFilter GetDynamicFilter(DynamicFilterUIHintAttribute dynamicFilterUIHintAttribute, DynamicPropertyMetadata dynamicPropertyMetadata, RouteValueDictionaryWrapper routeValueDictionaryWrapper);

    }
}