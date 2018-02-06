using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventorAccessPortal.DB.Objects;

namespace InventorAccessPortal.DB.Auth
{
    public static class Authorize
    {
        /// <summary>
        /// Will check to see if your credentials match any in the databse
        /// </summary>
        /// <param name="username">The username provided to check</param>
        /// <param name="password">The password to check</param>
        /// <param name="context">the Database context object</param>
        /// <returns></returns>
        public static bool CredentialsByUsername(String username, String password, Context context = null)
        {
            if (context == null) context = new Context();
            // hash the password
            var hashedPassword = Encrypt.GetSAH512Hash(password);
            // lopp the connections
            foreach (var conn in context.GetConnections())
            {

                // get the users with those credntals
                var accessGranted = conn.LoginDataAdapterManager().GetData().FirstOrDefault(p =>
                    !p.Suspended && p.Username == username && p.Password == hashedPassword
                );
                // if there are any users return true
                if (accessGranted != null)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Will check to see if your credentials match any in the databse
        /// </summary>
        /// <param name="email">The email provided to check</param>
        /// <param name="password">The password to check</param>
        /// <param name="context">the Database context object</param>
        /// <returns></returns>
        public static bool CredentialsByEmail(String email, String password, Context context = null)
        {
            if (context == null) context = new Context();
            // hash the password
            var hashedPassword = Encrypt.GetSAH512Hash(password);
            var lowerEmail = email.ToLower();
            // lopp the connections
            foreach (var conn in context.GetConnections())
            {
                // get the users with those credntals
                var loginAllowed = conn.LoginDataAdapterManager().GetData().FirstOrDefault(p =>
                    !p.Suspended && p.Password == hashedPassword &&
                    conn.InvestigatorsTableAdapter().GetData().Where(q =>
                        q.Investigator_Number == p.Investigator_Number && q.Email.ToLower() == lowerEmail
                    ).Any()
                ) != null;

                /*var loginAllowed = conn.LoginDataAdapterManager().GetData().FirstOrDefault(p =>
                    !p.Suspended && p.Password == hashedPassword &&
                    p.InvestigatorsRow.Email.ToLower() == lowerEmail
                ) != null;*/


                // if there are any users return true
                if (loginAllowed)
                  return true;
            }
            return false;
        }
    }
}
