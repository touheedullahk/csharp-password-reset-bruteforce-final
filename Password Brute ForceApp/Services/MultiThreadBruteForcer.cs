using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Password_Brute_ForceApp.Models;

namespace Password_Brute_ForceApp.Services
{
    public class MultiThreadBruteForcer
    {
        private readonly PasswordValidator _validator;

        public MultiThreadBruteForcer()
        {
            _validator = new PasswordValidator();
        }

        public AttackResult StartAttack(
            string targetHash,
            CancellationToken externalCancellationToken,
            IProgress<ProgressInfo> progress = null)
        {
            if (string.IsNullOrEmpty(targetHash))
            {
                throw new ArgumentException("Target hash cannot be null or empty.", nameof(targetHash));
            }

            Stopwatch stopwatch = Stopwatch.StartNew();

            int workerCount = AppSettings.MaxWorkerThreads;
            Task[] tasks = new Task[workerCount];

            long attempts = 0;
            long totalCombinations = AppSettings.GetTotalCombinationCount();

            string foundPassword = "";

            using (CancellationTokenSource internalCancellationSource =
                CancellationTokenSource.CreateLinkedTokenSource(externalCancellationToken))
            {
                CancellationToken cancellationToken = internalCancellationSource.Token;

                for (int workerIndex = 0; workerIndex < workerCount; workerIndex++)
                {
                    int localWorkerIndex = workerIndex;

                    tasks[localWorkerIndex] = Task.Run(() =>
                    {
                        SearchWorker(
                            targetHash,
                            localWorkerIndex,
                            workerCount,
                            cancellationToken,
                            internalCancellationSource,
                            ref attempts,
                            ref foundPassword,
                            totalCombinations,
                            progress
                        );
                    }, cancellationToken);
                }

                try
                {
                    Task.WaitAll(tasks);
                }
                catch (AggregateException)
                {
                    // Cancellation can cause task exceptions. This is expected when stopping.
                }
            }

            stopwatch.Stop();

            ReportProgress(
                progress,
                attempts,
                totalCombinations,
                AppSettings.BruteForceMaxLength
            );

            return new AttackResult
            {
                IsSuccess = !string.IsNullOrEmpty(foundPassword),
                FoundPassword = foundPassword,
                AttemptsCount = attempts,
                ElapsedTime = stopwatch.Elapsed,
                ThreadsUsed = workerCount
            };
        }

        private void SearchWorker(
            string targetHash,
            int workerIndex,
            int workerCount,
            CancellationToken cancellationToken,
            CancellationTokenSource cancellationSource,
            ref long attempts,
            ref string foundPassword,
            long totalCombinations,
            IProgress<ProgressInfo> progress)
        {
            for (int length = AppSettings.BruteForceMinLength; length <= AppSettings.BruteForceMaxLength; length++)
            {
                long totalForLength = CalculateCombinationCount(length);

                for (long combinationIndex = workerIndex; combinationIndex < totalForLength; combinationIndex += workerCount)
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        return;
                    }

                    string candidatePassword = ConvertIndexToPassword(combinationIndex, length);

                    long currentAttempts = Interlocked.Increment(ref attempts);

                    if (currentAttempts % 1000 == 0)
                    {
                        ReportProgress(progress, currentAttempts, totalCombinations, length);
                    }

                    if (_validator.IsPasswordMatch(candidatePassword, targetHash))
                    {
                        foundPassword = candidatePassword;
                        ReportProgress(progress, currentAttempts, totalCombinations, length);
                        cancellationSource.Cancel();
                        return;
                    }
                }
            }
        }

        private string ConvertIndexToPassword(long index, int length)
        {
            char[] password = new char[length];
            int characterSetLength = AppSettings.CharacterSet.Length;

            for (int position = length - 1; position >= 0; position--)
            {
                int characterIndex = (int)(index % characterSetLength);
                password[position] = AppSettings.CharacterSet[characterIndex];
                index /= characterSetLength;
            }

            return new string(password);
        }

        private long CalculateCombinationCount(int length)
        {
            long result = 1;
            int characterSetLength = AppSettings.CharacterSet.Length;

            for (int i = 0; i < length; i++)
            {
                result *= characterSetLength;
            }

            return result;
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