using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventorAccessPortal.DB.Objects;

namespace InventorAccessPortal.DB.Model
{
    public class Home
    {
        public static RecentActivites GetRecentActivites(CachedUser user, DbContext context) {
            var AllInvetigators = context.All_Investigators.Where(p => p.Investigator == user.InvestigatorName).ToList();
            return new RecentActivites()
            {
                AllInvestigators = AllInvetigators
            };
        }
    }
}
