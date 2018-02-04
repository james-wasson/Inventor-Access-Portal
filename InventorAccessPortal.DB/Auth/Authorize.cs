using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventorAccessPortal.DB.Objects;

namespace InventorAccessPortal.DB.Auth
{
    public static class Authorize
    {
        public static bool LoginData(String username, Context context = null)
        {
            if (context == null) { context = new Context(); }
            foreach (var conn in context.GetConnections())
            {
                var Investigator = conn.AllInvestigatorsTableAdapter.GetData().AsQueryable().FirstOrDefault(p => p.ID == 153);
                Console.Write("{0}", Investigator.Investigator);
            }
            return false;
        }
    }
}
