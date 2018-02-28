using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InventorAccessPortal.DB;
using InventorAccessPortal.DB.Auth;
namespace InventorAccessPortal.Web.Util
{
    public class CredentialsHelper
    {
        public static async void Login(String UsernameOrEmail, String password)
        {
           
            // Set some cookie stuff here, maybe return some data etc
        }
        // GET: Authorization
        public static async void GetAuthorization(String UsernameOrEmail, String password)
        {

        }
    }
}