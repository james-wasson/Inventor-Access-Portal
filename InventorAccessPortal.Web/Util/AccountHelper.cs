using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace InventorAccessPortal.Web.Util
{
    public class AccountHelper
    {
        /// <summary>
        /// Logs the user out
        /// </summary>
        public static void Logout()
        {
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
    }
}