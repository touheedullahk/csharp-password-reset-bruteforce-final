using System;
using System.Security.Cryptography;
using System.Text;

namespace PasswordBruteForceApp.Services
{
    public class PasswordHasher
    {
        public string HashPassword(string password)
        {
            if (password == null)
            {
                throw new ArgumentNullException(nameof(password), "Password cannot be null.");
            }

            string saltedPassword = AppSettings.StaticSalt + password;

            using SHA256 sha256 = SHA256.Create();

            byte[] inputBytes = Encoding.UTF8.GetBytes(saltedPassword);
            byte[] hashBytes = sha256.ComputeHash(inputBytes);

            return ConvertToHexString(hashBytes);
        }

        private string ConvertToHexString(byte[] bytes)
        {
            StringBuilder builder = new StringBuilder();

            foreach (byte b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }

            return builder.ToString();
        }
    }
}