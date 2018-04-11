using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;

namespace InventorAccessPortal.Web.Extennsions
{
    public static class HtmlHelperExtensions
    {
        public static IHtmlString InlineScripts(this HtmlHelper htmlHelper, string bundleVirtualPath)
        {
            return htmlHelper.InlineBundle(bundleVirtualPath, htmlTagName: "script");
        }

        public static IHtmlString InlineStyles(this HtmlHelper htmlHelper, string bundleVirtualPath)
        {
            return htmlHelper.InlineBundle(bundleVirtualPath, htmlTagName: "style");
        }



        private static IHtmlString InlineBundle(this HtmlHelper htmlHelper, string bundleVirtualPath, string htmlTagName)
        {
            string bundleContent = LoadBundleContent(htmlHelper.ViewContext.HttpContext, bundleVirtualPath);
            string htmlTag = string.Format("<{0}>{1}</{0}>", htmlTagName, bundleContent);

            return new HtmlString(htmlTag);
        }

        private static string LoadBundleContent(HttpContextBase httpContext, string bundleVirtualPath)
        {
            var bundleContext = new BundleContext(httpContext, BundleTable.Bundles, bundleVirtualPath);
            var bundle = BundleTable.Bundles.Single(b => b.Path == bundleVirtualPath);
            var bundleResponse = bundle.GenerateBundleResponse(bundleContext);

            return bundleResponse.Content;
        }

        public static IHtmlString GetBaseUri(this HtmlHelper htmlHelper, HttpContext httpContext)
        {
            return htmlHelper.Raw(httpContext.Request.Url.GetLeftPart(UriPartial.Authority));
        }

        public static IHtmlString DisplaySortableDateTime(this HtmlHelper htmlHelper, DateTime dt) {
            var sortDate = (DateTime.MaxValue.Subtract(dt)).TotalSeconds;
            var displayDate = dt.ToShortDateString();
            var html = string.Format("<span style=\"display:none;\">{0}</span>{1}", sortDate, displayDate);
            return new HtmlString(html);
        }
    }
}