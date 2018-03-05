using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace InventorAccessPortal.DB.Objects
{
    public class RecentActivitesDataItem
    {
        public File_Number FileNumber;
        public List<Transaction> Transactions = new List<Transaction>();
    }
}
