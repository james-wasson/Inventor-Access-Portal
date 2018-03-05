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
        public static List<RecentActivitesDataItem> GetRecentActivites(CachedUser user, DbContext context)
        {
            if (user == null) return new List<RecentActivitesDataItem>();
            var InvestigatorNames = context.All_Investigators.Where(p => p.Investigator == user.InvestigatorName).Select(p => p.Investigator).ToList();
            // finds it project number has a investigator that is the same as the CachedUser
            var projectNumbers = context.File_Numbers.Where(p =>
                    p.Project_Numbers.All_Investigators
                        .Select(q => q.Investigator)
                        .Any(q => InvestigatorNames.Contains(q))
                );

            return projectNumbers.Select(p =>
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
        public static InventionsData GetInventionsForm(CachedUser user, DbContext context)
        {
            if (user == null) return new InventionsData();
            return new InventionsData()
            {
                ProjectNumber = context.Project_Numbers.Where(p => p.Investigator.Investigator_Name == user.InvestigatorName).ToList()
            };
        }

        /// <summary>
        /// Pulling data from database for FilesFormModel
        /// </summary>
        /// <param UserClass="user"></param>
        /// <param DatabaseContext="context"></param>
        /// <returns>Two list of related information</returns>
        public static FilesData GetFilesForm(CachedUser user, DbContext context)
        {
            if (user == null) return new FilesData();
            return new FilesData()
            {
                FileNumbers = context.File_Numbers.Where(p => p.Project_Numbers.Investigator.Investigator_Name == user.InvestigatorName).ToList()
            };
        }

        /// <summary>
        /// Pulling data from database for FamiliesFormModel
        /// </summary>
        /// <param UserClass="user"></param>
        /// <param DatabaseContext="context"></param>
        /// <returns>Two list of related information</returns>
        public static List<FamiliesDataItem> GetFamiliesForm(CachedUser user, DbContext context)
        {
            if (user == null) return new List<FamiliesDataItem>();
            var fileNums = context.File_Numbers.Where(p => p.Project_Numbers.Investigator.Investigator_Name == user.InvestigatorName);
            return fileNums.Select(p => new FamiliesDataItem
            {
                Families = p.Family_Listings.Select(q => q.Family).ToList(),
                FileNumber = p
            }).ToList();
        }
    }
}
