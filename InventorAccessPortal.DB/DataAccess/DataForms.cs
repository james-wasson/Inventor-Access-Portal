using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventorAccessPortal.DB.Objects;

namespace InventorAccessPortal.DB.DataAccess
{
    public class DataForms
    {
        /// <summary>
        /// Pulling data from database for RecentActivitiesModel
        /// </summary>
        /// <param UserClass="user"></param>
        /// <param DatabaseContext="context"></param>
        /// <returns>Three list of related information</returns>
        public static List<RecentActivitesDataItem> GetRecentActivites(CachedUser user, EntityContext context)
        {
            if (user == null) return new List<RecentActivitesDataItem>();
            // finds it project number has a investigator that is the same as the CachedUser
            var fileNumbers = ByUser.GetFileNumber(user, context);

            return fileNumbers.Select(p =>
                new RecentActivitesDataItem()
                {
                    FileNumber = p,
                    Transactions = p.Transactions.ToList()
                }
            ).ToList();
        }

        /// <summary>
        /// Pulling data from database for InventionsFormModel
        /// </summary>
        /// <param UserClass="user"></param>
        /// <param DatabaseContext="context"></param>
        /// <returns>Two list of related information</returns>
        public static InventionsData GetInventionsForm(CachedUser user, EntityContext context)
        {
            if (user == null) return new InventionsData();
            return new InventionsData()
            {
                ProjectNumber = context.Project_Numbers.Where(p =>
                    p.All_Investigators.Select(q => q.Investigator).Contains(user.InvestigatorName)
                ).ToList()
            };
        }

        /// <summary>
        /// Pulling data from database for FilesFormModel
        /// </summary>
        /// <param UserClass="user"></param>
        /// <param DatabaseContext="context"></param>
        /// <returns>Two list of related information</returns>
        public static FilesData GetFilesForm(CachedUser user, EntityContext context)
        {
            if (user == null) return new FilesData();
            return new FilesData()
            {
                FileNumbers = ByUser.GetFileNumber(user, context).ToList()
        };
        }

        /// <summary>
        /// Pulling data from database for FamiliesFormModel
        /// </summary>
        /// <param UserClass="user"></param>
        /// <param DatabaseContext="context"></param>
        /// <returns>family with FileNumbers related to that family</returns>
        public static List<FamiliesDataItem> GetFamiliesForm(CachedUser user, EntityContext context)
        {
            if (user == null) return new List<FamiliesDataItem>();
            var families = ByUser.GetFamilies(user, context);

            return families.Select(p => new FamiliesDataItem
            {
                Family = p,
                FileNumbers = p.Family_Listings.Select(q => q.File_Numbers).ToList()
            }).ToList();
        }
    }
}
