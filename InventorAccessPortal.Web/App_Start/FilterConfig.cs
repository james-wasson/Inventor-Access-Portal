using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace InventorAccessPortal.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new AuthorizeAttribute());
#if !DEBUG
            GlobalFilters.Filters.Add(new RequireHttpsAttribute());
            AntiForgeryConfig.RequireSsl = true;
#endif
        }
    }
}
