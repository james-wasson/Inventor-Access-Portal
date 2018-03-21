using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorAccessPortal.DB.Objects
{
    public class NewActionData
    {
        public const int DEFAULT_EXPIRATION_DAYS = 7;
        public String InvestigatorName = null;
        public ActionNumberEnum ActionNumber;
        public Object Model = null;
        public DateTime? Expires = DateTime.Now.AddDays(DEFAULT_EXPIRATION_DAYS);
    }
}
