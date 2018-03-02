using System.Web;
using System.Web.Optimization;

namespace InventorAccessPortal.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            /*
             * DEV Bundles
             */

            bundles.Add(new ScriptBundle("~/Content/css")
                .IncludeDirectory("~/Content/Dev/", "*.css", false));

            bundles.Add(new ScriptBundle("~/bundles/scripts")
                .IncludeDirectory("~/Scripts/Dev/", "*.js", false));

            /*
            * Libaray Bundles 
            */

            bundles.Add(new ScriptBundle("~/bundles/Lib/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/Lib/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/Lib/modernizr").Include(
                        "~/Scripts/modernizr-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/Lib/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/Lib/css").Include(
                      "~/Content/bootstrap.css"
                      ));
        }
    }
}
