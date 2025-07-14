using System.Configuration;
using System.Web;

namespace GlobalSettings
{
    public class ServerInfo
    {
        public static bool IsProductionOverride = true;
        public static bool IsSpa
        {
            get
            {
                return false;
            }
        }

        public static string InternalIp = "127.0.0.1";
        public static string IpAddress
        {
            get {
                if (_ipaddress == null)
                    _ipaddress = HttpContext.Current.Request.ServerVariables["LOCAL_ADDR"]; ;
                return _ipaddress;
            }
        }
        private static string _ipaddress;

        public static string Url
        {
            get
            {
                if (_url == null)
                    _url = HttpContext.Current.Request.Url.AbsoluteUri;
                return _url;
            }
        }
        private static string _url;

        public static bool IsInternal
        {
            get { return IpAddress == InternalIp; }
        }
        public static bool IsSandbox
        {

            get 
            {
                return !Url.ToLower().Contains("//portal."); 
            }
        }
    }

    public class DateStores
    {
        private static string SqlServerIp {
            get {
                if (_sqlServerIp == null)
                    _sqlServerIp = ServerInfo.InternalIp +",5000";
                return _sqlServerIp;
            }
        }
        private static string _sqlServerIp;

        private static string DatabaseName(string dbName)
        {
            switch (dbName.ToLower())
            {
                case "breezeway": dbName = ServerInfo.IsSandbox ? (ServerInfo.IsProductionOverride ? "Breezeway2" : "BreezewaySandbox") : "Breezeway2"; break;
            }
            return dbName;
            //return dbName + (ServerInfo.IsSandbox ? "Sandbox" : "");
        }
        private static string FormatConnectionString(string database)
        {
            return string.Format("user id=;data source={0};initial catalog={1};password=", SqlServerIp, DatabaseName(database));
        }


        public static string AcmePortal
        {
            get
            {
                if (_acmePortal == null)
                    _acmePortal = FormatConnectionString("AcmePortal");
                return _acmePortal;
            }
        }
        private static string _acmePortal;
        public static string Breezeway
        {
            get
            {
                if (_breezeway == null)
                    _breezeway = FormatConnectionString("Breezeway");
                return _breezeway;
            }
        }
        private static string _breezeway;

        public static string CRM
        {
            get
            {
                if (_crm == null)
                    _crm = FormatConnectionString("AcmeCRM");
                return _crm;
            }
        }
        private static string _crm;
        public static string BambooHR
        {
            get
            {
                if (_bamboohr == null)
                    _bamboohr = FormatConnectionString("BambooHR");
                return _bamboohr;
            }
        }
        private static string _bamboohr;
        public static string SmarterTrack
        {
            get
            {
                if (_smartertrack == null)
                    _smartertrack = FormatConnectionString("SmarterTrack");
                return _smartertrack;
            }
        }
        private static string _smartertrack;
    }

    public class Images
    {
        public static string DefaultUser = "/Content/images/generic-user2.png";
    }

    public class Zoho
    {
        public static string OrganizationID = "OrganizationID";
        public class Credentials
        {
            public static string Username = "Username";
            public static string Password = "Password";
            public static string AuthToken { get; set; }
        }
        public static int SessionTimeout = 15;
    }

    public class BambooHR
    {
        public class Credentials
        {
            public static string BambooApiUser = "Username";
            public static string BambooApiKey = "ApiKey";
            public static string BambooApiUrl = "url";
            public static string BambooCompanyUrl = "https://?.bamboohr.com";

        }
    }

    public class Escapia
    {
        public static int PMCID = 1596;
        public class HomeAwayCredentials
        {
            public static string ClientID = "ClientID";
            public static string ClientSecret = "ClientSecret";
            public static string Encoded
            {
                get
                {
                    var bytes = System.Text.Encoding.UTF8.GetBytes(string.Format("{0}:{1}", ClientID, ClientSecret));
                    return System.Convert.ToBase64String(bytes);
                }
            }
            public static string Token
            {
                get { return _token; }
                set { _token = value; }
            }
            public static string _token;

        }
    }

    public class QuickBooksOnline
    {
        public static string Token { get; set; }
        public static string RefresToken { get; set; }
    }

    public class ExternalApplication {
        public const  int BambooHR = 1;
        public const int SmarterTrack = 2;
        public const int QuickBooks = 3;
        public const int Escapia = 4;
        public const int JitBit = 5;
    }
}