using System;

namespace PasswordBruteForceApp.Models
{
    public class AttackResult
    {
        public bool IsSuccess { get; set; }

        public string? FoundPassword { get; set; }

        public long AttemptsCount { get; set; }

        public TimeSpan ElapsedTime { get; set; }

        public int ThreadsUsed { get; set; }
    }
}