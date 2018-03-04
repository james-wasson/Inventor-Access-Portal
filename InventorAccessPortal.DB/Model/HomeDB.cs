using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventorAccessPortal.DB.Objects;

namespace InventorAccessPortal.DB.Model
{
    public class HomeDB
    {
        /// <summary>
        /// Pulling data from database for RecentActivitiesModel
        /// </summary>
        /// <param UserClass="user"></param>
        /// <param DatabaseContext="context"></param>
        /// <returns>Three list of related information</returns>
        public static RecentActivites GetRecentActivites(CachedUser user, DbContext context)
        {
            List<All_Investigator> AllInvetigators = context.All_Investigators.Where(p => p.Investigator == user.InvestigatorName).ToList();
            List<Project_Number> ProjectNumber = new List<Project_Number>();
            foreach (var data in AllInvetigators)
            {
                ProjectNumber.AddRange(context.Project_Numbers.Where(p => p.Project_Number1 == data.Project_Number).ToList());
            }
            List<File_Number> FileNumber = new List<File_Number>();
            foreach (var data in ProjectNumber)
            {
                FileNumber.AddRange(context.File_Numbers.Where(p => p.Project_Number == data.Project_Number1).ToList());
            }
            List<Transaction> Transactions = new List<Transaction>();
            foreach (var data in FileNumber)
            {
                Transactions.AddRange(context.Transactions.Where(p => p.File_Number == data.File_Number1).ToList());
            }

            return new RecentActivites()
            {
                AllInvestigators = AllInvetigators,
                Transactions = Transactions,
                FileNumbers = FileNumber
            };
        }

        /// <summary>
        /// Pulling data from database for InventionsFormModel
        /// </summary>
        /// <param UserClass="user"></param>
        /// <param DatabaseContext="context"></param>
        /// <returns>Two list of related information</returns>
        public static InventionsForm GetInventionsForm(CachedUser user, DbContext context)
        {
            List<All_Investigator> AllInvetigators = context.All_Investigators.Where(p => p.Investigator == user.InvestigatorName).ToList();
            List<Project_Number> ProjectNumber = new List<Project_Number>();
            foreach (var data in AllInvetigators)
            {
                ProjectNumber.AddRange(context.Project_Numbers.Where(p => p.Project_Number1 == data.Project_Number).ToList());
            }

            return new InventionsForm()
            {
                AllInvestigators = AllInvetigators,
                ProjectNumber = ProjectNumber
            };
        }

        /// <summary>
        /// Pulling data from database for FilesFormModel
        /// </summary>
        /// <param UserClass="user"></param>
        /// <param DatabaseContext="context"></param>
        /// <returns>Two list of related information</returns>
        public static FilesForm GetFilesForm(CachedUser user, DbContext context)
        {
            List<All_Investigator> AllInvetigators = context.All_Investigators.Where(p => p.Investigator == user.InvestigatorName).ToList();
            List<Project_Number> ProjectNumber = new List<Project_Number>();
            foreach (var data in AllInvetigators)
            {
                ProjectNumber.AddRange(context.Project_Numbers.Where(p => p.Project_Number1 == data.Project_Number).ToList());
            }
            List<File_Number> FileNumber = new List<File_Number>();
            foreach (var data in ProjectNumber)
            {
                FileNumber.AddRange(context.File_Numbers.Where(p => p.Project_Number == data.Project_Number1).ToList());
            }
            return new FilesForm()
            {
                AllInvestigators = AllInvetigators,
                FileNumbers = FileNumber
            };
        }

        /// <summary>
        /// Pulling data from database for FamiliesFormModel
        /// </summary>
        /// <param UserClass="user"></param>
        /// <param DatabaseContext="context"></param>
        /// <returns>Two list of related information</returns>
        public static FamiliesForm GetFamiliesForm(CachedUser user, DbContext context)
        {
            List<All_Investigator> AllInvetigators = context.All_Investigators.Where(p => p.Investigator == user.InvestigatorName).ToList();
            List<Project_Number> ProjectNumber = new List<Project_Number>();
            foreach (var data in AllInvetigators)
            {
                ProjectNumber.AddRange(context.Project_Numbers.Where(p => p.Project_Number1 == data.Project_Number).ToList());
            }
            List<File_Number> FileNumber = new List<File_Number>();
            foreach (var data in ProjectNumber)
            {
                FileNumber.AddRange(context.File_Numbers.Where(p => p.Project_Number == data.Project_Number1).ToList());
            }
            List<Family_Listing> FamilyListing = new List<Family_Listing>();
            foreach (var data in FileNumber)
            {
                FamilyListing.AddRange(context.Family_Listings.Where(p => p.File_Number == data.File_Number1).ToList());
            }
            List<Family> Families = new List<Family>();
            foreach (var data in FamilyListing)
            {
                Families.AddRange(context.Families.Where(p => p.Family_Number == data.Family_Number).ToList());
            }
            return new FamiliesForm()
            {
                AllInvestigators = AllInvetigators,
                Families = Families
            };
        }
    }
}
