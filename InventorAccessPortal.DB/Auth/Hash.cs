using System;
using System.Text;
using System.Security.Cryptography;

namespace InventorAccessPortal.DB.Auth
{ 
    static class Hash
    {
        public static String Default(String data)
        {
            using (SHA512 shaM = new SHA512Managed())
            {
                var byteData = Encoding.ASCII.GetBytes(data);
                return shaM.ComputeHash(byteData).ToString();
            }
        }
    }
}
