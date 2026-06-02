using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Password_Brute_ForceApp.Models;
using Password_Brute_ForceApp.Services;

namespace Password_Brute_ForceApp
{
    public partial class MainForm : Form
    {
        private readonly PasswordGenerator _passwordGenerator;
        private readonly PasswordHasher _passwordHasher;
        private readonly SingleThreadBruteForcer _singleThreadBruteForcer;
        private readonly MultiThreadBruteForcer _multiThreadBruteForcer;
        private readonly PerformanceLogger _performanceLogger;

        private CancellationTokenSource _cancellationTokenSource;

        public MainForm()
        {
            InitializeComponent();

            _passwordGenerator = new PasswordGenerator();
            _passwordHasher = new PasswordHasher();
            _singleThreadBruteForcer = new SingleThreadBruteForcer();
            _multiThreadBruteForcer = new MultiThreadBruteForcer();
            _performanceLogger = new PerformanceLogger();

            AddLog("Application started.");
        }

        private void btnCreatePassword_Click(object sender, EventArgs e)
        {
            try
            {
                string generatedPassword = _passwordGenerator.GeneratePassword();
                string passwordHash = _passwordHasher.HashPassword(generatedPassword);

                txtGeneratedPassword.Text = generatedPassword;
                txtHash.Text = passwordHash;

                ClearAttackOutput();

                AddLog($"Password generated successfully. Length: {generatedPassword.Length}");
                AddLog("SHA256 hash created using static salt.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error while creating password: {ex.Message}",
                    "Password Creation Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                AddLog($"Error while creating password: {ex.Message}");
            }
        }

        private async void btnStartSingleThread_Click(object sender, EventArgs e)
        {
            if (!CanStartAttack())
            {
                return;
            }

            PrepareAttackUi("single-thread");

            _cancellationTokenSource = new CancellationTokenSource();

            try
            {
                string targetHash = txtHash.Text;

                AddLog("Single-thread brute-force attack started.");

                AttackResult result = await Task.Run(() =>
                    _singleThreadBruteForcer.StartAttack(
                        targetHash,
                        _cancellationTokenSource.Token
                    )
                );

                DisplayAttackResult(result, "Single-thread");
                LogPerformanceResult("Single-thread", result);
            }
            catch (Exception ex)
            {
                ShowAttackError("single-thread", ex);
            }
            finally
            {
                ResetAttackUi();
            }
        }

        private async void btnStartMultiThread_Click(object sender, EventArgs e)
        {
            if (!CanStartAttack())
            {
                return;
            }

            PrepareAttackUi("multi-thread");

            _cancellationTokenSource = new CancellationTokenSource();

            try
            {
                string targetHash = txtHash.Text;

                AddLog("Multi-thread brute-force attack started.");
                AddLog($"Maximum worker threads allowed: {AppSettings.MaxWorkerThreads}");

                AttackResult result = await Task.Run(() =>
                    _multiThreadBruteForcer.StartAttack(
                        targetHash,
                        _cancellationTokenSource.Token
                    )
                );

                DisplayAttackResult(result, "Multi-thread");
                LogPerformanceResult("Multi-thread", result);
            }
            catch (Exception ex)
            {
                ShowAttackError("multi-thread", ex);
            }
            finally
            {
                ResetAttackUi();
            }
        }

        private void btnStopAttack_Click(object sender, EventArgs e)
        {
            if (_cancellationTokenSource == null)
            {
                AddLog("Stop button clicked, but no attack is currently running.");
                return;
            }

            _cancellationTokenSource.Cancel();
            AddLog("Stop requested. Attack cancellation signal sent.");
        }

        private bool CanStartAttack()
        {
            if (string.IsNullOrWhiteSpace(txtHash.Text))
            {
                MessageBox.Show(
                    "Please create a password first.",
                    "Missing Hash",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                AddLog("Attack could not start because no hash exists.");
                return false;
            }

            return true;
        }

        private void PrepareAttackUi(string attackType)
        {
            ClearAttackOutput();

            progressBarAttack.Style = ProgressBarStyle.Marquee;

            btnCreatePassword.Enabled = false;
            btnStartSingleThread.Enabled = false;
            btnStartMultiThread.Enabled = false;
            btnStopAttack.Enabled = true;

            AddLog($"Preparing {attackType} attack...");
        }

        private void ResetAttackUi()
        {
            progressBarAttack.Style = ProgressBarStyle.Blocks;
            progressBarAttack.Value = 100;

            btnCreatePassword.Enabled = true;
            btnStartSingleThread.Enabled = true;
            btnStartMultiThread.Enabled = true;
            btnStopAttack.Enabled = true;

            if (_cancellationTokenSource != null)
            {
                _cancellationTokenSource.Dispose();
                _cancellationTokenSource = null;
            }
        }

        private void DisplayAttackResult(AttackResult result, string attackName)
        {
            txtElapsedTime.Text = result.ElapsedTime.ToString(@"hh\:mm\:ss\.fff");
            txtAttempts.Text = result.AttemptsCount.ToString();
            txtThreadsUsed.Text = result.ThreadsUsed.ToString();

            if (result.IsSuccess)
            {
                txtFoundPassword.Text = result.FoundPassword;
                AddLog($"{attackName} attack completed successfully.");
                AddLog($"Password found: {result.FoundPassword}");
                AddLog($"Attempts checked: {result.AttemptsCount}");
                AddLog($"Elapsed time: {result.ElapsedTime}");
                AddLog($"Threads used: {result.ThreadsUsed}");
            }
            else
            {
                txtFoundPassword.Text = "Not found / stopped";
                AddLog($"{attackName} attack stopped or password was not found.");
                AddLog($"Attempts checked before stopping: {result.AttemptsCount}");
                AddLog($"Elapsed time: {result.ElapsedTime}");
                AddLog($"Threads used: {result.ThreadsUsed}");
            }
        }

        private void LogPerformanceResult(string attackType, AttackResult attackResult)
        {
            try
            {
                _performanceLogger.LogAttackResult(
                    attackType,
                    txtGeneratedPassword.Text,
                    txtHash.Text,
                    attackResult
                );

                AddLog($"{attackType} result saved to performance log.");
                AddLog($"Log file location: {_performanceLogger.GetLogFilePath()}");
            }
            catch (Exception ex)
            {
                AddLog($"Could not write performance log: {ex.Message}");
            }
        }

        private void ShowAttackError(string attackType, Exception ex)
        {
            MessageBox.Show(
                $"Error during {attackType} attack: {ex.Message}",
                "Attack Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );

            AddLog($"Error during {attackType} attack: {ex.Message}");
        }

        private void ClearAttackOutput()
        {
            progressBarAttack.Style = ProgressBarStyle.Blocks;
            progressBarAttack.Value = 0;

            txtElapsedTime.Clear();
            txtAttempts.Clear();
            txtThreadsUsed.Clear();
            txtFoundPassword.Clear();
        }

        private void AddLog(string message)
        {
            string logLine = $"[{DateTime.Now:HH:mm:ss}] {message}";
            txtLogOutput.AppendText(logLine + Environment.NewLine);
        }
    }
}