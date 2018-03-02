using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InventorAccessPortal.Web.Util;

namespace InventorAccessPortal.Web.Models.Account
{
    public enum LoginErrorCodes
    {
        [Description("Here is another")]
        InvalidUsernameOrPassword = 0,
        EmptyUsernameOrPassword = 1
    }
    public class LoginModel
    {
        public List<LoginErrorCodes> Errors = new List<LoginErrorCodes>();
        public string UsernameOrEmail { get; set; }
        public string Password { get; set; }
    }
} 