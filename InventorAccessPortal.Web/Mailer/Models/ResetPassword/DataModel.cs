using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using InventorAccessPortal.Web.Util;
using InventorAccessPortal.Web.Models;

namespace InventorAccessPortal.Web.Mailer.Models.ResetPassword
{
    public class DataModel : _ErrorModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordVerify { get; set; }
    }
}
