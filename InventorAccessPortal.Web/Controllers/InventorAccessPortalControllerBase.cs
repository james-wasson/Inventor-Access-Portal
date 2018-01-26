using Abp.Web.Mvc.Controllers;

namespace InventorAccessPortal.Web.Controllers
{
    /// <summary>
    /// Derive all Controllers from this class.
    /// </summary>
    public abstract class InventorAccessPortalControllerBase : AbpController
    {
        protected InventorAccessPortalControllerBase()
        {
            LocalizationSourceName = InventorAccessPortalConsts.LocalizationSourceName;
        }
    }
}