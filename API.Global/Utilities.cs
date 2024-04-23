//using System.Security.Cryptography;
using System.Text.RegularExpressions;
//using System.Web.Script.Serialization;

//using Global;
using System.Web;
using Global.Location;

/// <summary>
/// Summary description for Utilities
/// </summary>
namespace Global
{
    public partial class Utilities
    {
        public static class Format
        {

            public static string NullString(string? String)
            {
                return (string.IsNullOrWhiteSpace(String) ? "-" : String).Trim();
            }
            public static string JsonResult(Dictionary<string, string> ResultObjects)
            {
                string result = string.Empty;
                //result = (new JavaScriptSerializer()).Serialize(ResultObjects);
                return result;
            }
            public static string JsonResult(Dictionary<string, object> NestedObjects)
            {
                string result = string.Empty;
                //result = (new JavaScriptSerializer()).Serialize(NestedObjects);
                return result;
            }
            public static string JsonResult(object Object)
            {
                /*
                try
                {
                    return (new JavaScriptSerializer()).Serialize(Object);
                }
                catch
                {
                    var js = new JavaScriptSerializer()
                    {
                        MaxJsonLength = int.MaxValue
                    };
                    return js.Serialize(Object);
                }
                */
                return JsonResult(Object);
            }

            public static string PhoneNumber(string PhoneNumber)
            {
                if (PhoneNumber != null)
                {
                    string formatted = Regex.Replace(
                    PhoneNumber,
                    @"^\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*$",
                    "($1$2$3) $4$5$6-$7$8$9$10");
                    return formatted;
                }
                else return string.Empty;
            }

            public static string SqlDate(DateTime Date)
            {
                string result = Date == DateTime.MinValue ? "2000-01-01 00:00:00" : Date.Date.ToString("yyyy-MM-dd HH:mm:ss");
                return result;
            }

            public static string FriendlyDate(DateTime Date)
            {
                string d = "Earlier this year";
                DateTime now = DateTime.Now;
                TimeSpan span = now - Date;
                if (Date.Date == now.Date)
                {
                    if (span.Minutes < 35) d = "About a half hour ago";
                    if (span.Minutes < 10) d = "A few minutes ago";
                    if (span.Minutes < 3) d = "Just now";
                    if (span.Minutes >= 35) d = "About " + span.Minutes + " minutes ago";
                    if (span.Hours >= 1) d = "About an hour ago";
                    if (span.Hours >= 2) d = "A couple hours ago";
                    if (span.Hours > 2) d = "Earlier today";
                }
                else if (span.Days <= 7)
                {
                    if (span.Days == 1) d = "Yesterday";
                    if (span.Days == 2) d = "A couple days ago";
                    if (span.Days > 2) d = "Earlier this week";
                    if (span.Days > 5) d = "Last week";
                    if (span.Days == 0) d = "Today";
                    if (span.Days == -1) d = "Tomorrow";
                    if (span.Days < -2) d = "Later this week";
                    if (span.Days < 5) d = Date.DayOfWeek.ToString();

                }
                /*
                else if (span.Days >= -7)
                {
                    if (span.Days == 0) d = "Today";
                    if (span.Days == -1) d = "Tomorrow";
                    if (span.Days > -2) d = "Later this week";
                    if (span.Days > 5) d = Date.DayOfWeek.ToString();
                }
                */
                else
                {
                    if (Date.Month == now.Month && Date.Year == now.Year) d = "Earlier this month";
                    else if (Date.Year == now.Year && Date.Month == now.AddMonths(-1).Month) d = "Last month";
                    else if (Date.Year == now.Year && Date.Month != now.AddMonths(-1).Month) d = "Earlier this year";
                    else if (Date.Year == now.AddYears(-1).Year) d = "Last year";
                    else d = Date.Year.ToString();
                }
                return d;
            }

            public static DateTime CombineDateTimeStrings(string date, string time)
            {
                var d = date.Split('/');
                var t = time.Replace(" ", ":").Split(':');
                t[0] = (t[2].ToLower() == "pm" && t[0] != "12") ? (int.Parse(t[0]) + 12).ToString() : t[0];
                DateTime D = new DateTime(int.Parse(d[2]), int.Parse(d[0]), int.Parse(d[1]), int.Parse(t[0]), int.Parse(t[1]), 0);
                return D;
            }
            public static string DateToString(DateTime Date)
            {
                return (Date == DateTime.MinValue || Date == DateTime.MaxValue || Date.Date == new DateTime(2000, 1, 1)) ? "Unassigned" : Date.ToString();
            }
        }

        public static class Time
        {
            public class Current
            {
                public static DateTime UTC
                {
                    get { return DateTime.UtcNow; }
                }
                public static DateTime Server
                {
                    get { return DateTime.Now; }
                }
                public static DateTime PST
                {
                    get { return Convert.ToPst(DateTime.Now); }
                }
            }
            public class Convert
            {
                public static DateTime ToPst(DateTime dateTime)
                {
                    DateTime pst = TimeZoneInfo.ConvertTime(dateTime, TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time"));
                    return pst;
                }

            }
        }

