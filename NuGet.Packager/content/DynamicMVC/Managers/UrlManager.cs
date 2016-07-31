using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using DynamicMVC.UI.DynamicMVC.Interfaces;

namespace DynamicMVC.UI.DynamicMVC.Managers
{
    public class UrlManager : IUrlManager
    {
        public UrlManager()
        {
            Url = new UrlHelper(HttpContext.Current.Request.RequestContext, RouteTable.Routes);
        }

        public UrlHelper Url { get; set; }
    }
}
