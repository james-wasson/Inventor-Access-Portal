using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventorAccessPortal.DB.Objects;
using InventorAccessPortal.DB.Utils;

namespace InventorAccessPortal.DB.DataAccess
{
    public static class GetCachedUser
    {
        /// <summary>
        /// Returns the CachedUser based on the login data row passed in
        /// </summary>
        /// <param name="row">Login data row</param>
        /// <returns>a new cached user object</returns>
        public static CachedUser GetNew(Web_Login_datum row)
        {
            if (row == null) return null;
            var firstLast = GetName.FirstAndLast(row.Investigator_Name);

            return new CachedUser
            {
                Username = row.Username,
                InvestigatorName = row.Investigator_Name,
                InvestigatorNumber = row.Investigator.Investigator_Number,
                Email = row.Investigator.Email_Address,
                LastName = firstLast.LastName,
                FirstName = firstLast.FirstName,
                PhoneNumber = row.Investigator.Phone_Number
            };
        }

        /// <summary>
        /// Finds the user by username and returns the cached user object
        /// </summary>
        /// <param name="username">a username</param>
        /// <param name="e">database context</param>
        /// <returns>a new cached user object</returns>
        public static CachedUser UserByUsername(String username, EntityContext e)
        {
            var row = e.Web_Login_Data.FirstOrDefault(p => p.Username == username);
            return GetNew(row);
        }

        /// <summary>
        /// Finds the user by email and returns the cached user object
        /// </summary>
        /// <param name="email">a email</param>
        /// <param name="e">database context</param>
        /// <returns>a new cached user object</returns>
        public static CachedUser UserByEmail(String email, EntityContext e)
        {
            var lowerEmail = email.ToLower();
            var row = e.Web_Login_Data.FirstOrDefault(p => p.Investigator.Email_Address.ToLower() == lowerEmail);
            return GetNew(row);
        }
    }
}
