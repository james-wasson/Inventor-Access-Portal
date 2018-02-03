using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(InventorAccessPortal.Web.Startup))]
namespace InventorAccessPortal.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
