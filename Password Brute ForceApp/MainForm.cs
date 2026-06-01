using System;
using System.Windows.Forms;
using Password_Brute_ForceApp.Services;

namespace Password_Brute_ForceApp
{
    public partial class MainForm : Form
    {
        private readonly PasswordGenerator _passwordGenerator;
        private readonly PasswordHasher _passwordHasher;

        public MainForm()
        {
            InitializeComponent();

            _passwordGenerator = new PasswordGenerator();
            _passwordHasher = new PasswordHasher();

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

        private void btnStartSingleThread_Click(object sender, EventArgs e)
        {
            AddLog("Single-thread attack button clicked. Backend connection will be added in the next step.");
        }

        private void btnStartMultiThread_Click(object sender, EventArgs e)
        {
            AddLog("Multi-thread attack button clicked. Backend connection will be added in the next step.");
        }

        private void btnStopAttack_Click(object sender, EventArgs e)
        {
            AddLog("Stop button clicked. Cancellation logic will be added in the next step.");
        }

        private void ClearAttackOutput()
        {
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