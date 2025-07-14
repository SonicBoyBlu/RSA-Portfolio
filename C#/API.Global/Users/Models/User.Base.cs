using System.ComponentModel;

namespace Global.Users.Models
{
    public class User
    {
        public User()
        {
            DateLastLogin = DateTime.MinValue;
            DateCreated = DateTime.MinValue;
            DateFsiSchedule = DateTime.MinValue;

            UserID = 0;
            UserType = DataTypes.UserType.Guest;
            Market = DataTypes.Markets.All; // Utilities.GetDefaultMarket();
            StateAbr = "-";
            IsSmsEnabled = true;
            Dashboard = "Home";

            //DateSignup = DateTime.MinValue;
            FsiScoreA = string.Empty;
            FsiScoreP = string.Empty;
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
        public int StateID { get; set; }
        public string StateAbr { get; set; }
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

        public string FsiScoreA { get; set; } = string.Empty;
        public string FsiScoreP { get; set; } = string.Empty;

        public bool IsSignupSheet { get; set; }
        public int SignupEventID { get; set; }
        public string SignupLocation { get; set; }
        public DateTime? DateSignup { get; set; }

        public bool IsLoginSuccess { get; set; }
        public string LoginStatusMessage { get; set; }

        public string AuthToken { get; set; }
        public string AvartUrl { get; set; }
    }
}
