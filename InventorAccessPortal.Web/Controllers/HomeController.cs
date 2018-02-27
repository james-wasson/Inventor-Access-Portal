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

        /* Needed for Recent Activities
         * Date
         * Activity
         * Details
         * Notes
         * File Number
         * File Name
         * Status
         * Serial Number
         * Law Firm
         */
         /// <summary>
         /// 
         /// </summary>
         /// <returns></returns>
        public ActionResult RecentActivities()
        {
            ViewBag.Message = "Recent activities relevant to you.";

            return View();
        }

        /* Needed info for Inventions Form
         * Status
         * Project Number
         * Project Title
         */
         /// <summary>
         /// 
         /// </summary>
         /// <returns></returns>
        public ActionResult InventionsForm()
        {
            ViewBag.Message = "All Inventions";

            return View();
        }


        /* Needed info for Files Form:
         * Status
         * File Number
         * File Name
         * Serial Number
         * Continuity
         * Law Firm
         * Project Number
         */
         /// <summary>
         /// 
         /// </summary>
         /// <returns></returns>
        public ActionResult FilesForm()
        {
            ViewBag.Message = "All Files";

            return View();
        }
           
        /* Needed info for Families Form
         * Status
         * Family Name
         * Family Number
         */
         /// <summary>
         /// 
         /// </summary>
         /// <returns></returns>
        public ActionResult FamiliesForm()
        {
            ViewBag.Message = "All Families";

            return View();
        }
    }
}