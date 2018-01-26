using Abp.Web.Mvc.Views;

namespace InventorAccessPortal.Web.Views
{
    public abstract class InventorAccessPortalWebViewPageBase : InventorAccessPortalWebViewPageBase<dynamic>
    {

    }

    public abstract class InventorAccessPortalWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected InventorAccessPortalWebViewPageBase()
        {
            LocalizationSourceName = InventorAccessPortalConsts.LocalizationSourceName;
        }
    }
}