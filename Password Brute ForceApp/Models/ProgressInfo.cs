namespace Password_Brute_ForceApp.Models
{
    public class ProgressInfo
    {
        public long AttemptsChecked { get; set; }

        public long TotalCombinations { get; set; }

        public int CurrentLength { get; set; }

        public int ProgressPercentage { get; set; }
    }
}