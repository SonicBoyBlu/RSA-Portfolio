namespace Global.Candidates.Details.Models
{
    public class Status
    {
        public int UserID { get; set; }
        public int StatusID { get; set; }
        public string StatusNotes { get; set; }
        public DateTime StatusDate { get; set; }
        public bool IsMPC { get; set; }
        public string MpcNotes { get; set; }
        public DateTime MpcDate { get; set; }
        public bool IsQuickStart { get; set; }
        public bool IsFSI { get; set; }
        public string FsiStatus { get; set; }
        public string APIC_A { get; set; }
        public string APIC_P { get; set; }
        public string ScoreP_Raw { get; set; }
        public string FsiNotes { get; set; }
        public DateTime FsiDate { get; set; }
        public int PastHires { get; set; }
        public int AppCount { get; set; }
        public bool ShowAppCount { get; set; }
        public DateTime DateConverted { get; set; }
        public DateTime DateDirect { get; set; }
        public int ResumeScore { get; set; }
        public string ExecutiveSummary { get; set; }
    }
}
