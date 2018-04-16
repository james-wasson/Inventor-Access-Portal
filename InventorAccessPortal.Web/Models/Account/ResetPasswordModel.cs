using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventorAccessPortal.Web.Models.Account
{
    public class ResetPasswordModel : _ToasterModel
    {
        public string Email { get; set; } = "";
    }
}