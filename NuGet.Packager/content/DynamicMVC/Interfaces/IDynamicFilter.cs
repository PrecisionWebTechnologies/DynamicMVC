using System.Collections.Generic;
using System.Linq;
using DynamicMVC.DynamicEntityMetadataLibrary.Models;

namespace DynamicMVC.UI.DynamicMVC.Interfaces
{
    public interface IDynamicFilter
    {
        IQueryable Filter(IQueryable qry);
        void ViewModelCreated(DynamicPropertyMetadata dynamicPropertyMetadata, IDictionary<string, object> controlParameters);
        string PropertyName { get; set; }
        RouteValueDictionaryWrapper RouteValueDictionaryWrapper { get; set; }
        int Order { get; set; }
        string QueryStringName { get; set; }
        string DynamicFilterViewName();
        bool FilterIsApplied();
    }
}
