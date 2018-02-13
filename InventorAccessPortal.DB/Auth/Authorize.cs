using System;
using System.Data;
using System.Data.Objects;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventorAccessPortal.DB.Objects;
using InventorAccessPortal.DB.Objects.Collections;
using System.Threading.Tasks;
using InventorAccessPortal.DB;
using System.Security.Cryptography;
using Microsoft.AspNet.Identity;

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
        public static async Task<InvestigatorLoginRow> CredentialsByUsername(String username, String password, Context context = null)
        {
            if (context == null) context = new Context();
            // lopp the connections
            foreach (var conn in context.GetConnections())
            {
                await conn.FillInvestigatorsAsync();
                await conn.FillLoginDataAsync();
                // get the users with those credntals
                var InvestigatorLoginRow = conn.Login_Data.Select(p =>
                    new InvestigatorLoginRow
                    {
                        InvestigatorsRow = p.InvestigatorsRow,
                        LoginDataRow = p
                    }).FirstOrDefault(p =>
                        p.LoginDataRow.Username == username &&
                        PasswordVerificationResult.Success == CustomPasswordHasher.VerifyHashedPassword(p, password)
                    );
                // if there are any users return true
                if (InvestigatorLoginRow != null)
                {
                    return InvestigatorLoginRow;
                }
            }
            return null;
        }

        /// <summary>
        /// Will check to see if your credentials match any in the databses
        /// </summary>
        /// <param name="email">The email provided to check, not case sensitive</param>
        /// <param name="password">The password will be hashed and compared to the one in the database</param>
        /// <param name="context">the Database context object</param>
        /// <returns>Investigator Object if valid Credentials, otherwise null</returns>
        public static async Task<InvestigatorLoginRow> CredentialsByEmail(String email, String password, Context context = null)
        {
            if (context == null) context = new Context();
            var lowerEmail = email.ToLower();
            // lopp the connections
            foreach (var conn in context.GetConnections())
            {
                await conn.FillInvestigatorsAsync();
                await conn.FillLoginDataAsync();
                // get the users with those credntals
                var InvestigatorLoginRow = conn.Login_Data.Select(p =>
                    new InvestigatorLoginRow
                    {
                        InvestigatorsRow = p.InvestigatorsRow,
                        LoginDataRow = p
                    }).FirstOrDefault(p =>
                        p.InvestigatorsRow.Email_Address.ToLower() == lowerEmail &&
                        PasswordVerificationResult.Success == CustomPasswordHasher.VerifyHashedPassword(p, password)
                    );
                // if there are any users return true
                if (InvestigatorLoginRow != null)
                {
                    return InvestigatorLoginRow;
                }
            }
            return null;
        }

    }
}
