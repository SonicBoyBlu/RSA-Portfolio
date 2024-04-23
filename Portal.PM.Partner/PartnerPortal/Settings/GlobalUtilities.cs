using System;
using System.Linq;

namespace Acme
{
    public class GlobalUtilities
    {
        public static string Coalesce(params string[] strings)
        {
            return strings.Where(s => !string.IsNullOrEmpty(s)).FirstOrDefault();
        }

        // http://stackoverflow.com/questions/13467230/how-to-set-time-to-midnight-for-current-day/39962449#39962449
        // Set date with the time component set to 00:00:00
        public static DateTime GetDateZeroTime(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
        }
        public static string GetDateString(DateTime date)
        {
            return date.ToString("MM/dd/yyyy");
        }
        public static string GetDateString(DateTime? date)
        {
            return date == null ? "" :  ((DateTime)date).ToString("MM/dd/yyyy");
        }

        public static bool ParseJSCheckbox(object str)
        {
            return  (str.ToStringOrDefault().Trim().ToLower() == "on");
        }

        public static string ToPrettyDate(DateTime date)
        {
            var timeSince = DateTime.Now.Subtract(date);
            if (timeSince.TotalMilliseconds < 1) return "<span class='label'>not yet</span>";
            if (timeSince.TotalMinutes < 1) return "just now";
            if (timeSince.TotalMinutes < 2) return "1 minute ago";
            if (timeSince.TotalMinutes < 60) return string.Format("{0} minutes ago", timeSince.Minutes);
            if (timeSince.TotalMinutes < 120) return "1 hour ago";
            if (timeSince.TotalHours < 24) return string.Format("{0} hours ago", timeSince.Hours);
            if (timeSince.TotalDays == 1) return "yesterday";
            if (timeSince.TotalDays < 7) return string.Format("{0} day(s) ago", timeSince.Days);
            if (timeSince.TotalDays < 14) return "last week";
            if (timeSince.TotalDays < 21) return "2 weeks ago";
            if (timeSince.TotalDays < 28) return "3 weeks ago";
            if (timeSince.TotalDays < 60) return "last month";
            if (timeSince.TotalDays < 365) return string.Format("{0} months ago", Math.Round(timeSince.TotalDays / 30));
            if (timeSince.TotalDays < 730) return "last year";
            if (date == DateTime.MinValue) return "<span class='label'>not yet</span>";
            return string.Format("{0} years ago", Math.Round(timeSince.TotalDays / 365));
        }

    }
}