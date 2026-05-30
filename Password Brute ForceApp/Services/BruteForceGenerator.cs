using System;
using System.Collections.Generic;

namespace PasswordBruteForceApp.Services
{
    public class BruteForceGenerator
    {
        public IEnumerable<string> GenerateAllCombinations()
        {
            for (int length = AppSettings.BruteForceMinLength; length <= AppSettings.BruteForceMaxLength; length++)
            {
                foreach (string combination in GenerateCombinationsByLength(length))
                {
                    yield return combination;
                }
            }
        }

        public IEnumerable<string> GenerateCombinationsByLength(int length)
        {
            if (length < 1)
            {
                throw new ArgumentException("Length must be at least 1.", nameof(length));
            }

            char[] currentCombination = new char[length];

            foreach (string combination in GenerateRecursive(currentCombination, 0, length))
            {
                yield return combination;
            }
        }

        private IEnumerable<string> GenerateRecursive(char[] currentCombination, int position, int length)
        {
            if (position == length)
            {
                yield return new string(currentCombination);
                yield break;
            }

            foreach (char character in AppSettings.CharacterSet)
            {
                currentCombination[position] = character;

                foreach (string combination in GenerateRecursive(currentCombination, position + 1, length))
                {
                    yield return combination;
                }
            }
        }
    }
}