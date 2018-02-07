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
    public class CredentialsHelper
    {
        public static async void Login(String UsernameOrEmail, String password)
        {
            InvestigatorLoginRow LoginInfo = await GetAuthorization(UsernameOrEmail, password);
            // Set some cookie stuff here, maybe return some data etc
        }
        // GET: Authorization
        public static async Task<InvestigatorLoginRow> GetAuthorization(String UsernameOrEmail, String password)
        {
            using (var e = new Context())
            {
                if (EmailHelper.isValid(UsernameOrEmail)) // is email
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