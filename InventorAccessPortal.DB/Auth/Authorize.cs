using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using InventorAccessPortal.DB._InventorAccessPortal_1DataSetTableAdapters;

namespace InventorAccessPortal.DB.Auth
{
    public class Authorize : Context
    {
        public bool ByUsername(String username) {
            foreach (var conn in GetConnections())
            {
                //All_InvestigatorsTableAdapter users = new All_InvestigatorsTableAdapter();
            }
            return false;
        }
    }
}
