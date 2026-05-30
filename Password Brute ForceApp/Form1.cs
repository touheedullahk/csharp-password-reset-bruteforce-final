using PasswordBruteForceApp.Services;

namespace Password_Brute_ForceApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            PasswordGenerator generator = new PasswordGenerator();
            string password = generator.GeneratePassword();

            PasswordHasher hasher = new PasswordHasher();
            string hash = hasher.HashPassword(password);

            MessageBox.Show(
                $"Generated Password: {password}\nLength: {password.Length}\nHash: {hash}",
                "Password Generator Test"
            );
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
