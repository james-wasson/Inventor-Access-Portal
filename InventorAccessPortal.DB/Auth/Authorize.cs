using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventorAccessPortal.DB._DB1_DataSetTableAdapters;

namespace InventorAccessPortal.DB.Auth
{
    public class Authorize : Context
    {
        public bool ByUsername(String username) {
            All_InvestigatorsTableAdapter users = new All_InvestigatorsTableAdapter();
            var data = users.GetData();
            foreach (var row in data.Rows)
            {
                var a = row;
            }
            return false;
        }
    }
}
