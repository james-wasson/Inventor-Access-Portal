using System.Data.Entity;
using System.Reflection;
using Abp.EntityFramework;
using Abp.Modules;
using InventorAccessPortal.EntityFramework;

namespace InventorAccessPortal
{
    [DependsOn(typeof(AbpEntityFrameworkModule), typeof(InventorAccessPortalCoreModule))]
    public class InventorAccessPortalDataModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = "Default";
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            Database.SetInitializer<InventorAccessPortalDbContext>(null);
        }
    }
}
