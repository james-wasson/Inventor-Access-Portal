using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using InventorAccessPortal.Web.Enums;

namespace InventorAccessPortal.Web.Util
{
    public static class PasswordRequirements
    {
        public const int MinLength = 16;
        public const int MaxLength = 255;
        public static bool IsSpecialChacter(char a)
        {
            return char.IsSymbol(a) || char.IsPunctuation(a);
        }
        public static bool IsPasswordCharValid(char a)
        {
            if (a == '\0') return false;
            return true;
        }
    }
    public static class UsernameRequirements
    {
        public const int MinLength = 4;
        public const int MaxLength = 255;
        public static bool IsUsernameCharValid(char a)
        {
            if (a == '\0' || a == '@' || char.IsWhiteSpace(a)) return false;
            return true;
        }
    }
    public static class EmailRequirements
    {
        public const int MaxLength = 255;
    }

    /// <summary>
    /// A Helper function inplimenting the above classes
    /// </summary>
    public static class CredentialsHelper
    {
        #region Email Validation
        // email validation
        public static bool IsEmailValid(string email)
        {
            return new EmailAddressAttribute().IsValid(email) && email.Length <= EmailRequirements.MaxLength;
        }
        #endregion

        #region Password Validation
        public static bool IsPasswordValid(string password)
        {
            return (
                !String.IsNullOrEmpty(password) && 
                password.Length >= PasswordRequirements.MinLength &&
                password.Length <= PasswordRequirements.MaxLength &&
                password.All(p => PasswordRequirements.IsPasswordCharValid(p))
            );
        }
        
        public static List<Enum> GetPasswordWarnings(string password)
        {
            var rv = new List<Enum>();
            if (!(password.Any(p => Char.IsUpper(p)) && password.Any(p => Char.IsLower(p)))) {
                rv.Add(PasswordWarnings.UpperAndLower);
            }
            if (!password.Any(p => PasswordRequirements.IsSpecialChacter(p)))
            {
                rv.Add(PasswordWarnings.SpecialCharacter);
            }
            if (!password.Any(p => char.IsDigit(p)))
            {
                rv.Add(PasswordWarnings.Number);
            }
            return rv;
        }
        #endregion

        #region Username Validation
        public static bool IsUsernameValid(string username)
        {
            return (
                !String.IsNullOrEmpty(username) && 
                username.Length >= UsernameRequirements.MinLength &&
                username.Length <= UsernameRequirements.MaxLength &&
                username.All(p => UsernameRequirements.IsUsernameCharValid(p))
            );
        }
        #endregion
    }
}