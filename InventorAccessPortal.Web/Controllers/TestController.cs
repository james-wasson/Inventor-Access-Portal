using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InventorAccessPortal.Web.DB.Objects;
using InventorAccessPortal.Web.Models;

namespace InventorAccessPortal.Web.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            var test = new Test() { Mydb = new Connection(System.Configuration.ConfigurationManager.ConnectionStrings["Access.DB.1"].ToString(), "Access.DB.1", "Microsoft.ACE.OLEDB.12.0") };
            return View(test);
        }
    }
}