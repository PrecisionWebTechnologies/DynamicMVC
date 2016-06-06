using System;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using DynamicMVC.UI.DynamicMVC.Extensions;
using DynamicMVC.UI.DynamicMVC.Interfaces;
using DynamicMVC.UI.DynamicMVC.ViewModels.DynamicControls;

namespace DynamicMVC.UI.DynamicMVC.Helpers
{
    public static class Helpers
    {
        //ToDo:  This logic needs to be moved behind the view so it can be unit tested.
        public static HtmlString DynamicSortNameActionLink(this AjaxHelper helper, string expression, string actionName, string partialActionName, string controllerName, RouteValueDictionaryWrapper routeValueDictionaryWrapper, AjaxOptions options, string sortExpression = null)
        {
            var htmlHelper = new HtmlHelper(helper.ViewContext, helper.ViewDataContainer);
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            var columnHeader = htmlHelper.DisplayName(expression).ToString();

            if (helper.ViewDataContainer.ViewData.Model is DynamicTableHeaderViewModel)
            {
                columnHeader = ((DynamicTableHeaderViewModel)helper.ViewDataContainer.ViewData.Model).DisplayName;

            }

            routeValueDictionaryWrapper = routeValueDictionaryWrapper.Clone();
            if (sortExpression == null)
                sortExpression = expression;
            if (routeValueDictionaryWrapper.ContainsKey("OrderBy") && routeValueDictionaryWrapper.GetValue("OrderBy").ToString() == sortExpression)
                routeValueDictionaryWrapper.SetValue("OrderBy", sortExpression + " descending");
            else
                routeValueDictionaryWrapper.SetValue("OrderBy", sortExpression);

            routeValueDictionaryWrapper.SetValue("Page", 1);
            options.Url = urlHelper.Action(partialActionName, controllerName, routeValueDictionaryWrapper.GetRouteValueDictionary());
            return helper.ActionLink(columnHeader, actionName, controllerName, routeValueDictionaryWrapper.GetRouteValueDictionary(), options);
        }

        public static HtmlString DynamicEditor(this HtmlHelper helper, DynamicEditorViewModel dynamicEditorViewModel)
        {
            // ReSharper disable once RedundantAnonymousTypePropertyName
            return helper.Editor(dynamicEditorViewModel.ViewModelPropertyName, dynamicEditorViewModel.DynamicEditorName, new { DynamicPropertyEditorViewModel = dynamicEditorViewModel.DynamicPropertyEditorViewModel });
        }

        public static HtmlString DynamicLabelFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, IDynamicDisplayName dynamicDisplayName)
        {
            if (!string.IsNullOrWhiteSpace(dynamicDisplayName.DisplayName) && html.ViewData.ModelMetadata.DisplayName == null)
            {
                html.ViewData.ModelMetadata.DisplayName = dynamicDisplayName.DisplayName;
            }

            return html.LabelFor(expression);
        }

        public static string GetDisplayName(this HtmlHelper html, IDynamicDisplayName dynamicDisplayName)
        {
            //if (!string.IsNullOrWhiteSpace(dynamicDisplayName.DisplayName))
            //    return dynamicDisplayName.DisplayName;

            return html.DisplayName(dynamicDisplayName.ViewModelPropertyName).ToString();
        }
        //ToDo:  Delete this after release.  This was logic used for above
        //public string GetDisplayName(HtmlHelper html)
        //{
        //    if (!string.IsNullOrWhiteSpace(DisplayName) && PropertyName == html.DisplayName(ViewModelPropertyName).ToString())
        //    {
        //        return DisplayName;
        //    }
        //    return html.DisplayName(ViewModelPropertyName).ToString();
        //}
    }
}