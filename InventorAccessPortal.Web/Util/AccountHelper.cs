using System;
using System.Web;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using System.Security.Principal;

namespace InventorAccessPortal.Web.Util
{
    public static class AccountHelper
    {
        /// <summary>
        /// Logs the user out
        /// </summary>
        public static void Logout(HttpContextBase hc)
        {
            hc.Request.Cookies.Clear();
            hc.Session.Clear();
            hc.Session.Abandon();//Abandon session
            FormsAuthentication.SignOut();
            // set new http context user
            HttpContext.Current.User = new GenericPrincipal(new GenericIdentity(string.Empty), null);
        }
        /// <summary>
        /// returns true or false depending on if the user is logged in
        /// </summary>
        /// <returns>true if loggged in, false otherwise</returns>
        [AllowAnonymous]
        public static bool IsLoggedIn()
        {
            return HttpContext.Current.User.Identity.IsAuthenticated;
        }
    }
}