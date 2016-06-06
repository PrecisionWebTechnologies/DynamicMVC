using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using DynamicMVC.Annotations.Enums;
using DynamicMVC.DynamicEntityMetadataLibrary.Interfaces;

namespace DynamicMVC.DynamicEntityMetadataLibrary.Models
{
    public class DynamicMethod
    {
        public string MethodName { get; set; }
        public string ButtonText { get; set; }
        public string SubmitValue { get; set; }
        public string RedirectUrl { get; set; }
        public TemplateTypeEnum TemplateTypeEnum { get; set; }
        public IDynamicMethodInvoker DynamicMethodInvoker { get; set; }

        /// <summary>
        ///  Function with the following signature - object Invoke(object obj, object[] parameters)
        /// </summary>
        public Func<object, object[], object> InvokeMethodFunction { get; set; }

        public void InvokePreSaveOperation(dynamic id, NameValueCollection formCollection, dynamic model,ref string returnUrl, IDictionary<string, object> tempData, IDictionary<string, object> viewData)
        {
            DynamicMethodInvoker.InvokeMethodPreSaveOperation(InvokeMethodFunction, id, formCollection, model,ref returnUrl, tempData, viewData);
        }

        public void InvokePostSaveOperation(dynamic id, NameValueCollection formCollection, dynamic model,ref string returnUrl, IDictionary<string, object> tempData, IDictionary<string, object> viewData)
        {
            DynamicMethodInvoker.InvokePostSaveOperation(InvokeMethodFunction, id, formCollection, model,ref returnUrl, tempData, viewData);
        }

        public bool PersistModel()
        {
            return DynamicMethodInvoker.PersistModel;
        }

        public bool ReturnSucessfulRedirect()
        {
            return DynamicMethodInvoker.ReturnSucessfulRedirect;
        }
    }
}
