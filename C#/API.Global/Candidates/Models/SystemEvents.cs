namespace Global.Candidates.Details.Models
{
    public class CandidateLogs : List<CandidateEventItem> { }
    public class CandidateEventItem
    {
        public CandidateEventItem()
        {
            SystemEventName = string.Empty;
            JobName = string.Empty;
            CompanyName = string.Empty;
            ReasonInactivated = string.Empty;
            ScoreA = string.Empty;
            ScoreI = string.Empty;
            ScoreP = string.Empty;
            Notes = string.Empty;
            IdVerificationNotes = string.Empty;
            OptNotes = string.Empty;
            OtherVisaNotes = string.Empty;

            ActionTakenByName = string.Empty;
            InterviewContactName = string.Empty;
            InterviewerName = string.Empty;
            VerificationName = string.Empty;

            Market = string.Empty;
            InterestLevel = string.Empty;
            RespondedBy = string.Empty;

            DateOccurred = DateTime.MinValue;
            DateStart = DateTime.MinValue;
            DateEnd = DateTime.MinValue;
            DateInterviewScheduled = DateTime.MinValue;
            DateDenied = DateTime.MinValue;
        }
        public int SystemEventID { get; set; }
        public int SystemEventTypeID { get; set; }
        public string SystemEventName { get; set; }
        public DateTime DateOccurred { get; set; }
        public int AppID { get; set; }
        public int HireID { get; set; }
        public int JobID { get; set; }
        public string JobName { get; set; }
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public string ReasonInactivated { get; set; }
        public decimal HourlyRate { get; set; }
        public decimal TotalHours { get; set; }
        public int InterviewID { get; set; }
        public string ScoreA { get; set; }
        public string ScoreP { get; set; }
        public string ScoreI { get; set; }
        public bool IsOrientated { get; set; }
        public string Notes { get; set; }
        public string ActionTakenByName { get; set; }
        public string InterviewerName { get; set; }
        public string InterviewContactName { get; set; }
        public bool IsFeedback { get; set; }
        public bool IsAppDenied { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateInterviewScheduled { get; set; }

        public bool IsOPT { get; set; }
        public DateTime DateOptExpires { get; set; }
        public string OptNotes { get; set; }
        public bool IsOtherVisa { get; set; }
        public DateTime DateOtherVisaExpires { get; set; }
        public string OtherVisaNotes { get; set; }
        public bool IsIdVerified { get; set; }
        public string IdVerificationNotes { get; set; }


        public int SweType { get; set; }
        public int VerificationID { get; set; }
        public int VerificationItemID { get; set; }
        public bool IsVerificationNormal { get; set; }
        public string VerificationName { get; set; }

        public int ItemID { get; set; }
        public string Market { get; set; }
        public string InterestLevel { get; set; }
        public DateTime DateDenied { get; set; }
        public string RespondedBy { get; set; }
    }
}