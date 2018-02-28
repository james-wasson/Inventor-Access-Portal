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

            };
            return View(model);
        }

        public ActionResult LogOff()
        {
            System.Web.Security.FormsAuthentication.SignOut();
            return View();
        }

        public async Task<string> Confirmation()
        {
            //Receive data from front end
            string username = Request["username"];
            string password = Request["password"];
            //check username and password from database
            if (await Authorize.CredentialsByUsername(username, password) != null)
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

        private bool WriteCookie(string username, string password)
        {
            HttpCookie cookie = new HttpCookie("Logininfo");
            DateTime dtNow = DateTime.Now;
            TimeSpan tsMinute = new TimeSpan(0, 0, 3, 0);
            cookie.Expires = dtNow + tsMinute;
            cookie["username"] = username;
            Response.Cookies.Add(cookie);
            return true;
        }
    }
}