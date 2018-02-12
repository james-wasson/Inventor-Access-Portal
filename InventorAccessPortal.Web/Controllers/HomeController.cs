using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;

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

        public ActionResult RecentActivities()
        {
            ViewBag.Message = "Recent activities relevant to you.";

            return View();
        }

        public ActionResult InventionsForm()
        {
            ViewBag.Message = "All Inventions";

            return View();
        }

        public ActionResult FilesForm()
        {
            ViewBag.Message = "All Files";

            return View();
        }

        public ActionResult FamiliesForm()
        {
            ViewBag.Message = "All Families";

            return View();
        }
    }
}