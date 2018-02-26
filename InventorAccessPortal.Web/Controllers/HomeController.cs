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

        public ActionResult RecentActivities()
        {
            ViewBag.Message = "Recent activities relevant to you.";

            return View();
        }

        public ActionResult InventionsForm()
        {
            ViewBag.Message = "All Inventions";
            var a = new Inventions.Item {
                Status = "1",
                ProjectNumber = "2",
                ProjectTitle = "3"
            };
            var b = new Inventions.Item
            {
                Status = "a",
                ProjectNumber = "b",
                ProjectTitle = "c"
            };
            var c = new Inventions.Item
            {
                Status = "x",
                ProjectNumber = "y",
                ProjectTitle = "z"
            };
            var d = new List<Inventions.Item> { a, b, c };
            return View(new Inventions.Form
            {
                Inventions = d
            });
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