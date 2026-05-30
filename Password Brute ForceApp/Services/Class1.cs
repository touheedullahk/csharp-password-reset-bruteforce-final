using System;

namespace PasswordBruteForceApp.Services
{
    public static class AppSettings
    {
        // Static salt used together with the password before SHA256 hashing.
        // In real security systems, each user should have a unique random salt.
        // For this educational project, the requirement asks for a constant static salt.
        public const string StaticSalt = "CSharpFinalTask_StaticSalt_2026";

        // Character set used for generated passwords and brute-force combinations.
        // The set is intentionally limited so the demonstration can run during class.
        public const string CharacterSet = "abc123XYZ";

        // Password length must be randomly generated between [4-6).
        // This means minimum is 4 and maximum exclusive is 6,
        // so generated passwords can have length 4 or 5.
        public const int GeneratedPasswordMinLength = 4;
        public const int GeneratedPasswordMaxLengthExclusive = 6;

        // Brute force must start from length 1 and continue up to maximum length 6.
        public const int BruteForceMinLength = 1;
        public const int BruteForceMaxLength = 6;

        // Calculates the maximum number of worker threads allowed by the task.
        // Requirement: use maximum of CPU cores - 1.
        // If the computer has only one core, we still allow one worker.
        public static int MaxWorkerThreads
        {
            get
            {
                int cpuCores = Environment.ProcessorCount;
                return Math.Max(1, cpuCores - 1);
            }
        }

        // Total number of possible combinations from length 1 to BruteForceMaxLength.
        // This will later be used for progress calculation.
        public static long GetTotalCombinationCount()
        {
            long total = 0;
            int characterCount = CharacterSet.Length;

            for (int length = BruteForceMinLength; length <= BruteForceMaxLength; length++)
            {
                total += Power(characterCount, length);
            }

            return total;
        }

        private static long Power(int baseNumber, int exponent)
        {
            long result = 1;

            for (int i = 0; i < exponent; i++)
            {
                result *= baseNumber;
            }

            return result;
        }
    }
}