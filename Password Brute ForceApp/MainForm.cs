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
        private Timer _elapsedTimer;
        private DateTime _attackStartTime;

        public MainForm()
        {
            InitializeComponent();

            _passwordGenerator = new PasswordGenerator();
            _passwordHasher = new PasswordHasher();
            _singleThreadBruteForcer = new SingleThreadBruteForcer();
            _multiThreadBruteForcer = new MultiThreadBruteForcer();
            _performanceLogger = new PerformanceLogger();

            _elapsedTimer = new Timer();
            _elapsedTimer.Interval = 250;
            _elapsedTimer.Tick += ElapsedTimer_Tick;

            AddLog("Application started.");
            SetStatus("Ready.");
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
                SetStatus("Password created. Ready to start attack.");
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
                SetStatus("Password creation failed.");
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
                SetStatus("Single-thread attack running...");

                IProgress<ProgressInfo> progress = new Progress<ProgressInfo>(UpdateProgressDisplay);

                AttackResult result = await Task.Run(() =>
                    _singleThreadBruteForcer.StartAttack(
                        targetHash,
                        _cancellationTokenSource.Token,
                        progress
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
                SetStatus("Multi-thread attack running...");

                IProgress<ProgressInfo> progress = new Progress<ProgressInfo>(UpdateProgressDisplay);

                AttackResult result = await Task.Run(() =>
                    _multiThreadBruteForcer.StartAttack(
                        targetHash,
                        _cancellationTokenSource.Token,
                        progress
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
                SetStatus("No attack is currently running.");
                return;
            }

            _cancellationTokenSource.Cancel();
            AddLog("Stop requested. Attack cancellation signal sent.");
            SetStatus("Stopping attack...");
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
                SetStatus("Create a password before starting attack.");
                return false;
            }

            return true;
        }

        private void PrepareAttackUi(string attackType)
        {
            ClearAttackOutput();

            progressBarAttack.Style = ProgressBarStyle.Blocks;
            progressBarAttack.Value = 0;

            _attackStartTime = DateTime.Now;
            _elapsedTimer.Start();

            btnCreatePassword.Enabled = false;
            btnStartSingleThread.Enabled = false;
            btnStartMultiThread.Enabled = false;
            btnStopAttack.Enabled = true;

            AddLog($"Preparing {attackType} attack...");
            SetStatus($"Preparing {attackType} attack...");
        }

        private void ResetAttackUi()
        {
            _elapsedTimer.Stop();

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

        private void UpdateProgressDisplay(ProgressInfo progressInfo)
        {
            int progressValue = progressInfo.ProgressPercentage;

            if (progressValue < 0)
            {
                progressValue = 0;
            }

            if (progressValue > 100)
            {
                progressValue = 100;
            }

            progressBarAttack.Value = progressValue;
            lblProgress.Text = $"Progress: {progressValue}%";

            txtAttempts.Text = progressInfo.AttemptsChecked.ToString();

            SetStatus(
                $"Searching length {progressInfo.CurrentLength} | " +
                $"Attempts: {progressInfo.AttemptsChecked} / {progressInfo.TotalCombinations}"
            );
        }

        private void DisplayAttackResult(AttackResult result, string attackName)
        {
            txtElapsedTime.Text = result.ElapsedTime.ToString(@"hh\:mm\:ss\.fff");
            txtAttempts.Text = result.AttemptsCount.ToString();
            txtThreadsUsed.Text = result.ThreadsUsed.ToString();

            if (result.IsSuccess)
            {
                progressBarAttack.Value = 100;
                lblProgress.Text = "Progress: 100%";
                txtFoundPassword.Text = result.FoundPassword;

                AddLog($"{attackName} attack completed successfully.");
                AddLog($"Password found: {result.FoundPassword}");
                AddLog($"Attempts checked: {result.AttemptsCount}");
                AddLog($"Elapsed time: {result.ElapsedTime}");
                AddLog($"Threads used: {result.ThreadsUsed}");

                SetStatus($"{attackName} attack completed. Password found.");
            }
            else
            {
                txtFoundPassword.Text = "Not found / stopped";

                AddLog($"{attackName} attack stopped or password was not found.");
                AddLog($"Attempts checked before stopping: {result.AttemptsCount}");
                AddLog($"Elapsed time: {result.ElapsedTime}");
                AddLog($"Threads used: {result.ThreadsUsed}");

                SetStatus($"{attackName} attack stopped or password was not found.");
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
            SetStatus($"{attackType} attack failed.");
        }

        private void ClearAttackOutput()
        {
            progressBarAttack.Style = ProgressBarStyle.Blocks;
            progressBarAttack.Value = 0;
            lblProgress.Text = "Progress: 0%";

            txtElapsedTime.Clear();
            txtAttempts.Clear();
            txtThreadsUsed.Clear();
            txtFoundPassword.Clear();

            SetStatus("Ready.");
        }

        private void ElapsedTimer_Tick(object sender, EventArgs e)
        {
            TimeSpan elapsed = DateTime.Now - _attackStartTime;
            txtElapsedTime.Text = elapsed.ToString(@"hh\:mm\:ss\.fff");
        }

        private void SetStatus(string message)
        {
            AddLog($"Status: {message}");
        }

        private void AddLog(string message)
        {
            string logLine = $"[{DateTime.Now:HH:mm:ss}] {message}";
            txtLogOutput.AppendText(logLine + Environment.NewLine);
        }
    }
}