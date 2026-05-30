using PasswordBruteForceApp.Services;

namespace Password_Brute_ForceApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            PasswordHasher hasher = new PasswordHasher();
            string testHash = hasher.HashPassword("test");

            MessageBox.Show(testHash, "SHA256 Test Hash");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
