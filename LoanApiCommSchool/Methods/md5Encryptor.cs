using System.Security.Cryptography;
using System.Text;
using System;

namespace LoanApiCommSchool.Methods
{
    public class md5Encryptor
    {
        public static string HashPasswordMD5(string password)
        {
            if (string.IsNullOrEmpty(password))
                return string.Empty;

            using (var md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to a hexadecimal string
                return Convert.ToHexString(hashBytes);
            }
        }
    }
}
