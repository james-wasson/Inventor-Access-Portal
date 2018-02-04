using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventorAccessPortal.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (var authContext = new DB.Auth.Authorize())
            {
                authContext.ByUsername("adam");
            }
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
    }
}