using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InventorAccessPortal.DB;
using InventorAccessPortal.DB.Objects;
using InventorAccessPortal.DB.DataAccess;

namespace InventorAccessPortal.Web.Util
{
    public static class SessionHelper
    {
        /// <summary>
        /// stores new user inot Session
        /// </summary>
        /// <param name="u">CachedUser</param>
        public static void SetSessionUser(CachedUser u)
        {
            HttpContext.Current.Session["userID"] = u.Username;
            HttpContext.Current.Session["CachedUser"] = u;
        }

        /// <summary>
        /// removes cached user from session
        /// </summary>
        public static void DeleteSessionUser()
        {
            HttpContext.Current.Session["userID"] = null;
            HttpContext.Current.Session["CachedUser"] = null;
        }

        /// <summary>
        /// deletes the session user, then gets a new session user
        /// </summary>
        public static void RefreshSessionUser()
        {
            DeleteSessionUser();
            GetSessionUser();
        }

        /// <summary>
        /// Gets the users current session Real Name,
        /// Returns the Identity.name is the session is null
        /// </summary>
        /// <returns>returns the users first space last name</returns>
        public static String GetRealName()
        {
            var u = GetSessionUser();
            if (u != null)
            {
                return u.FirstName + " " + u.LastName;
            }
            return "";
        }
        /// <summary>
        /// Gets the current sessions cached user
        /// </summary>
        /// <returns>returns cached user</returns>
        public static CachedUser GetSessionUser()
        {
            // null if not logged in
            if (!AccountHelper.IsLoggedIn()) return null;
            // gets the current cached user
            var user = (CachedUser)HttpContext.Current.Session["CachedUser"];
            // if there is no user or if the current session user is not the user logged in
            if (user == null || user.InvestigatorName != HttpContext.Current.User.Identity.Name)
            {
                // store new user
                using (var e = new DbContext())
                {
                    user = GetCachedUser.UserByUsername(HttpContext.Current.User.Identity.Name, e);
                    SetSessionUser(user);
                }
            }
            // return new or old cached user
            return user;
        }
    }
}