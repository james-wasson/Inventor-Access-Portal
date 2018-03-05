using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InventorAccessPortal.DB;
using InventorAccessPortal.Web.Models.Home;
using InventorAccessPortal.DB.DataAccess;
using InventorAccessPortal.DB.Objects;
using InventorAccessPortal.Web.Util;


namespace InventorAccessPortal.Web.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// redirects to RecentActivites
        /// </summary>
        /// <returns>Returns RecentActivites View</returns>
        public ActionResult Index()
        {
            return RedirectToAction("RecentActivities", "Home");
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        /// <summary>
        /// Uses the model for the Recent Activities form and fills the appropriate data fields
        /// </summary>
        /// <returns> Page with table containing data according to the model </returns>
        public ActionResult RecentActivities()
        {
            using (var e = new DbContext())
            {
                var recentActivitesObject = DataForms.GetRecentActivites(SessionHelper.GetSessionUser(), e);
                var model = new RecentActivitiesModel.Form();
                foreach (var fileNumWithTransAct in recentActivitesObject)
                {
                    var fileNum = fileNumWithTransAct.FileNumber;
                    foreach (var transAct in fileNumWithTransAct.Transactions)
                    {
                        model.RecentActivities.Add(new RecentActivitiesModel.Item()
                        {
                            Date = transAct.Transaction_Date.Value.ToShortDateString(),
                            Activity = transAct.Code,
                            Details = transAct.Details,
                            Notes = transAct.Notes,
                            FileNum = fileNum.File_Number1,
                            FileName = fileNum.File_Name,
                            Status = fileNum.Status,
                            SerialNum = fileNum.Serial_Number,
                            LawFirm = fileNum.Law_Firm
                        });
                    }
                }
                
                return View(model);
            }
        }

        /// <summary>
        /// Uses the model for the Inventions form and fills the appropriate data fields 
        /// </summary>
        /// <returns> Page with table containing data according to the model </returns>
        public ActionResult InventionsForm()
        {
            using (var e = new DbContext())
            {
                var data = DataForms.GetInventionsForm(SessionHelper.GetSessionUser(), e);
                var model = new InventionsModel.Form()
                {
                    Inventions = data.ProjectNumber.Select(p =>
                        new InventionsModel.Item()
                        {
                            Status = p.Status,
                            ProjectNumber = p.Project_Number1,
                            ProjectTitle = p.Project_Title
                        }).ToList()
                };
                
                return View(model);
            }
        }

        /// <summary>
        /// Uses the model for the Files form and fills the appropriate data fields
        /// </summary>
        /// <returns> Page with table containing data according to the model </returns>
        public ActionResult FilesForm()
        {
            using (var e = new DbContext())
            {
                var data = DataForms.GetFilesForm(SessionHelper.GetSessionUser(), e);
                var model = new FilesModel.Form()
                {
                    Files = data.FileNumbers.Select(p =>
                        new FilesModel.Item()
                        {
                             Continuity = p.Continuity,
                             FileName = p.File_Name,
                             FileNum = p.File_Number1,
                             LawFirm = p.Law_Firm,
                             ProjectNum = p.Project_Number.HasValue ? p.Project_Number.ToString() : "",
                             SerialNum = p.Serial_Number,
                             Status = p.Status
                        }).ToList()
                };

                return View(model);
            }
        }
           
        /// <summary>
        /// Uses the model for the Families form and fills the appropriate data fields
        /// </summary>
        /// <returns> Page with table containing the data according to the model </returns>
        public ActionResult FamiliesForm()
        {
            using (var e = new DbContext())
            {
                var familesObject = DataForms.GetFamiliesForm(SessionHelper.GetSessionUser(), e);
                var model = new FamiliesModel.Form();
                foreach (var fileNumWithFamily in familesObject)
                {
                    var fileNum = fileNumWithFamily.FileNumber;
                    foreach (var fam in fileNumWithFamily.Families)
                    {
                        model.Families.Add(new FamiliesModel.Item()
                        {
                            FamilyName = fam.Family_Name,
                            FamilyNum = fam.Family_Number,
                            Status = fileNum.Status
                        });
                    }
                }
                return View(model);
            }
        }
    }
}