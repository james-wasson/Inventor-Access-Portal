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
                .IncludeDirectory("~/Content/", "*.css", false));

            bundles.Add(new ScriptBundle("~/bundles/scripts")
                .IncludeDirectory("~/Scripts/", "*.js", false));

            /*
            * Libaray Bundles 
            */

            bundles.Add(new ScriptBundle("~/bundles/Lib/jquery").Include(
                        "~/Scripts/Lib/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/Lib/jqueryval").Include(
                        "~/Scripts/Lib/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/Lib/modernizr").Include(
                        "~/Scripts/Lib/modernizr-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/Lib/bootstrap").Include(
                      "~/Scripts/Lib/bootstrap.js",
                      "~/Scripts/Lib/respond.js"));

            bundles.Add(new StyleBundle("~/Content/Lib/css").Include(
                      "~/Content/Lib/bootstrap.css"
                      ));
        }
    }
}
