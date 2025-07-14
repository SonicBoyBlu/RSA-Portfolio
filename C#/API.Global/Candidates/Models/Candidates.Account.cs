namespace Global.Candidates.Models
{
    public class User
    {
        public User()
        {
            Username = string.Empty;
            Password = string.Empty;
            FirstName = string.Empty;
            MiddleName = string.Empty;
            LastName = string.Empty;
            NamePrefix = string.Empty;
            Title = string.Empty;
            Email = string.Empty;
            PhonePrimary = string.Empty;
            PhoneSecondary = string.Empty;
            AvartUrl = string.Empty;
            
            FsiStatus = string.Empty;
            FsiScoreA = string.Empty;
            FsiScoreP = string.Empty;

            SignupLocation = string.Empty;

            LoginStatusMessage = "unknown";
            AuthToken = string.Empty;

            DateLastLogin = DateTime.MinValue;
            DateCreated = DateTime.MinValue;
            UserID = 0;
            UserType = DataTypes.UserType.Guest;
            Market = DataTypes.Markets.All; // Utilities.GetDefaultMarket();
            IsSmsEnabled = true;
            Dashboard = "Home";
        }
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string NamePrefix { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
        public DateTime DateCreated { get; set; }
        public DateTime DateLastLogin { get; set; }
        public DataTypes.UserType UserType { get; set; }
        public int CompanyID { get; set; }
        public string Email { get; set; }
        public string PhonePrimary { get; set; }
        public string PhoneSecondary { get; set; }
        public int MarketID { get; set; }
        public DataTypes.Markets Market { get; set; }
        public bool IsActive { get; set; }
        public string Dashboard { get; set; }
        public bool IsLogoAdmin { get; set; }
        public bool IsHired { get; set; }
        public bool ShowTimecard { get; set; }
        public bool IsTest { get; set; }
        public bool IsCpUser { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsBanned { get; set; }

        public bool IsSmsEnabled { get; set; }
        public bool IsPushEnabled { get; set; }

        public bool IsPayroll { get; set; }
        public bool IsAgreedTermsOfUse { get; set; }

        public string FsiStatus { get; set; }
        public DateTime DateFsiSchedule { get; set; }
        public string FsiScoreA { get; set; }
        public string FsiScoreP { get; set; }
        public bool IsSignupSheet { get; set; }
        public int SignupEventID { get; set; }
        public string SignupLocation { get; set; }
        public DateTime DateSignup { get; set; }

        public bool IsLoginSuccess { get; set; }
        public string LoginStatusMessage { get; set; }

        public string AuthToken { get; set; }
        public string AvartUrl { get; set; }
    }


    public class SetRegistration
    {
        public SetRegistration()
        {
            Password = string.Empty;
            FirstName = string.Empty;
            MiddleName = string.Empty;
            LastName = string.Empty;
            Email = string.Empty;
            Phone = string.Empty;

            ReferralName = string.Empty;
            FacebookAccessToken = string.Empty;

            UserPreID = 0;
            Notes = string.Empty;
            JobID = 0;
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int MarketID { get; set; }
        public int ReferralID { get; set; }
        public string ReferralName { get; set; }
        public string FacebookAccessToken { get; set; }

        public int UserID { get; set; }
        public int UserPreID { get; set; }
        public string Notes { get; set; }
        public int JobID { get; set; }
        public int CpUserID { get; set; }

        //public string ResumeData { get; set; }
        //public Resumes.HireAbility.HireAbilityJSONResults ResumeData { get; set; }
        //public Resumes.HireAbility.ResumeParserRequest FileInfo { get; set; }
    }


    public class RequestVerifyAccountRecovery
    {
        public RequestVerifyAccountRecovery()
        {
            Username = string.Empty;
            Token = string.Empty;
            DateLogged = DateTime.MinValue;
        }
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
        public int NumAttempts { get; set; }
        public DateTime DateLogged { get; set; }
        public int RecoveryCode { get; set; }
    }

}
