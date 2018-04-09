using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventorAccessPortal.Web.Models.Account
{
    public class LoginResetPasswordModel : _TitleModel
    {
        public string Password { get; set; }
        public string PasswordVerify { get; set; }

    }
}