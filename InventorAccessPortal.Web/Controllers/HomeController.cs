using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using InventorAccessPortal.Web.Models.Home;

namespace InventorAccessPortal.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        /// <summary>
        /// Data needed: Date, Activity, Details, Notes, File Number, File Name, Status, Serial Number, Law Firm
        /// </summary>
        /// <returns> Page with table containing relevant data </returns>
        public ActionResult RecentActivities()
        {
            ViewBag.Message = "Recent activities relevant to you.";

            return View();
        }

        /// <summary>
        /// Data needed: Status, Project Number, Project Title
        /// </summary>
        /// <returns> Page with table containing relevant data </returns>
        public ActionResult InventionsForm()
        {
            ViewBag.Message = "All Inventions";

            return View();
        }

        /// <summary>
        /// Data needed for this: STatus, File Number, File Name, Serial Number, Continuity, Law Firm, Project Number
        /// </summary>
        /// <returns> Page with table containing relevant data </returns>
        public ActionResult FilesForm()
        {
            ViewBag.Message = "All Files";

            return View();
        }
           
        /// <summary>
        /// Data needed for this form: Status, Family Name, and Family Number
        /// </summary>
        /// <returns> Page with table containing relevant data </returns>
        public ActionResult FamiliesForm()
        {
            ViewBag.Message = "All Families";

            return View();
        }
    }
}