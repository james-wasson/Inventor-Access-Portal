using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InventorAccessPortal.DB;
using InventorAccessPortal.DB.Auth;

namespace InventorAccessPortal.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (var e = new Context()) {
                var log1 = Authorize.CredentialsByUsername("KristyOwen", "Dog#123", e);
                var log2 = Authorize.CredentialsByEmail("kristy.owen@siu.edu", "Dog#123", e);
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