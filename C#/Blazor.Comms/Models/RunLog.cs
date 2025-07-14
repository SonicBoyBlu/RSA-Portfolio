namespace Models.Logs
{
    public class RunLog
    {
        public RunLog()
        {
            DateStart = DateTime.Now;
            Hour = DateTime.Now.Hour;
            IsDebug = System.Diagnostics.Debugger.IsAttached;
        }
        public int RunID { get; set; }
        public int Hour { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public int TotalUsers { get; set; }
        public int TotalSkipped { get; set; }
        public int TotalSent { get; set; }
        public int Timespan { get; set; } // in munutes

        public double Threshold { get; set; }
        public double AverageUsers { get; set; }
        public double SendMargin { get; set; }
        public bool IsAlert { get; set; }
        public bool IsDebug { get; set; }

    }
}
