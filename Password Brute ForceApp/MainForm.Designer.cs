using System.Drawing;
using System.Windows.Forms;

namespace Password_Brute_ForceApp
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblTitle;
        private Label lblPasswordSection;
        private Label lblAttackSection;
        private Label lblResultSection;
        private Label lblLogSection;

        private Label lblGeneratedPassword;
        private Label lblHash;
        private Label lblElapsedTime;
        private Label lblAttempts;
        private Label lblThreadsUsed;
        private Label lblFoundPassword;
        private Label lblProgress;
        private Label lblStatus;

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
            lblPasswordSection = new Label();
            lblAttackSection = new Label();
            lblResultSection = new Label();
            lblLogSection = new Label();

            lblGeneratedPassword = new Label();
            lblHash = new Label();
            lblElapsedTime = new Label();
            lblAttempts = new Label();
            lblThreadsUsed = new Label();
            lblFoundPassword = new Label();
            lblProgress = new Label();
            lblStatus = new Label();

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
            ClientSize = new Size(940, 700);
            MinimumSize = new Size(940, 700);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "C# Final Task - Password Brute Force Demo";

            // Title
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 17F, FontStyle.Bold);
            lblTitle.Location = new Point(25, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Text = "Password Reset Brute Force Application";

            // Password Section
            lblPasswordSection.AutoSize = true;
            lblPasswordSection.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblPasswordSection.Location = new Point(30, 70);
            lblPasswordSection.Name = "lblPasswordSection";
            lblPasswordSection.Text = "1. Password Creation";

            lblGeneratedPassword.AutoSize = true;
            lblGeneratedPassword.Location = new Point(35, 105);
            lblGeneratedPassword.Name = "lblGeneratedPassword";
            lblGeneratedPassword.Text = "Generated Password:";

            txtGeneratedPassword.Location = new Point(185, 102);
            txtGeneratedPassword.Name = "txtGeneratedPassword";
            txtGeneratedPassword.Size = new Size(230, 23);
            txtGeneratedPassword.ReadOnly = true;

            btnCreatePassword.Location = new Point(440, 99);
            btnCreatePassword.Name = "btnCreatePassword";
            btnCreatePassword.Size = new Size(160, 30);
            btnCreatePassword.Text = "Create Password";
            btnCreatePassword.UseVisualStyleBackColor = true;
            btnCreatePassword.Click += btnCreatePassword_Click;

            lblHash.AutoSize = true;
            lblHash.Location = new Point(35, 145);
            lblHash.Name = "lblHash";
            lblHash.Text = "SHA256 Hash:";

            txtHash.Location = new Point(185, 142);
            txtHash.Name = "txtHash";
            txtHash.Size = new Size(700, 23);
            txtHash.ReadOnly = true;

            // Attack Section
            lblAttackSection.AutoSize = true;
            lblAttackSection.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblAttackSection.Location = new Point(30, 195);
            lblAttackSection.Name = "lblAttackSection";
            lblAttackSection.Text = "2. Brute Force Attack Controls";

            btnStartSingleThread.Location = new Point(35, 230);
            btnStartSingleThread.Name = "btnStartSingleThread";
            btnStartSingleThread.Size = new Size(200, 38);
            btnStartSingleThread.Text = "Start Single-Thread Attack";
            btnStartSingleThread.UseVisualStyleBackColor = true;
            btnStartSingleThread.Click += btnStartSingleThread_Click;

            btnStartMultiThread.Location = new Point(255, 230);
            btnStartMultiThread.Name = "btnStartMultiThread";
            btnStartMultiThread.Size = new Size(200, 38);
            btnStartMultiThread.Text = "Start Multi-Thread Attack";
            btnStartMultiThread.UseVisualStyleBackColor = true;
            btnStartMultiThread.Click += btnStartMultiThread_Click;

            btnStopAttack.Location = new Point(475, 230);
            btnStopAttack.Name = "btnStopAttack";
            btnStopAttack.Size = new Size(140, 38);
            btnStopAttack.Text = "Stop Attack";
            btnStopAttack.UseVisualStyleBackColor = true;
            btnStopAttack.Click += btnStopAttack_Click;

            lblProgress.AutoSize = true;
            lblProgress.Location = new Point(35, 295);
            lblProgress.Name = "lblProgress";
            lblProgress.Text = "Progress: 0%";

            progressBarAttack.Location = new Point(185, 292);
            progressBarAttack.Name = "progressBarAttack";
            progressBarAttack.Size = new Size(700, 26);
            progressBarAttack.Minimum = 0;
            progressBarAttack.Maximum = 100;
            progressBarAttack.Value = 0;

            lblStatus.AutoSize = true;
            lblStatus.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            lblStatus.Location = new Point(185, 325);
            lblStatus.Name = "lblStatus";
            lblStatus.Text = "Status: Ready.";

            // Result Section
            lblResultSection.AutoSize = true;
            lblResultSection.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblResultSection.Location = new Point(30, 365);
            lblResultSection.Name = "lblResultSection";
            lblResultSection.Text = "3. Attack Result";

            lblElapsedTime.AutoSize = true;
            lblElapsedTime.Location = new Point(35, 400);
            lblElapsedTime.Name = "lblElapsedTime";
            lblElapsedTime.Text = "Elapsed Time:";

            txtElapsedTime.Location = new Point(185, 397);
            txtElapsedTime.Name = "txtElapsedTime";
            txtElapsedTime.Size = new Size(230, 23);
            txtElapsedTime.ReadOnly = true;

            lblAttempts.AutoSize = true;
            lblAttempts.Location = new Point(35, 440);
            lblAttempts.Name = "lblAttempts";
            lblAttempts.Text = "Attempts Checked:";

            txtAttempts.Location = new Point(185, 437);
            txtAttempts.Name = "txtAttempts";
            txtAttempts.Size = new Size(230, 23);
            txtAttempts.ReadOnly = true;

            lblThreadsUsed.AutoSize = true;
            lblThreadsUsed.Location = new Point(475, 400);
            lblThreadsUsed.Name = "lblThreadsUsed";
            lblThreadsUsed.Text = "Threads Used:";

            txtThreadsUsed.Location = new Point(600, 397);
            txtThreadsUsed.Name = "txtThreadsUsed";
            txtThreadsUsed.Size = new Size(230, 23);
            txtThreadsUsed.ReadOnly = true;

            lblFoundPassword.AutoSize = true;
            lblFoundPassword.Location = new Point(475, 440);
            lblFoundPassword.Name = "lblFoundPassword";
            lblFoundPassword.Text = "Found Password:";

            txtFoundPassword.Location = new Point(600, 437);
            txtFoundPassword.Name = "txtFoundPassword";
            txtFoundPassword.Size = new Size(230, 23);
            txtFoundPassword.ReadOnly = true;

            // Log Section
            lblLogSection.AutoSize = true;
            lblLogSection.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblLogSection.Location = new Point(30, 500);
            lblLogSection.Name = "lblLogSection";
            lblLogSection.Text = "4. Application Log";

            txtLogOutput.Location = new Point(35, 530);
            txtLogOutput.Name = "txtLogOutput";
            txtLogOutput.Size = new Size(850, 120);
            txtLogOutput.Multiline = true;
            txtLogOutput.ScrollBars = ScrollBars.Vertical;
            txtLogOutput.ReadOnly = true;

            // Controls
            Controls.Add(lblTitle);

            Controls.Add(lblPasswordSection);
            Controls.Add(lblGeneratedPassword);
            Controls.Add(txtGeneratedPassword);
            Controls.Add(btnCreatePassword);
            Controls.Add(lblHash);
            Controls.Add(txtHash);

            Controls.Add(lblAttackSection);
            Controls.Add(btnStartSingleThread);
            Controls.Add(btnStartMultiThread);
            Controls.Add(btnStopAttack);
            Controls.Add(lblProgress);
            Controls.Add(progressBarAttack);
            Controls.Add(lblStatus);

            Controls.Add(lblResultSection);
            Controls.Add(lblElapsedTime);
            Controls.Add(txtElapsedTime);
            Controls.Add(lblAttempts);
            Controls.Add(txtAttempts);
            Controls.Add(lblThreadsUsed);
            Controls.Add(txtThreadsUsed);
            Controls.Add(lblFoundPassword);
            Controls.Add(txtFoundPassword);

            Controls.Add(lblLogSection);
            Controls.Add(txtLogOutput);

            ResumeLayout(false);
            PerformLayout();
        }
    }
}