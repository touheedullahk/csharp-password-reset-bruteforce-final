using System;
using System.Security.Cryptography;
using System.Text;

namespace Password_Brute_ForceApp.Services
{
    public class PasswordGenerator
    {
        public string GeneratePassword()
        {
            int passwordLength = RandomNumberGenerator.GetInt32(
                AppSettings.GeneratedPasswordMinLength,
                AppSettings.GeneratedPasswordMaxLengthExclusive
            );

            StringBuilder passwordBuilder = new StringBuilder();

            for (int i = 0; i < passwordLength; i++)
            {
                int randomIndex = RandomNumberGenerator.GetInt32(AppSettings.CharacterSet.Length);
                char randomCharacter = AppSettings.CharacterSet[randomIndex];

                passwordBuilder.Append(randomCharacter);
            }

            return passwordBuilder.ToString();
        }
    }
}