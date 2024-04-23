namespace Global.Candidates.Details.Models
{
    public class Overview
    {
        public Overview()
        {
            DateCurrentWeek = DateTime.MinValue;
            DateFsi = DateTime.MinValue;
            DateHired = DateTime.MinValue;
            DateLastApplied = DateTime.MinValue;
            DateTimecardLastUpdate = DateTime.MinValue;
            DateTimecardWeek = DateTime.MinValue;
        }
        public int StatsID { get; set; }
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName {
            get {
             return FirstName + " " + LastName;
            } 
        }
        public string Email { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Phone3 { get; set; }
        public string PhoneLast { get; set; }
        public int MarketID { get; set; }
        public string MarketName { get; set; }

        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public int StateID { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }

        public bool IsFsiComplete { get; set; }
        public bool IsHotList { get; set; }
        public bool IsQuickStart { get; set; }
        public bool IsHired { get; set; }
        public bool IsMpc { get; set; }

        public DateTime DateFsi { get; set; }
        public decimal ScoreA { get; set; }
        public decimal ScoreP { get; set; }
        public string FsiNotes { get; set; }
        public int NumFsiMissed { get; set; }
        public string APIC { get; set; }
        public string JobAPIC { get; set; }

        public DateTime DateMpcAdded { get; set; }
        public string MpcAssigned { get; set; }
        public string MpcSpecialty { get; set; }
        public string MpcNotes { get; set; }


        public int HireID { get; set; }
        public int HireCompanyID { get; set; }
        public string HireCompanyName { get; set; }
        public int HireJobID { get; set; }
        public string HireJobName { get; set; }
        public DateTime DateHired { get; set; }

        public int NumJobsWorked { get; set; }
        public int NumJobsApplied { get; set; }
        public int NumJobsFollow { get; set; }
        public int NumCompaniesFollow { get; set; }
        public int NumJobFeedback { get; set; }
        public DateTime DateLastApplied { get; set; }
        public DateTime DateLastLogin { get; set; }

        public int NumTimecardsTotal { get; set; }
        public int NumTimecardsSubmit { get; set; }
        public int NumTimecardsApprove { get; set; }
        public int NumTimecardsUnapproved { get; set; }
        public DateTime DateCurrentWeek { get; set; }
        public DateTime DateTimecardWeek { get; set; }
        public DateTime DateTimecardLastUpdate { get; set; }
        public bool IsTimecardCurrent { get; set; }
        public string ReferralSource { get; set; }
    }
}
