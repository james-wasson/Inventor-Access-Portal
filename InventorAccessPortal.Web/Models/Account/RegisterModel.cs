using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using InventorAccessPortal.Web.Util;

namespace InventorAccessPortal.Web.Models.Account
{
    public class RegisterModel : _ErrorModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordVerify { get; set; }
    }
}
