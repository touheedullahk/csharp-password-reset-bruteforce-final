using System;
using System.Security.Cryptography;
using System.Text;

namespace PasswordBruteForceApp.Services
{
    public class PasswordHasher
    {
        public string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Password cannot be null or empty.", nameof(password));
            }

            string saltedPassword = CombineSaltAndPassword(password);

            using SHA256 sha256 = SHA256.Create();

            byte[] inputBytes = Encoding.UTF8.GetBytes(saltedPassword);
            byte[] hashBytes = sha256.ComputeHash(inputBytes);

            return ConvertToHexString(hashBytes);
        }

        public bool VerifyPassword(string plainPassword, string expectedHash)
        {
            if (string.IsNullOrEmpty(expectedHash))
            {
                return false;
            }

            string actualHash = HashPassword(plainPassword);

            return string.Equals(actualHash, expectedHash, StringComparison.OrdinalIgnoreCase);
        }

        private string CombineSaltAndPassword(string password)
        {
            return AppSettings.StaticSalt + password;
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