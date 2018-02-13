using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InventorAccessPortal.DB.Objects.Collections;

namespace InventorAccessPortal.DB
{
    public static class CustomPasswordHasher
    {
        public static string HashPassword(string password, string salt = null)
        {
            return Encrypt.GetSAH512Hash((salt != null ? password + salt : password));
        }
        public static PasswordVerificationResult VerifyHashedPassword(InvestigatorLoginRow loginData, string providedPassword)
        {
            if (loginData.LoginDataRow.Password == HashPassword(providedPassword, loginData.LoginDataRow.Salt))
                return PasswordVerificationResult.Success;
            else
                return PasswordVerificationResult.Failed;
        }
    }
}