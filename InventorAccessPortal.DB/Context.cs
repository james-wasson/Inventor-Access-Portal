using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventorAccessPortal.DB;

namespace InventorAccessPortal.DB
{
    public class EntityContext : IAP_Entities {} // results in a cleaner, more readable namespace
    public static class ContextCheck
    {
        public static void CheckInit(this EntityContext c)
        {
            if (c == null) c = new EntityContext();
        }
    }
}
