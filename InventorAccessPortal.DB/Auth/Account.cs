using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventorAccessPortal.DB.Auth;
using InventorAccessPortal.DB.Objects;
using InventorAccessPortal.DB.DataAccess;

namespace InventorAccessPortal.DB.Auth
{
    public class Account
    {
        const string DATA_SOURCE_NAME = "IAP";

        public static CachedUser MakeNewUserLogin(String username, String email, String password, EntityContext e = null) {
            e.CheckInit();
            if (!Authorize.EmailExists(email, e)) return null;
            if (Authorize.EmailIsRegistered(email, e)) return null;
            if (Authorize.UsernameIsRegistered(username, e)) return null;
            var lowerEmail = email.ToLower();
            try
            {
                var investigator = e.Investigators.FirstOrDefault(p => p.Email_Address.ToLower() == lowerEmail);
                var loginData = investigator.Web_Login_Data;
                var salt = Encrypt.GenerateSalt();
                loginData = new Web_Login_datum()
                {
                    Investigator = investigator,
                    Salt = salt,
                    Password = PasswordVerify.HashPassword(password, salt),
                    Temp_Password = false,
                    Investigator_Name = investigator.Investigator_Name,
                    Username = username,
                    Suspended = false,
                    Email_Confirmed = false,
                    DataSource = DATA_SOURCE_NAME
                };
                e.Web_Login_Data.Add(loginData);
                e.SaveChanges();
                return GetCachedUser.GetNew(loginData);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
