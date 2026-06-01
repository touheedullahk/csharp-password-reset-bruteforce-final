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
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.Location = new Point(25, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(424, 30);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Password Reset Brute Force Application";
            // 
            // lblGeneratedPassword
            // 
            lblGeneratedPassword.AutoSize = true;
            lblGeneratedPassword.Location = new Point(30, 80);
            lblGeneratedPassword.Name = "lblGeneratedPassword";
            lblGeneratedPassword.Size = new Size(117, 15);
            lblGeneratedPassword.TabIndex = 1;
            lblGeneratedPassword.Text = "Generated Password:";
            // 
            // lblHash
            // 
            lblHash.AutoSize = true;
            lblHash.Location = new Point(30, 120);
            lblHash.Name = "lblHash";
            lblHash.Size = new Size(81, 15);
            lblHash.TabIndex = 4;
            lblHash.Text = "SHA256 Hash:";
            // 
            // lblElapsedTime
            // 
            lblElapsedTime.AutoSize = true;
            lblElapsedTime.Location = new Point(30, 285);
            lblElapsedTime.Name = "lblElapsedTime";
            lblElapsedTime.Size = new Size(80, 15);
            lblElapsedTime.TabIndex = 11;
            lblElapsedTime.Text = "Elapsed Time:";
            // 
            // lblAttempts
            // 
            lblAttempts.AutoSize = true;
            lblAttempts.Location = new Point(30, 325);
            lblAttempts.Name = "lblAttempts";
            lblAttempts.Size = new Size(108, 15);
            lblAttempts.TabIndex = 13;
            lblAttempts.Text = "Attempts Checked:";
            // 
            // lblThreadsUsed
            // 
            lblThreadsUsed.AutoSize = true;
            lblThreadsUsed.Location = new Point(30, 365);
            lblThreadsUsed.Name = "lblThreadsUsed";
            lblThreadsUsed.Size = new Size(81, 15);
            lblThreadsUsed.TabIndex = 15;
            lblThreadsUsed.Text = "Threads Used:";
            // 
            // lblFoundPassword
            // 
            lblFoundPassword.AutoSize = true;
            lblFoundPassword.Location = new Point(30, 405);
            lblFoundPassword.Name = "lblFoundPassword";
            lblFoundPassword.Size = new Size(97, 15);
            lblFoundPassword.TabIndex = 17;
            lblFoundPassword.Text = "Found Password:";
            // 
            // lblProgress
            // 
            lblProgress.AutoSize = true;
            lblProgress.Location = new Point(30, 235);
            lblProgress.Name = "lblProgress";
            lblProgress.Size = new Size(55, 15);
            lblProgress.TabIndex = 9;
            lblProgress.Text = "Progress:";
            // 
            // txtGeneratedPassword
            // 
            txtGeneratedPassword.Location = new Point(180, 77);
            txtGeneratedPassword.Name = "txtGeneratedPassword";
            txtGeneratedPassword.ReadOnly = true;
            txtGeneratedPassword.Size = new Size(220, 23);
            txtGeneratedPassword.TabIndex = 2;
            // 
            // txtHash
            // 
            txtHash.Location = new Point(180, 117);
            txtHash.Name = "txtHash";
            txtHash.ReadOnly = true;
            txtHash.Size = new Size(650, 23);
            txtHash.TabIndex = 5;
            // 
            // txtElapsedTime
            // 
            txtElapsedTime.Location = new Point(180, 282);
            txtElapsedTime.Name = "txtElapsedTime";
            txtElapsedTime.ReadOnly = true;
            txtElapsedTime.Size = new Size(220, 23);
            txtElapsedTime.TabIndex = 12;
            // 
            // txtAttempts
            // 
            txtAttempts.Location = new Point(180, 322);
            txtAttempts.Name = "txtAttempts";
            txtAttempts.ReadOnly = true;
            txtAttempts.Size = new Size(220, 23);
            txtAttempts.TabIndex = 14;
            // 
            // txtThreadsUsed
            // 
            txtThreadsUsed.Location = new Point(180, 362);
            txtThreadsUsed.Name = "txtThreadsUsed";
            txtThreadsUsed.ReadOnly = true;
            txtThreadsUsed.Size = new Size(220, 23);
            txtThreadsUsed.TabIndex = 16;
            // 
            // txtFoundPassword
            // 
            txtFoundPassword.Location = new Point(180, 402);
            txtFoundPassword.Name = "txtFoundPassword";
            txtFoundPassword.ReadOnly = true;
            txtFoundPassword.Size = new Size(220, 23);
            txtFoundPassword.TabIndex = 18;
            // 
            // txtLogOutput
            // 
            txtLogOutput.Location = new Point(30, 455);
            txtLogOutput.Multiline = true;
            txtLogOutput.Name = "txtLogOutput";
            txtLogOutput.ReadOnly = true;
            txtLogOutput.ScrollBars = ScrollBars.Vertical;
            txtLogOutput.Size = new Size(800, 120);
            txtLogOutput.TabIndex = 19;
            // 
            // btnCreatePassword
            // 
            btnCreatePassword.Location = new Point(430, 75);
            btnCreatePassword.Name = "btnCreatePassword";
            btnCreatePassword.Size = new Size(160, 28);
            btnCreatePassword.TabIndex = 3;
            btnCreatePassword.Text = "Create Password";
            btnCreatePassword.UseVisualStyleBackColor = true;
            btnCreatePassword.Click += btnCreatePassword_Click;
            // 
            // btnStartSingleThread
            // 
            btnStartSingleThread.Location = new Point(30, 170);
            btnStartSingleThread.Name = "btnStartSingleThread";
            btnStartSingleThread.Size = new Size(180, 35);
            btnStartSingleThread.TabIndex = 6;
            btnStartSingleThread.Text = "Start Single-Thread Attack";
            btnStartSingleThread.UseVisualStyleBackColor = true;
            btnStartSingleThread.Click += btnStartSingleThread_Click;
            // 
            // btnStartMultiThread
            // 
            btnStartMultiThread.Location = new Point(230, 170);
            btnStartMultiThread.Name = "btnStartMultiThread";
            btnStartMultiThread.Size = new Size(180, 35);
            btnStartMultiThread.TabIndex = 7;
            btnStartMultiThread.Text = "Start Multi-Thread Attack";
            btnStartMultiThread.UseVisualStyleBackColor = true;
            btnStartMultiThread.Click += btnStartMultiThread_Click;
            // 
            // btnStopAttack
            // 
            btnStopAttack.Location = new Point(430, 170);
            btnStopAttack.Name = "btnStopAttack";
            btnStopAttack.Size = new Size(120, 35);
            btnStopAttack.TabIndex = 8;
            btnStopAttack.Text = "Stop Attack";
            btnStopAttack.UseVisualStyleBackColor = true;
            btnStopAttack.Click += btnStopAttack_Click;
            // 
            // progressBarAttack
            // 
            progressBarAttack.Location = new Point(180, 232);
            progressBarAttack.Name = "progressBarAttack";
            progressBarAttack.Size = new Size(650, 25);
            progressBarAttack.TabIndex = 10;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(900, 620);
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
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Password Reset Brute Force Demo";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}