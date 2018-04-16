using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using InventorAccessPortal.Web.Util;

namespace InventorAccessPortal.Web.Models.Account
{
    public class LoginModel : _ToasterModel
    {
        public string UsernameOrEmail { get; set; }
        public string Password { get; set; }
    }
} 