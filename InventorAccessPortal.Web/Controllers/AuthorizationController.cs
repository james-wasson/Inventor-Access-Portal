using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Text;
using InventorAccessPortal.DB;

namespace InventorAccessPortal.Web.Controllers
{
    public class AuthorizationController : Controller
    {
        // GET: Authorization
        public bool Credentials(String username, String password)
        {
            using (var DbContext = new Context())
            {
                DB.Auth.Authorize.Credentials("username", "password", DbContext);
            }
            return false;
        }
    }
}