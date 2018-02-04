using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventorAccessPortal.DB
{
    public static class Authorization
    {
        public static void check(Context DBContext = null) {
            if (DBContext == null) DBContext = new Context();
            var a = DBContext.GetConnections();
        }
    }
}