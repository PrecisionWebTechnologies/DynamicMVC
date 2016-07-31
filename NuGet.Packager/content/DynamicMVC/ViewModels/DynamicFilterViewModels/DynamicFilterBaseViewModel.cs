using System.Collections.Generic;
using System.Linq;
using DynamicLinqExtensions;
using DynamicMVC.DynamicEntityMetadataLibrary.Models;
using DynamicMVC.UI.DynamicMVC.Interfaces;

namespace DynamicMVC.UI.DynamicMVC.ViewModels.DynamicFilterViewModels
{
    public abstract class DynamicFilterBaseViewModel : IDynamicFilter
    {
        public virtual IQueryable Filter(IQueryable qry)
        {
            if (RouteValueDictionaryWrapper.ContainsKey(QueryStringName))
            {
                return qry.DynamicWhere(PropertyName, RouteValueDictionaryWrapper.GetValue(QueryStringName));
            }
            return qry;
        }

        public virtual void ViewModelCreated(DynamicPropertyMetadata dynamicPropertyMetadata, IDictionary<string, object> controlParameters)
        {
            LabelText = dynamicPropertyMetadata.DisplayName;
            QueryStringName = PropertyName;
            if (controlParameters.ContainsKey("QueryStringName"))
                QueryStringName = controlParameters["QueryStringName"].ToString();
            
            if (controlParameters.ContainsKey("LabelText"))
                LabelText = controlParameters["LabelText"].ToString();
        }

        public string PropertyName { get; set; }
        public RouteValueDictionaryWrapper RouteValueDictionaryWrapper { get; set; }
        public string LabelText { get; set; }
        public string QueryStringName { get; set; }
        public int Order { get; set; }
        public abstract string DynamicFilterViewName();
        public virtual bool FilterIsApplied()
        {
            return RouteValueDictionaryWrapper.ContainsKey(QueryStringName);
        }
    }
}
