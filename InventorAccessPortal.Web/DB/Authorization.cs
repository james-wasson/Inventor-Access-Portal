using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventorAccessPortal.Web.DB
{
    public static class Authorization
    {
        public static void check(Context DBContext) {
            if (DBContext == null) DBContext = new Context();
            var a = DBContext.GetConnections();
        }
    }
}