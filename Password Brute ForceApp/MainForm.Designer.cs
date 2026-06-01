using System.Drawing;
using System.Windows.Forms;

namespace Password_Brute_ForceApp
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblTitle;
        private Label lblGeneratedPassword;
        private Label lblHash;
        private Label lblElapsedTime;
        private Label lblAttempts;
        private Label lblThreadsUsed;
        private Label lblFoundPassword;
        private Label lblProgress;

        private TextBox txtGeneratedPassword;
        private TextBox txtHash;
        private TextBox txtElapsedTime;
        private TextBox txtAttempts;
        private TextBox txtThreadsUsed;
        private TextBox txtFoundPassword;
        private TextBox txtLogOutput;

        private Button btnCreatePassword;
        private Button btnStartSingleThread;
        private Button btnStartMultiThread;
        private Button btnStopAttack;

        private ProgressBar progressBarAttack;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblTitle = new Label();
            lblGeneratedPassword = new Label();
            lblHash = new Label();
            lblElapsedTime = new Label();
            lblAttempts = new Label();
            lblThreadsUsed = new Label();
            lblFoundPassword = new Label();
            lblProgress = new Label();

            txtGeneratedPassword = new TextBox();
            txtHash = new TextBox();
            txtElapsedTime = new TextBox();
            txtAttempts = new TextBox();
            txtThreadsUsed = new TextBox();
            txtFoundPassword = new TextBox();
            txtLogOutput = new TextBox();

            btnCreatePassword = new Button();
            btnStartSingleThread = new Button();
            btnStartMultiThread = new Button();
            btnStopAttack = new Button();

            progressBarAttack = new ProgressBar();

            SuspendLayout();

            // MainForm
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(900, 620);
            Text = "Password Reset Brute Force Demo";
            StartPosition = FormStartPosition.CenterScreen;

            // Title
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.Location = new Point(25, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(420, 30);
            lblTitle.Text = "Password Reset Brute Force Application";

            // Generated Password Label
            lblGeneratedPassword.AutoSize = true;
            lblGeneratedPassword.Location = new Point(30, 80);
            lblGeneratedPassword.Name = "lblGeneratedPassword";
            lblGeneratedPassword.Size = new Size(120, 15);
            lblGeneratedPassword.Text = "Generated Password:";

            // Generated Password TextBox
            txtGeneratedPassword.Location = new Point(180, 77);
            txtGeneratedPassword.Name = "txtGeneratedPassword";
            txtGeneratedPassword.Size = new Size(220, 23);
            txtGeneratedPassword.ReadOnly = true;

            // Create Password Button
            btnCreatePassword.Location = new Point(430, 75);
            btnCreatePassword.Name = "btnCreatePassword";
            btnCreatePassword.Size = new Size(160, 28);
            btnCreatePassword.Text = "Create Password";
            btnCreatePassword.UseVisualStyleBackColor = true;
            btnCreatePassword.Click += btnCreatePassword_Click;

            // Hash Label
            lblHash.AutoSize = true;
            lblHash.Location = new Point(30, 120);
            lblHash.Name = "lblHash";
            lblHash.Size = new Size(82, 15);
            lblHash.Text = "SHA256 Hash:";

            // Hash TextBox
            txtHash.Location = new Point(180, 117);
            txtHash.Name = "txtHash";
            txtHash.Size = new Size(650, 23);
            txtHash.ReadOnly = true;

            // Start Single Thread Button
            btnStartSingleThread.Location = new Point(30, 170);
            btnStartSingleThread.Name = "btnStartSingleThread";
            btnStartSingleThread.Size = new Size(180, 35);
            btnStartSingleThread.Text = "Start Single-Thread Attack";
            btnStartSingleThread.UseVisualStyleBackColor = true;
            btnStartSingleThread.Click += btnStartSingleThread_Click;

            // Start Multi Thread Button
            btnStartMultiThread.Location = new Point(230, 170);
            btnStartMultiThread.Name = "btnStartMultiThread";
            btnStartMultiThread.Size = new Size(180, 35);
            btnStartMultiThread.Text = "Start Multi-Thread Attack";
            btnStartMultiThread.UseVisualStyleBackColor = true;
            btnStartMultiThread.Click += btnStartMultiThread_Click;

            // Stop Attack Button
            btnStopAttack.Location = new Point(430, 170);
            btnStopAttack.Name = "btnStopAttack";
            btnStopAttack.Size = new Size(120, 35);
            btnStopAttack.Text = "Stop Attack";
            btnStopAttack.UseVisualStyleBackColor = true;
            btnStopAttack.Click += btnStopAttack_Click;

            // Progress Label
            lblProgress.AutoSize = true;
            lblProgress.Location = new Point(30, 235);
            lblProgress.Name = "lblProgress";
            lblProgress.Size = new Size(55, 15);
            lblProgress.Text = "Progress:";

            // Progress Bar
            progressBarAttack.Location = new Point(180, 232);
            progressBarAttack.Name = "progressBarAttack";
            progressBarAttack.Size = new Size(650, 25);
            progressBarAttack.Minimum = 0;
            progressBarAttack.Maximum = 100;
            progressBarAttack.Value = 0;

            // Elapsed Time Label
            lblElapsedTime.AutoSize = true;
            lblElapsedTime.Location = new Point(30, 285);
            lblElapsedTime.Name = "lblElapsedTime";
            lblElapsedTime.Size = new Size(82, 15);
            lblElapsedTime.Text = "Elapsed Time:";

            // Elapsed Time TextBox
            txtElapsedTime.Location = new Point(180, 282);
            txtElapsedTime.Name = "txtElapsedTime";
            txtElapsedTime.Size = new Size(220, 23);
            txtElapsedTime.ReadOnly = true;

            // Attempts Label
            lblAttempts.AutoSize = true;
            lblAttempts.Location = new Point(30, 325);
            lblAttempts.Name = "lblAttempts";
            lblAttempts.Size = new Size(106, 15);
            lblAttempts.Text = "Attempts Checked:";

            // Attempts TextBox
            txtAttempts.Location = new Point(180, 322);
            txtAttempts.Name = "txtAttempts";
            txtAttempts.Size = new Size(220, 23);
            txtAttempts.ReadOnly = true;

            // Threads Used Label
            lblThreadsUsed.AutoSize = true;
            lblThreadsUsed.Location = new Point(30, 365);
            lblThreadsUsed.Name = "lblThreadsUsed";
            lblThreadsUsed.Size = new Size(82, 15);
            lblThreadsUsed.Text = "Threads Used:";

            // Threads Used TextBox
            txtThreadsUsed.Location = new Point(180, 362);
            txtThreadsUsed.Name = "txtThreadsUsed";
            txtThreadsUsed.Size = new Size(220, 23);
            txtThreadsUsed.ReadOnly = true;

            // Found Password Label
            lblFoundPassword.AutoSize = true;
            lblFoundPassword.Location = new Point(30, 405);
            lblFoundPassword.Name = "lblFoundPassword";
            lblFoundPassword.Size = new Size(100, 15);
            lblFoundPassword.Text = "Found Password:";

            // Found Password TextBox
            txtFoundPassword.Location = new Point(180, 402);
            txtFoundPassword.Name = "txtFoundPassword";
            txtFoundPassword.Size = new Size(220, 23);
            txtFoundPassword.ReadOnly = true;

            // Log Output TextBox
            txtLogOutput.Location = new Point(30, 455);
            txtLogOutput.Name = "txtLogOutput";
            txtLogOutput.Size = new Size(800, 120);
            txtLogOutput.Multiline = true;
            txtLogOutput.ScrollBars = ScrollBars.Vertical;
            txtLogOutput.ReadOnly = true;

            // Add Controls
            Controls.Add(lblTitle);

            Controls.Add(lblGeneratedPassword);
            Controls.Add(txtGeneratedPassword);
            Controls.Add(btnCreatePassword);

            Controls.Add(lblHash);
            Controls.Add(txtHash);

            Controls.Add(btnStartSingleThread);
            Controls.Add(btnStartMultiThread);
            Controls.Add(btnStopAttack);

            Controls.Add(lblProgress);
            Controls.Add(progressBarAttack);

            Controls.Add(lblElapsedTime);
            Controls.Add(txtElapsedTime);

            Controls.Add(lblAttempts);
            Controls.Add(txtAttempts);

            Controls.Add(lblThreadsUsed);
            Controls.Add(txtThreadsUsed);

            Controls.Add(lblFoundPassword);
            Controls.Add(txtFoundPassword);

            Controls.Add(txtLogOutput);

            ResumeLayout(false);
            PerformLayout();
        }
    }
}