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
        public bool Authorize(String username)
        {
            using (var DbContext = new Context())
            {
                DB.Auth.Authorize.ByUsername("adam", DbContext);
            }
            return false;
        }
    }
}