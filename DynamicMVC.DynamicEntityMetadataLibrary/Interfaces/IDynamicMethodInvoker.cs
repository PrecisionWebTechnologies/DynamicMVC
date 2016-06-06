using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace DynamicMVC.DynamicEntityMetadataLibrary.Interfaces
{
    public interface IDynamicMethodInvoker
    {
        string DynamicMethodInvokerName();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="invokeMethodFunction">Function with the following signature - object Invoke(object obj, object[] parameters)</param>
        /// <param name="id"></param>
        /// <param name="formCollection"></param>
        /// <param name="model"></param>
        /// <param name="returnUrl"></param>
        /// <param name="tempData"></param>
        /// <param name="viewData"></param>
        void InvokeMethodPreSaveOperation(Func<object, object[], object> invokeMethodFunction, dynamic id, NameValueCollection formCollection, dynamic model, ref string returnUrl, IDictionary<string, object> tempData, IDictionary<string, object> viewData);

        void InvokePostSaveOperation(Func<object, object[], object> invokeMethodFunction, dynamic id, NameValueCollection formCollection, dynamic model, ref string returnUrl, IDictionary<string, object> tempData, IDictionary<string, object> viewData);

        bool PersistModel { get; set; }
        bool ReturnSucessfulRedirect { get; set; }
    }
}
