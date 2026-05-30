using PasswordBruteForceApp.Services;

namespace Password_Brute_ForceApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            PasswordHasher hasher = new PasswordHasher();
            PasswordValidator validator = new PasswordValidator();

            string originalPassword = "abc1";
            string hash = hasher.HashPassword(originalPassword);

            bool correctTest = validator.IsPasswordMatch("abc1", hash);
            bool wrongTest = validator.IsPasswordMatch("wrong", hash);

            MessageBox.Show(
                $"Original Password: {originalPassword}\n" +
                $"Hash: {hash}\n\n" +
                $"Correct password test: {correctTest}\n" +
                $"Wrong password test: {wrongTest}",
                "Password Validator Test"
                );

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
