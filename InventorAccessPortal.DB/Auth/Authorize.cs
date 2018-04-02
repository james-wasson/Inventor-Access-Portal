using System;
using System.Collections;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Threading.Tasks;
using InventorAccessPortal.DB.Objects;
using InventorAccessPortal.DB.Utils;
using InventorAccessPortal.DB.DataAccess;
using System.Collections.Generic;

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
        public static CachedUser CredentialsByUsername(String username, String password, EntityContext context = null)
        {
            context.CheckInit();
            var LoginData = context.Web_Login_Data.FirstOrDefault(p => p.Username == username);

            if (!IsLoginAllowed(LoginData, password)) return null;

            return GetCachedUser.GetNew(LoginData);
        }

        /// <summary>
        /// Will check to see if your credentials match any in the databses
        /// </summary>
        /// <param name="email">The email provided to check, not case sensitive</param>
        /// <param name="password">The password will be hashed and compared to the one in the database</param>
        /// <param name="context">the Database context object</param>
        /// <returns>Investigator Object if valid Credentials, otherwise null</returns>
        public static CachedUser CredentialsByEmail(String email, String password, EntityContext context = null)
        {
            context.CheckInit();
            var lowerEmail = email.ToLower();
            var LoginData = context.Web_Login_Data.FirstOrDefault(p => p.Investigator.Email_Address.ToLower() == lowerEmail);

            if (!IsLoginAllowed(LoginData, password)) return null;

            return GetCachedUser.GetNew(LoginData);
        }

        public static bool IsLoginAllowed(Web_Login_datum row, String password)
        {
            // clear errors
            AuthorizeErrors = new List<AuthorizeErrorsEnum>();
            // check no login data 
            if (row == null) AuthorizeErrors.Add(AuthorizeErrorsEnum.NoLoginData);
            // cehck is suspended
            if (row != null && row.Suspended == true) AuthorizeErrors.Add(AuthorizeErrorsEnum.LoginSuspended);
            // check is email confirmed
            if (row != null && row.Email_Confirmed == false) AuthorizeErrors.Add(AuthorizeErrorsEnum.EmailNotConfirmed);
            // check if password is verified
            if (row != null && PasswordVerify.VerifyHashedPassword(row.Password, row.Salt, password) == PasswordVerify.Failed)
                AuthorizeErrors.Add(AuthorizeErrorsEnum.PasswordNotVerified);
            return !GetAuthorizeErrors().Any(); // true if no errors
        }

        /// <summary>
        /// Checks if the Username is already registered with an account
        /// </summary>
        /// <param name="username"></param>
        /// <param name="context">Databasse context</param>
        /// <returns>True if the username is registered</returns>
        public static bool UsernameIsRegistered(String username, EntityContext context = null)
        {
            context.CheckInit();
            return context.Web_Login_Data.Any(p => p.Username == username);
        }

        /// <summary>
        /// Checks if the Email is already registered with an account,
        /// NOTE: If email confirmed is null, it lets you register anyway
        /// </summary>
        /// <param name="email"></param>
        /// <param name="context"></param>
        /// <returns>True if the email is registered</returns>
        public static bool EmailIsRegistered(String email, EntityContext context = null)
        {
            context.CheckInit();
            var lowerEmail = email.ToLower();
            var a = context.Investigators.Where(p => p.Email_Address.ToLower() == lowerEmail).Select(q => q.Web_Login_Data).ToList();
            var loginRow = context.Investigators.FirstOrDefault(p => p.Email_Address.ToLower() == lowerEmail)?.Web_Login_Data;
            if (loginRow == null || loginRow.Email_Confirmed == false ||(String.IsNullOrEmpty(loginRow.Username) && String.IsNullOrEmpty(loginRow.Password))) return false;
            return true;
        }

        /// <summary>
        /// Checks if the email is registed to a user in the databasse, this does not always mean they have an account
        /// </summary>
        /// <param name="email"></param>
        /// <param name="context"></param>
        /// <returns>True if there is an investigator with that email</returns>
        public static bool EmailExists(String email, EntityContext context = null)
        {
            context.CheckInit();
            var lowerEmail = email.ToLower();
            return context.Investigators.Any(p => p.Email_Address.ToLower() == lowerEmail);
        }

        private static List<AuthorizeErrorsEnum> AuthorizeErrors = new List<AuthorizeErrorsEnum>();
        public static List<AuthorizeErrorsEnum> GetAuthorizeErrors() {
            return new List<AuthorizeErrorsEnum>(AuthorizeErrors);
        }
        public enum AuthorizeErrorsEnum
        {
            NoLoginData,
            LoginSuspended,
            EmailNotConfirmed,
            PasswordNotVerified
        }

        public static bool ConfirmEmail(string email, string username, string investigatorName, EntityContext dbContext = null) {
            dbContext.CheckInit();

            var lowerEmail = email.ToLower();
            var loginData = dbContext.Web_Login_Data.FirstOrDefault(p => 
                p.Username == username && 
                p.Investigator.Email_Address.ToLower() == lowerEmail && 
                p.Investigator_Name == investigatorName
            );
            if (loginData == null) return false;
            loginData.Email_Confirmed = true;
            try { dbContext.SaveChanges(); } catch (Exception ex) { return false; }
            
            return true;
        }

        public static bool ResetPassword(string email, string password, EntityContext dbContext = null)
        {
            dbContext.CheckInit();
          
            var lowerEmail = email.ToLower();
            var loginData = dbContext.Web_Login_Data.FirstOrDefault(p =>
                p.Investigator.Email_Address.ToLower() == lowerEmail
            );
            if (loginData == null) return false;
            loginData.Password = PasswordVerify.HashPassword(password, loginData.Salt);
            try { dbContext.SaveChanges(); } catch (Exception ex) { return false; }

            return true;
        }
    }
}
