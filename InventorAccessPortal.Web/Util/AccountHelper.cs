using System;
using System.Web;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;

namespace InventorAccessPortal.Web.Util
{
    public class AccountHelper
    {
        /// <summary>
        /// Logs the user out
        /// </summary>
        public static void Logout(HttpContextBase hc)
        {
            hc.Session.Clear();
            hc.Session.Abandon();//Abandon session
            hc.Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            hc.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            hc.Response.Cache.SetNoStore();
            hc.Response.Cache.SetNoServerCaching();
            FormsAuthentication.SignOut();
        }
        /// <summary>
        /// returns true or false depending on if the user is logged in
        /// </summary>
        /// <returns>true if loggged in, false otherwise</returns>
        public static bool IsLoggedIn()
        {
            return HttpContext.Current.User.Identity.IsAuthenticated;
        }
        /// <summary>
        /// redirects the user if not logged in
        /// </summary>
        public static void RedirectIfNotLoggedIn()
        {
            //if (!IsLoggedIn()) HttpContext.Current.Response.Redirect(FormsAuthentication.LoginUrl);
        }
    }
}