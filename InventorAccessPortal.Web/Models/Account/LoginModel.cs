using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using InventorAccessPortal.Web.Util;

namespace InventorAccessPortal.Web.Models.Account
{
    public enum LoginErrorCodes
    {
        [Description("Invalid Username or Password.")]
        InvalidUsernameOrPassword = 0,
        [Description("Username or Password is empty.")]
        EmptyUsernameOrPassword = 1
    }
    public class LoginModel : SharedModel
    {
        public string UsernameOrEmail { get; set; }
        public string Password { get; set; }
    }
} 