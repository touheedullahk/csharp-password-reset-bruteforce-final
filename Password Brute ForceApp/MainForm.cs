using System;
using System.Windows.Forms;

namespace Password_Brute_ForceApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnCreatePassword_Click(object? sender, EventArgs e)
        {
            AddLog("Create Password button clicked. Backend connection will be added in the next step.");
        }

        private void btnStartSingleThread_Click(object? sender, EventArgs e)
        {
            AddLog("Single-thread attack button clicked. Backend connection will be added in the next step.");
        }

        private void btnStartMultiThread_Click(object? sender, EventArgs e)
        {
            AddLog("Multi-thread attack button clicked. Backend connection will be added in the next step.");
        }

        private void btnStopAttack_Click(object? sender, EventArgs e)
        {
            AddLog("Stop button clicked. Cancellation logic will be added in the next step.");
        }

        private void AddLog(string message)
        {
            string logLine = $"[{DateTime.Now:HH:mm:ss}] {message}";
            txtLogOutput.AppendText(logLine + Environment.NewLine);
        }
    }
}