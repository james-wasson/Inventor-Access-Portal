using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace InventorAccessPortal.Web.Util
{
    public static class EmailHelper
    {
        public static bool isValid(string email)
        {
            return new EmailAddressAttribute().IsValid(email);
        }
    }
}