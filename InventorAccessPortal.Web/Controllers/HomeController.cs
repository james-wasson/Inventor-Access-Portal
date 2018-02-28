using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Data.Entity;
using InventorAccessPortal.DB;
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
        /// Uses the model for the Recent Activities form and fills the appropriate data fields
        /// </summary>
        /// <returns> Page with table containing data according to the model </returns>
        public ActionResult RecentActivities()
        {
            ViewBag.Message = "Recent activities relevant to you.";

            return View();
        }

        /// <summary>
        /// Uses the model for the Inventions form and fills the appropriate data fields 
        /// </summary>
        /// <returns> Page with table containing data according to the model </returns>
        public ActionResult InventionsForm()
        {
            ViewBag.Message = "All Inventions";

            return View();
        }

        /// <summary>
        /// Uses the model for the Files form and fills the appropriate data fields
        /// </summary>
        /// <returns> Page with table containing data according to the model </returns>
        public ActionResult FilesForm()
        {
            ViewBag.Message = "All Files";

            return View();
        }
           
        /// <summary>
        /// Uses the model for the Families form and fills the appropriate data fields
        /// </summary>
        /// <returns> Page with table containing the data according to the model </returns>
        public ActionResult FamiliesForm()
        {
            ViewBag.Message = "All Families";

            return View();
        }
    }
}