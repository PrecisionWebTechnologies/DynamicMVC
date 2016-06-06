using System.Collections.Generic;
using System.Collections.Specialized;

namespace DynamicMVC.DynamicEntityMetadataLibrary.Models
{
    public class DynamicOperation
    {
        public DynamicOperation()
        {
            PersistModel = true;
            ReturnSucessfulRedirect = true;
        }
        public bool PersistModel { get; set; }
        public bool ReturnSucessfulRedirect { get; set; }
        public DynamicMethod DynamicMethod { get; set; }

        public void PerformPreSaveOperation(dynamic id, NameValueCollection formCollection, dynamic model,ref string returnUrl, IDictionary<string, object> tempData, IDictionary<string, object> viewData)
        {
            if (DynamicMethod != null)
            {
                DynamicMethod.InvokePreSaveOperation(id, formCollection, model,ref returnUrl, tempData, viewData);
                PersistModel = DynamicMethod.PersistModel();
            }
        }

        public void PeformPostSaveOperation(dynamic id, NameValueCollection formCollection, dynamic model,ref string returnUrl, IDictionary<string, object> tempData, IDictionary<string, object> viewData)
        {
            if (DynamicMethod != null)
            {
                DynamicMethod.InvokePostSaveOperation(id, formCollection, model,ref returnUrl, tempData, viewData);
                ReturnSucessfulRedirect = DynamicMethod.ReturnSucessfulRedirect();
            }
        }
    }
}
