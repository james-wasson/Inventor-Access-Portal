using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InventorAccessPortal.DB;
using InventorAccessPortal.Web.Models.Home;

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
            ViewBag.Message = "Recent activities relevant to you.";
            var model = new RecentActivitiesModel.Form();
            return View(model);
        }

        /// <summary>
        /// Uses the model for the Inventions form and fills the appropriate data fields 
        /// </summary>
        /// <returns> Page with table containing data according to the model </returns>
        public ActionResult InventionsForm()
        {
            ViewBag.Message = "All Inventions";
            var model = new InventionsModel.Form();
            return View(model);
        }

        /// <summary>
        /// Uses the model for the Files form and fills the appropriate data fields
        /// </summary>
        /// <returns> Page with table containing data according to the model </returns>
        public ActionResult FilesForm()
        {
            ViewBag.Message = "All Files";
            var model = new FilesModel.Form();
            return View(model);
        }
           
        /// <summary>
        /// Uses the model for the Families form and fills the appropriate data fields
        /// </summary>
        /// <returns> Page with table containing the data according to the model </returns>
        public ActionResult FamiliesForm()
        {
            ViewBag.Message = "All Families";
            var model = new FamiliesModel.Form();
            return View(model);
        }
    }
}