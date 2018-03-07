using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventorAccessPortal.DB.Objects;

namespace InventorAccessPortal.DB.DataAccess
{
    class ByUser
    {
        public static IQueryable<File_Number> GetFileNumber(CachedUser user, DbContext e)
        {
            return e.File_Numbers.Where(p =>
                    p.Project_Numbers.All_Investigators
                        .Select(q => q.Investigator)
                        .Contains(user.InvestigatorName)
                );
        }

        public static IQueryable<Family> GetFamilies(CachedUser user, DbContext e)
        {
            return e.Families.Where(p =>
                p.Family_Listings.Where(q =>
                    q.File_Numbers.Project_Numbers.All_Investigators
                        .Select(y => y.Investigator).Contains(user.InvestigatorName)
                ).Count() > 0
            );
        }
    }
}
