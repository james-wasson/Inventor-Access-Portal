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
            bundles.Add(new StyleBundle("~/Content/css")
                /*.IncludeDirectory("~/Content/css/", "*.css", true)*/
                .IncludeDirectory("~/Content/less/", "*.css", true));

            bundles.Add(new ScriptBundle("~/bundles/scripts")
                .IncludeDirectory("~/Scripts/dev/main", "*.js", true)
                );

            bundles.Add(new ScriptBundle("~/bundles/account/scripts")
                .IncludeDirectory("~/Scripts/dev/account", "*.js", true)
                );

            /*
            * Libaray Bundles 
            */

            bundles.Add(new ScriptBundle("~/bundles/Lib/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/detect-element-resize.js"));

            bundles.Add(new ScriptBundle("~/bundles/Lib/toastr").Include(
                "~/Scripts/toastr.js"));

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
                      "~/Content/bootstrap.css",
                      "~/Content/toastr.css"
                      ));

            bundles.Add(new StyleBundle("~/Content/font-awesome.css").Include(
                "~/Content/font-awesome.css"
                ));

            /*
             * Email Bundles
             * lightweight js and css
             */

            bundles.Add(new StyleBundle("~/Content/Email/css").Include(
                      "~/Mailer/Styles/less/main.css"
                      ));
        }
    }
}

