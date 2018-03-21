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
using InventorAccessPortal.DB.Enums;


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

        /// <summary>
        /// Returns the view for recent activites
        /// </summary>
        /// <param name="fileNumber">a filter by attribute</param>
        /// <param name="projectNumber">a filter by attribute</param>
        /// <returns>the view containing the recent activites model</returns>
        public ActionResult RecentActivities(String fileNumber = null, String projectNumber = null, String extendedTitle = null)
        {
            using (var e = new EntityContext())
            {
                var recentActivitesObject = DataForms.GetRecentActivites(SessionHelper.GetSessionUser(), e);
                var model = new RecentActivitiesModel.Form();
                // extends the page name
                model.SetExtendedTitle(extendedTitle);
                // filters
                if (fileNumber != null)
                {
                    recentActivitesObject = recentActivitesObject.Where(p => p.FileNumber.File_Number1 == fileNumber).ToList();
                    if (!model.HasExtendedTitle()) // if no extended name try to find one
                        model.SetExtendedTitle(recentActivitesObject.FirstOrDefault()?.FileNumber.File_Name);
                }
                if (projectNumber != null)
                {
                    recentActivitesObject = recentActivitesObject.Where(p => p.FileNumber.Project_Number.ToString() == projectNumber).ToList();
                    if (!model.HasExtendedTitle()) // if no extended name try to find one
                        model.SetExtendedTitle(recentActivitesObject.FirstOrDefault()?.FileNumber.Project_Numbers.Project_Name);
                }
                

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
            using (var e = new EntityContext())
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
        /// Process data from database for the FileForm for the current user
        /// </summary>
        /// <param name="familyNumber">A Filter that can be used</param>
        /// <param name="extendedTitle">An extended title that can be used</param>
        /// <returns>View containing the FilesModel</returns>
        public ActionResult FilesForm(String familyNumber = null, String extendedTitle = null)
        {
            using (var e = new EntityContext())
            {
                var data = DataForms.GetFilesForm(SessionHelper.GetSessionUser(), e);
                // new model
                var model = new FilesModel.Form();
                // extends the page name
                model.SetExtendedTitle(extendedTitle);
                // filters
                if (familyNumber != null)
                {
                    data.FileNumbers = data.FileNumbers.Where(p => p.Family_Listings.Select(q => q.Family_Number).Contains(familyNumber)).ToList();
                    // if no extended name try to find one
                    if (!model.HasExtendedTitle())
                        model.SetExtendedTitle(data.FileNumbers.FirstOrDefault()?.Project_Numbers.Project_Name);
                }

                model.Files = data.FileNumbers.Select(p =>
                    new FilesModel.Item()
                    {
                        Continuity = p.Continuity,
                        FileName = p.File_Name,
                        FileNum = p.File_Number1,
                        LawFirm = p.Law_Firm,
                        ProjectNum = p.Project_Number.HasValue ? p.Project_Number.ToString() : "",
                        SerialNum = p.Serial_Number,
                        Status = p.Status
                    }).ToList();

                return View(model);
            }
        }
           
        /// <summary>
        /// Uses the model for the Families form and fills the appropriate data fields
        /// </summary>
        /// <returns> Page with table containing the data according to the model </returns>
        public ActionResult FamiliesForm()
        {
            using (var e = new EntityContext())
            {
                var familesObject = DataForms.GetFamiliesForm(SessionHelper.GetSessionUser(), e);
                var model = new FamiliesModel.Form();
                foreach (var familyWithFileNums in familesObject)
                {
                    model.Families.Add(new FamiliesModel.Item
                    {
                        FamilyName = familyWithFileNums.Family.Family_Name,
                        FamilyNum = familyWithFileNums.Family.Family_Number,
                        // if any file is still active the family is considered active
                        Status = familyWithFileNums.FileNumbers.Any(p => p.Status == StatusEnum.Active) ? StatusEnum.Active : StatusEnum.Inactive
                    });
                }
                return View(model);
            }
        }
    }
}