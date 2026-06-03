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

        public AttackResult StartAttack(
            string targetHash,
            CancellationToken cancellationToken,
            IProgress<ProgressInfo> progress = null)
        {
            if (string.IsNullOrEmpty(targetHash))
            {
                throw new ArgumentException("Target hash cannot be null or empty.", nameof(targetHash));
            }

            Stopwatch stopwatch = Stopwatch.StartNew();

            long attempts = 0;
            long totalCombinations = AppSettings.GetTotalCombinationCount();

            for (int length = AppSettings.BruteForceMinLength; length <= AppSettings.BruteForceMaxLength; length++)
            {
                foreach (string candidatePassword in _generator.GenerateCombinationsByLength(length))
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        stopwatch.Stop();

                        ReportProgress(progress, attempts, totalCombinations, length);

                        return new AttackResult
                        {
                            IsSuccess = false,
                            FoundPassword = "",
                            AttemptsCount = attempts,
                            ElapsedTime = stopwatch.Elapsed,
                            ThreadsUsed = 1
                        };
                    }

                    attempts++;

                    if (attempts % 1000 == 0)
                    {
                        ReportProgress(progress, attempts, totalCombinations, length);
                    }

                    if (_validator.IsPasswordMatch(candidatePassword, targetHash))
                    {
                        stopwatch.Stop();

                        ReportProgress(progress, attempts, totalCombinations, length);

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
            }

            stopwatch.Stop();

            ReportProgress(progress, attempts, totalCombinations, AppSettings.BruteForceMaxLength);

            return new AttackResult
            {
                IsSuccess = false,
                FoundPassword = "",
                AttemptsCount = attempts,
                ElapsedTime = stopwatch.Elapsed,
                ThreadsUsed = 1
            };
        }

        private void ReportProgress(
            IProgress<ProgressInfo> progress,
            long attempts,
            long totalCombinations,
            int currentLength)
        {
            if (progress == null)
            {
                return;
            }

            int percentage = 0;

            if (totalCombinations > 0)
            {
                percentage = (int)((attempts * 100) / totalCombinations);
            }

            if (percentage > 100)
            {
                percentage = 100;
            }

            progress.Report(new ProgressInfo
            {
                AttemptsChecked = attempts,
                TotalCombinations = totalCombinations,
                CurrentLength = currentLength,
                ProgressPercentage = percentage
            });
        }
    }
}