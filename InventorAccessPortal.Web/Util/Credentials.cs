using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InventorAccessPortal.DB;
using InventorAccessPortal.DB.Auth;
using InventorAccessPortal.DB.Objects.Collections;

namespace InventorAccessPortal.Web.Util
{
    public class Credentials
    {
        // GET: Authorization
        public static async Task<InvestigatorLoginRow> Login(String UsernameOrEmail, String password)
        {
            using (var e = new Context())
            {
                if (Email.isValid(UsernameOrEmail)) // is email
                {
                    return await Authorize.CredentialsByEmail(UsernameOrEmail, password, e);
                }
                else // is username
                {
                    return await Authorize.CredentialsByUsername(UsernameOrEmail, password, e);
                }
            }
        }
    }
}