        public static class SQL
        {
            public static string Coalesce(params string[] strings)
            {
                return strings.Where(s => !string.IsNullOrEmpty(s)).FirstOrDefault();
            }
            public static class DataConversion
            {
                public static bool? GetBoolValue(string Value)
                {
                    if (Value == null) return null;
                    else
                    {
                        string val = Value.ToString().ToLower();
                        switch (val)
                        {
                            case "true": return true;
                            case "1": return true;
                            case "false": return false;
                            case "0": return false;
                            default: return null;
                        }
                    }
                }
                public static bool? GetBoolValue(int? Value)
                {
                    if (Value == null) return null;
                    else return GetBoolValue(Value.ToString());
                }
                public static int? GetBitValue(bool? BitValue)
                {
                    if (BitValue == null) return null;
                    else return BitValue == true ? 1 : 0;
                }
            }

        }

        public static class Clean
        {
            public static string ScrubStringForPermalink(string Value) { return Value.Replace(":", "-").Replace("&", "").Replace("/", "").Replace("-", "").Replace(" ", "").Replace("'", "").Replace("\"", ""); }
            public static string ScrubStringForSqlQuery(string Value) { return Value.Replace("'", "%"); }
            public static string ScrubPhoneNumber(string PhoneNumber)
            {
                string f = Regex.Replace(PhoneNumber, @"[^\d]", "");
                return f;
            }
        }

        public class UI
        {
            public static string GetBgImage
            {
                get
                {
                    int max = 6;
                    string i = string.Empty;
                    List<string> bg = new List<string>();
                    for (var x = 1; x < max + 1; x++) { bg.Add(x + ".jpg"); }
                    var r = new Random().Next(1, max);
                    i = "Assets/Images/bg/" + bg.ToArray()[r];
                    return i;
                }
            }
            public class Render
            {
                public static string StatesDropdown(string name, int id, bool isRequired = false)
                {
                    var st = States.GetStates();
                    string l = string.Empty;
                    l += "<option value='0'>-- Select --</option>";
                    foreach (var s in st)
                    {
                        l += string.Format("<option value='{0}'{2}>{1}</option>", s.StateID, s.StateCode, (s.StateID == id ? " selected='selected'" : ""));
                    }
                    if (isRequired)
                        l = string.Format("<select placeholder='State' name='{0}' class='form-control' required='required' data-error-min-value='1'>{1}</select>", name, l);
                    else
                        l = string.Format("<select placeholder='State' name='{0}' class='form-control'>{1}</select>", name, l);
                    return l;
                }
            }
        }


        public static class UrlExtension
        {
            public static string RemoveQueryStringsSingle(string url, string key)
            {
                return RemoveQueryStrings(url, new string[] { key });
            }
            public static string RemoveQueryStrings(string url, string[] keys)
            {
                if (!url.Contains("?"))
                    return url;

                string[] urlParts = url.ToLower().Split('?');
                try
                {
                    var querystrings = HttpUtility.ParseQueryString(urlParts[1]);
                    foreach (string key in keys)
                        querystrings.Remove(key.ToLower());

                    if (querystrings.Count > 0)
                        return urlParts[0]
                          + "?"
                          + string.Join("&", querystrings.AllKeys.Select(c => c.ToString() + "=" + querystrings[c.ToString()]));
                    else
                        return urlParts[0];
                }
                catch (NullReferenceException)
                {
                    return urlParts[0];
                }
            }
        }

        /*
        public static class Encryption
        {
            private const string _securityKey = "1Zi4C0DfBx";
            public static string GenerateAuthToken(int UserID)
            {
                var u = CampusPoint.Common.Users.Candidates.GetUserAuthCreds(UserID);
                string token = string.Empty;
                try
                {
                    if(u!= null)
                    {
                        token = Encrypt(string.Format("{0}|{1}", u.Username, u.Password));
                    }
                }
                catch(Exception ex) { }
                return token;
            }
        }
        */

        public static class Markets
        {
            public static string GetMarketCode(int MarketID)
            {
                return ((DataTypes.Markets)MarketID).ToString();
                /*
                switch (MarketID)
                {
                    case 1: return "SEA";
                    case 2: return "PDX";
                    default: return "Unknown";
                }
                */
            }
            public static int GetDefaultStateIdByMarket(int MarketID)
            {
                return GetDefaultStateIdByMarket((DataTypes.Markets)MarketID);
            }

            public static int GetDefaultStateIdByMarket(DataTypes.Markets Market)
            {
                int id = 0;
                switch (Market)
                {
                    case DataTypes.Markets.PDX: id = 38; break;
                    case DataTypes.Markets.SEA: id = 1; break;
                    default: id = 1; break;
                }
                return id;
            }
        }


        public static class Environment
        {
            public static bool IsEnvironmentDebugging
            {
                get
                {
                    return System.Diagnostics.Debugger.IsAttached;
                }
            }
            public static DataTypes.EnvironmentMode Mode(string url)
            {
                url = url.ToLower();
                DataTypes.EnvironmentMode mode = DataTypes.EnvironmentMode.Production;
                switch (url)
                {
                    case string u when u.Contains("localhost"):
                        mode = DataTypes.EnvironmentMode.Dev;
                        break;
                    case string u when u.Contains("beta."):
                        mode = DataTypes.EnvironmentMode.Beta;
                        break;
                }
                return mode;
            }
            public static string Label(DataTypes.EnvironmentMode mode, bool isHtml = false)
            {
                string l = string.Empty;
                if (mode != DataTypes.EnvironmentMode.Unknown && mode != DataTypes.EnvironmentMode.Production)
                    l = string.Format("[{0}]", mode.ToString().ToUpper());
                if (isHtml) l = string.Format("<span class='dev mode-id pulse announce'>{0}</span>", l);
                return l;
            }
        }
    }
}