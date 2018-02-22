using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Threading.Tasks;
using InventorAccessPortal.DB.Objects;
using InventorAccessPortal.DB.Utils;

namespace InventorAccessPortal.DB.Auth
{
    public static class Authorize
    {
        /// <summary>
        /// Will check to see if your credentials match any in the databses
        /// </summary>
        /// <param name="username">The username provided to check, case sensitive</param>
        /// <param name="password">The password will be hashed and compared to the one in the database</param>
        /// <param name="context">the Database context object</param>
        /// <returns>Investigator Object if valid Credentials, otherwise null</returns>
        public static CachedUser CredentialsByUsername(String username, String password, Context context = null)
        {
            context.Check();
            var LoginData = context.Login_Data.FirstOrDefault(p => p.Username == username);

            if (LoginData == null || PasswordVerify.VerifyHashedPassword(LoginData.Password, LoginData.Salt, password) == PasswordVerify.Failed)
                return null;

            var firstLast = GetName.FirstAndLast(LoginData.Investigator_Name);

            return new CachedUser
            {
                Username = LoginData.Username,
                InvestigatorName = LoginData.Investigator_Name,
                InvestigatorNumber = LoginData.Investigator.Investigator_Number,
                Email = LoginData.Investigator.Email_Address,
                LastName = firstLast.LastName,
                FirstName = firstLast.FirstName,
                PhoneNumber = LoginData.Investigator.Phone_Number
            };
        }

        /// <summary>
        /// Will check to see if your credentials match any in the databses
        /// </summary>
        /// <param name="email">The email provided to check, not case sensitive</param>
        /// <param name="password">The password will be hashed and compared to the one in the database</param>
        /// <param name="context">the Database context object</param>
        /// <returns>Investigator Object if valid Credentials, otherwise null</returns>
        public static CachedUser CredentialsByEmail(String email, String password, Context context = null)
        {
            context.Check();
            var lowerEmail = email.ToLower();
            var LoginData = context.Login_Data.FirstOrDefault(p => p.Investigator.Email_Address.ToLower() == lowerEmail);

            if (LoginData == null || PasswordVerify.VerifyHashedPassword(LoginData.Password, LoginData.Salt, password) == PasswordVerify.Failed)
                return null;

            var firstLast = GetName.FirstAndLast(LoginData.Investigator_Name);

            return new CachedUser
            {
                Username = LoginData.Username,
                InvestigatorName = LoginData.Investigator_Name,
                InvestigatorNumber = LoginData.Investigator.Investigator_Number,
                Email = LoginData.Investigator.Email_Address,
                LastName = firstLast.LastName,
                FirstName = firstLast.FirstName,
                PhoneNumber = LoginData.Investigator.Phone_Number
            };
        }

    }
}
