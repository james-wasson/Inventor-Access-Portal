using System;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using InventorAccessPortal.Web.Models;
using InventorAccessPortal.Web.Models.Account;
using InventorAccessPortal.Web.Util;
using InventorAccessPortal.DB.Auth;
using System.Web.Security;
using InventorAccessPortal.DB;

namespace InventorAccessPortal.Web.Controllers
{
    /// <summary>
    /// This Controller allows anonmous requests.
    /// Don't put anything non-public here
    /// </summary>
    [AllowAnonymous]
    public class AccountController : Controller
    {

        public ActionResult Login()
        {
            return View(new LoginModel());
        }

        [ValidateAntiForgeryToken, HttpPost]
        public ActionResult Login(LoginModel model)
        {
            // clears the errors from the model
            model.Errors.Clear();
            // checks if the user passed in their login data
            if (!String.IsNullOrEmpty(model.UsernameOrEmail) && !String.IsNullOrEmpty(model.Password))
            {
                using (var e = new DbContext()) // db context
                {
                    //check username and password from database
                    var cachedUser = Authorize.CredentialsByUsername(model.UsernameOrEmail, model.Password, e);
                    if (cachedUser != null)
                    {
                        //if username and password is correct, create session and return Success
                        SessionHelper.SetSessionUser(cachedUser);
                        FormsAuthentication.SetAuthCookie(cachedUser.Username, true);
                        
                        // goes to home screen or previous screen
                        FormsAuthentication.RedirectFromLoginPage(cachedUser.Username, true);
                    }
                    // throws an InvalidUsernameOrPassword error
                    model.Errors.Add(LoginErrorCodes.InvalidUsernameOrPassword);
                }
            }
            else
            {
                // throws a EmptyUsernameOrPassword error
                model.Errors.Add(LoginErrorCodes.EmptyUsernameOrPassword);
            }
            return View(model);
        }

        public ActionResult LogOff()
        {
            AccountHelper.Logout(HttpContext);
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }
    }
}