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

        public AttackResult StartAttack(string targetHash, CancellationToken externalCancellationToken)
        {
            if (string.IsNullOrEmpty(targetHash))
            {
                throw new ArgumentException("Target hash cannot be null or empty.", nameof(targetHash));
            }

            Stopwatch stopwatch = Stopwatch.StartNew();

            int workerCount = AppSettings.MaxWorkerThreads;
            Task[] tasks = new Task[workerCount];

            long attempts = 0;
            string? foundPassword = null;

            using CancellationTokenSource internalCancellationSource =
                CancellationTokenSource.CreateLinkedTokenSource(externalCancellationToken);

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
                        ref foundPassword
                    );
                }, cancellationToken);
            }

            try
            {
                Task.WaitAll(tasks);
            }
            catch (AggregateException)
            {
                // Some tasks may stop because cancellation was requested.
                // This is expected when the password is found or user stops the attack.
            }

            stopwatch.Stop();

            return new AttackResult
            {
                IsSuccess = foundPassword != null,
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
            ref string? foundPassword)
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

                    Interlocked.Increment(ref attempts);

                    if (_validator.IsPasswordMatch(candidatePassword, targetHash))
                    {
                        foundPassword = candidatePassword;
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
    }
}