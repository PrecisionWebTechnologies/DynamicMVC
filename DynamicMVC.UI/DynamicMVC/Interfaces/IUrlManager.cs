using System.Web.Mvc;

namespace DynamicMVC.UI.DynamicMVC.Interfaces
{
    public interface IUrlManager
    {
        UrlHelper Url { get; set; }
    }
}