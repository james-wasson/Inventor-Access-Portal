using System.Reflection;
using Abp.Application.Services;
using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.WebApi;

namespace InventorAccessPortal
{
    [DependsOn(typeof(AbpWebApiModule), typeof(InventorAccessPortalApplicationModule))]
    public class InventorAccessPortalWebApiModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            Configuration.Modules.AbpWebApi().DynamicApiControllerBuilder
                .ForAll<IApplicationService>(typeof(InventorAccessPortalApplicationModule).Assembly, "app")
                .Build();
        }
    }
}
