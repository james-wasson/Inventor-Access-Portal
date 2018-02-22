using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InventorAccessPortal.Web.Models;
using InventorAccessPortal.Web.Models.Account;
using InventorAccessPortal.Web.Util;
using System.Threading.Tasks;
using InventorAccessPortal.DB.Auth;
using System.Web.Security;
using InventorAccessPortal.DB;

namespace InventorAccessPortal.Web.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Register()
        {
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

        public ActionResult LogOff()
        {
            System.Web.Security.FormsAuthentication.SignOut();
            return View();
        }

        public string Confirmation()
        {
            using (var e = new Context())
            {
                //Receive data from front end
                string username = Request["username"];
                string password = Request["password"];
                //check username and password from database
                var cachedUser = Authorize.CredentialsByUsername(username, password, e);
                if (cachedUser != null)
                {
                    //if username and password is correct, create session and return Success
                    Session["userID"] = username;
                    Session["realName"] = "Jhon";
                    Session["JID"] = "1";
                    FormsAuthentication.SetAuthCookie(username, true);
                    return "Success";
                }
                //if username and password is not correct then return Failure
                else
                {
                    return "Failure";
                }
            }
        }
    }
}