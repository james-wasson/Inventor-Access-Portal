using System.Reflection;
using Abp.Modules;

namespace InventorAccessPortal
{
    public class InventorAccessPortalCoreModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
