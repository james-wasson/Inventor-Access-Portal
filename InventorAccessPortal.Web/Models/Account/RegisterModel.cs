using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using InventorAccessPortal.Web.Util;

namespace InventorAccessPortal.Web.Models.Account
{
    public enum RegisterErrorCodes
    {
        [Description("Invalid Username or Password.")]
        InvalidUsernameOrPassword = 0,
        [Description("Username or Password is empty.")]
        EmptyUsernameOrPassword = 1
    }
    public class RegisterModel : SharedModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordVerify { get; set; }
    }
} 