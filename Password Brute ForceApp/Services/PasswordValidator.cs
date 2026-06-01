using System;

namespace Password_Brute_ForceApp.Services
{
    public class PasswordValidator
    {
        private readonly PasswordHasher _passwordHasher;

        public PasswordValidator()
        {
            _passwordHasher = new PasswordHasher();
        }

        public bool IsPasswordMatch(string candidatePassword, string targetHash)
        {
            if (string.IsNullOrEmpty(candidatePassword))
            {
                return false;
            }

            if (string.IsNullOrEmpty(targetHash))
            {
                return false;
            }

            return _passwordHasher.VerifyPassword(candidatePassword, targetHash);
        }
    }
}