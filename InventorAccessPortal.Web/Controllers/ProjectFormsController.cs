using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;

namespace InventorAccessPortal.Web.Controllers
{
    public class ProjectFormsController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProjectReport()
        {
            return View("~/Views/ReportsForms/ProjectReport.cshtml");
        }

        public ActionResult FileReport()
        {
            return View("~/Views/ReportsForms/FileReport.cshtml");
        }

    }
}