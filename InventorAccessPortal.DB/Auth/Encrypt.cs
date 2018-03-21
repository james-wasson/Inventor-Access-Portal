using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace InventorAccessPortal.DB
{
    public static class Encrypt
    {
        public static string GetSAH512Hash(string input)
        {
            using (SHA512CryptoServiceProvider sha512 = new SHA512CryptoServiceProvider())
            {
                byte[] b = System.Text.Encoding.UTF8.GetBytes(input);
                b = sha512.ComputeHash(b);
                return ByteArrayToString(b);
            }
        }
        private static RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
        public static string GenerateSalt()
        {
            byte[] secretkey = new Byte[64];
            rng.GetBytes(secretkey);
            return ByteArrayToString(secretkey);
        }

        private static string ByteArrayToString(byte[] b)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (byte x in b)
                sb.Append(x.ToString("x2"));
            return sb.ToString();
        }
    }
}