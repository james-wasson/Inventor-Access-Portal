using Abp.Application.Services;

namespace InventorAccessPortal
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class InventorAccessPortalAppServiceBase : ApplicationService
    {
        protected InventorAccessPortalAppServiceBase()
        {
            LocalizationSourceName = InventorAccessPortalConsts.LocalizationSourceName;
        }
    }
}