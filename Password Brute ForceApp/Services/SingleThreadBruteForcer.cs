using System;
using System.Diagnostics;
using System.Threading;
using Password_Brute_ForceApp.Models;

namespace Password_Brute_ForceApp.Services
{
    public class SingleThreadBruteForcer
    {
        private readonly BruteForceGenerator _generator;
        private readonly PasswordValidator _validator;

        public SingleThreadBruteForcer()
        {
            _generator = new BruteForceGenerator();
            _validator = new PasswordValidator();
        }

        public AttackResult StartAttack(string targetHash, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(targetHash))
            {
                throw new ArgumentException("Target hash cannot be null or empty.", nameof(targetHash));
            }

            Stopwatch stopwatch = Stopwatch.StartNew();
            long attempts = 0;

            foreach (string candidatePassword in _generator.GenerateAllCombinations())
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    stopwatch.Stop();

                    return new AttackResult
                    {
                        IsSuccess = false,
                        FoundPassword = null,
                        AttemptsCount = attempts,
                        ElapsedTime = stopwatch.Elapsed,
                        ThreadsUsed = 1
                    };
                }

                attempts++;

                if (_validator.IsPasswordMatch(candidatePassword, targetHash))
                {
                    stopwatch.Stop();

                    return new AttackResult
                    {
                        IsSuccess = true,
                        FoundPassword = candidatePassword,
                        AttemptsCount = attempts,
                        ElapsedTime = stopwatch.Elapsed,
                        ThreadsUsed = 1
                    };
                }
            }

            stopwatch.Stop();

            return new AttackResult
            {
                IsSuccess = false,
                FoundPassword = null,
                AttemptsCount = attempts,
                ElapsedTime = stopwatch.Elapsed,
                ThreadsUsed = 1
            };
        }
    }
}