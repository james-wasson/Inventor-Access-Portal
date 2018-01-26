using System.Reflection;
using Abp.Modules;

namespace InventorAccessPortal
{
    [DependsOn(typeof(InventorAccessPortalCoreModule))]
    public class InventorAccessPortalApplicationModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
