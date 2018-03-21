using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventorAccessPortal.DB
{
    public static class PasswordVerify
    {
        public static PasswordVerificationResult Success = PasswordVerificationResult.Success;
        public static PasswordVerificationResult Failed = PasswordVerificationResult.Failed;
        public static string HashPassword(string password, string salt = null)
        {
            return Encrypt.GetSAH512Hash((salt != null ? password + salt : password));
        }
        public static PasswordVerificationResult VerifyHashedPassword(string storedPassword, string salt, string providedPassword)
        {
            if (storedPassword == HashPassword(providedPassword, salt))
                return PasswordVerificationResult.Success;
            else
                return PasswordVerificationResult.Failed;
        }
    }
}