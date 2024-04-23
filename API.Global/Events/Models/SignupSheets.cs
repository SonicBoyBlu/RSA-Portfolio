namespace Global.Events.Models.SignupSheets
{
    public class SignupSheetItem
    {
        public SignupSheetItem()
        {
            UserType = "n";
            FirstName = string.Empty;
            LastName = string.Empty;
            Email = string.Empty;
            Phone = string.Empty;
            MajorName = string.Empty;
            Notes = string.Empty;
            EventName = string.Empty;
            Location = string.Empty;
        }
        public int UserPreID { get; set; }
        public int UserID { get; set; }
        public int CpUserID { get; set; }
        public int SignupSheetID { get; set; }
        public int SignupSheetItemID { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int GradMonth { get; set; }
        public int GradYear { get; set; }
        public string GradDate
        {
            get
            {
                return GradMonth + "/" + GradYear;
            }
        }
        public int MajorID { get; set; }
        public string MajorName { get; set; }
        public float ScoreA { get; set; }
        public float ScoreP { get; set; }
        public string Notes { get; set; }
        public string UserType { get; set; }

        public int EventID { get; set; }
        public string EventName { get; set; }
        public string Location { get; set; }

        public bool IsRegistered { get; set; }
        public bool IsAccountCreate { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateRegistered { get; set; }
    }
}