using System.Web;
using System.Web.Optimization;

namespace DynamicMVC.UI
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/select2.js",
                        "~/Scripts/DynamicScript.js",
                        "~/Scripts/jquery-ui-1.11.1.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                                 "~/Scripts/jquery.validate*",
                                 "~/Scripts/jquery.unobtrusive-ajax.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/bundle/css").Include(
                    "~/Content/bootstrap.css",
                    "~/Content/DynamicStyle.css",
                    "~/Content/site.css",
                    "~/Content/Validation.css",
                    "~/Content/css/select2.css",
                    "~/Content/select2custom.css"));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;
        }
    }
}
