using System;
using System.IO;
using Password_Brute_ForceApp.Models;

namespace Password_Brute_ForceApp.Services
{
    public class PerformanceLogger
    {
        private readonly string _logFilePath;

        public PerformanceLogger()
        {
            string programDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string logDirectory = Path.Combine(programDirectory, "Logs");

            Directory.CreateDirectory(logDirectory);

            _logFilePath = Path.Combine(logDirectory, "performance-log.txt");
        }

        public void LogAttackResult(
            string attackType,
            string generatedPassword,
            string targetHash,
            AttackResult attackResult)
        {
            using (StreamWriter writer = new StreamWriter(_logFilePath, true))
            {
                writer.WriteLine("--------------------------------------------------");
                writer.WriteLine($"Date/Time: {DateTime.Now}");
                writer.WriteLine($"Attack Type: {attackType}");
                writer.WriteLine($"Generated Password: {generatedPassword}");
                writer.WriteLine($"Target SHA256 Hash: {targetHash}");
                writer.WriteLine($"Success: {attackResult.IsSuccess}");
                writer.WriteLine($"Found Password: {attackResult.FoundPassword}");
                writer.WriteLine($"Attempts Checked: {attackResult.AttemptsCount}");
                writer.WriteLine($"Elapsed Time: {attackResult.ElapsedTime}");
                writer.WriteLine($"Threads Used: {attackResult.ThreadsUsed}");
                writer.WriteLine("--------------------------------------------------");
                writer.WriteLine();
            }
        }

        public string GetLogFilePath()
        {
            return _logFilePath;
        }
    }
}