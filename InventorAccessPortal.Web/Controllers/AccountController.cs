using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InventorAccessPortal.Web.Models;
using InventorAccessPortal.Web.Models.Account;

namespace InventorAccessPortal.Web.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Register() {
            return View();
        }

        public ActionResult Login()
        {
            var model = new LoginModel
            {
                WelcomeMessage = "Hello and welcome to our website"
            };
            return View(model);
        }
    }
}