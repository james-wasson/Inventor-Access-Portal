using System.Data.Entity.Migrations;

namespace InventorAccessPortal.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<InventorAccessPortal.EntityFramework.InventorAccessPortalDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "InventorAccessPortal";
        }

        protected override void Seed(InventorAccessPortal.EntityFramework.InventorAccessPortalDbContext context)
        {
            // This method will be called every time after migrating to the latest version.
            // You can add any seed data here...
        }
    }
}
