/// <summary>
/// Global settings across all apps
/// </summary>
namespace Global
{
	public class Settings
	{
		public class Defaults
		{
			public static int TestUserID = 127697;

		}
		public class Time
		{
			public static DateTime GetCurrentUtc
			{
				get
				{
					return DateTime.Now.ToUniversalTime();
				}
			}
			public const int second = 1000;
			public const int minute = 60 * second;
			public const int hour = 60 * minute;
			public class Format
			{
				public const string Date = "yyyy-MM-dd";
			}

			public static DateTime SystemTimeHQ
			{
				get
				{
					/*
					var dateTimeUnspec = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
					DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(dateTimeUnspec, 
						TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time"));
					*/
					var localTime = DateTime.UtcNow.AddHours(-7);
					return localTime;
					//return DateTime.Now.AddHours(-8);
				}
			}
		}
		public class Application
		{
			public static bool IsDebug
			{
				get
				{
					return System.Diagnostics.Debugger.IsAttached;
				}
			}
			public class Version
			{
				public static string API = "2.1.27";
				public static string WWW = "6.0.481.0";
				// RSA: 06-05-2023
				//public static string WWW = "5.7.6.13";
				public static string Caesar = "23.1.1.9";
				public static string Candidates = "3.2.23";
				public static string Blazor = "1.3.10";
				public static string Cloud = "3.7.13";
				public static string Singups = "3.2.13";
			}
		}

		public class API
		{
			public static string AccessTokenBase = "AccessTokenBase";
			public static string AccessToken
			{
                //get { return Utilities.Cryptography.Encrypt(AccessTokenBase); }
                get { return string.Empty; }
            }
        }

		public class Urls
		{
			public static string Logos = "https://logos.campuspoint.com/";
			public static string Ads = "https://cp-adds.azureedge.net/";
			public static class BlazorServer
			{
				public static string Debug = "https://localhost:7148/";
				public static string Production = "https://bzserver.campuspoint.com/";
			}
		}

		public class CompanyDetails
		{
			public const string CompanyName = "CompanyName";

			public static string OlBlueUrl = "https://www2.campuspoint.com";

			public class Addresses
			{
				
			}
		}

		public class DataStores
		{
			public static string DefaultConnectionString
			{
				get
				{
					if (_defaultconnectionstring == null)
						_defaultconnectionstring =
								"_defaultconnectionstring";
					return _defaultconnectionstring;
				}
			}

			public static string TicketsConnectionString = "TicketsConnectionString";

			//private static string _externalip;
			private static string _defaultconnectionstring;

			public static string DefaultImageLocation = "assets/images/";

			public static string AzureBlobWebFiles = "DefaultEndpointsProtocol=https;AccountName=webfiles;AccountKey=AccountKey;EndpointSuffix=core.windows.net";
		}

		public class MailServer
		{

			public static string SMTPServerUrl = "localhost";
			public static int SMTPServerPort = 25;
			public static string SMTPServerDomain = "SMTPServerDomain";
			public static bool SMTPSecureConnectionRequired = false;
			public static string SMTPServerLoginName = "";
			public static string SMTPServerPassword = "";
			public static string WebmasterEmailAddress = "NoReplyEmailAddress";
			public static string NoReplyEmailAddress = "NoReplyEmailAddress";

		}

		public class CDN
		{
			public static string Root = "https://?.blob.core.windows.net/";

			public class Images
			{
				public static string www = Root + "images/www/";
			}
			public class Plugins
			{
				public static string www = Root + "Plugins/";
			}
		}
		public class Social
		{
			public class Facebook
			{
				public string LoginAppID = "LoginAppID";
				public string LoginAppSecret = "LoginAppSecret";
				public string RegistrationAppScope = "public_profile,email,user_about_me,public_profile,user_education_history,user_birthday,user_hometown,user_location,user_work_history";
				public string BaseUrl = "https://graph.facebook.com/oauth/{0}?client_id={1}&redirect_uri={2}&scope={3}";

			}
		}

		public class MiscKeys
		{
			public static string RememberMeCookieName = "CPRM";
			public static string CryptographySalt = "CryptographySalt!";
		}

		public class SearchSettings
		{
			public static bool ShowPublicOnly = true;
		}
	}
}