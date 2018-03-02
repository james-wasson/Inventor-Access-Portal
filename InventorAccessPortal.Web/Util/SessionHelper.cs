using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InventorAccessPortal.DB.Objects;

namespace InventorAccessPortal.Web.Util
{
    public static class SessionHelper
    {
        public static void SetSessionUser(CachedUser u)
        {
            HttpContext.Current.Session["userID"] = u.Username;
            HttpContext.Current.Session["CachedUser"] = u;
        }
        public static void DeleteSessionUser(CachedUser u)
        {
            HttpContext.Current.Session["userID"] = null;
            HttpContext.Current.Session["CachedUser"] = null;
        }
        /// <summary>
        /// Gets the users current session Real Name,
        /// Returns the Identity.name is the session is null
        /// </summary>
        /// <returns>returns the users first space last name</returns>
        public static String GetRealName()
        {
            if (HttpContext.Current.Cache["CachedUser"] != null)
            {
                var u = (CachedUser)HttpContext.Current.Session["CachedUser"];
                if (u != null)
                    return u.FirstName + " " + u.LastName;
            }
            return HttpContext.Current.User.Identity.Name;
        }
        /// <summary>
        /// Gets the current sessions cached user
        /// </summary>
        /// <returns>returns cached user</returns>
        public static CachedUser GetSessionUser()
        {
            return (CachedUser)HttpContext.Current.Session["CachedUser"];
        }
    }
}