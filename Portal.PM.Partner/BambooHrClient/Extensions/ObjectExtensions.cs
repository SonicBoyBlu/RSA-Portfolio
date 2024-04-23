﻿using System.Text;

namespace BambooHrClient.Extensions
{
    public static class ObjectExtensions
    {
        public static string PropsToString(this object thing)
        {
            var propertyInfos = thing.GetType().GetProperties();

            var sb = new StringBuilder();

            foreach (var info in propertyInfos)
            {
                var value = info.GetValue(thing, null) ?? "(null)";
                sb.AppendLine(info.Name + ": " + value);
            }

            return sb.ToString();
        }
    }
}
