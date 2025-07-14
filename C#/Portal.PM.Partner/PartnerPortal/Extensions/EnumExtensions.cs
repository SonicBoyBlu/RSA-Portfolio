using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Acme.Extensions
{
    public static class Enums
    {
        /// <summary>
        /// Converts an enum to a KeyValuPair List
        /// </summary>
        /// <typeparam name="T">Enum</typeparam>
        /// <returns>KeyValuePaire List representation of the enum</returns>
        public static List<KeyValuePair<string, int>> ToList<T>() where T : struct
        {
            var result = new List<KeyValuePair<string, int>>();

            if (!typeof(T).IsEnum) return result;

            var enumType = typeof(T);
            var values = Enum.GetValues(enumType);
            foreach (var value in values)
            {
                var memInfo = enumType.GetMember(enumType.GetEnumName(value));
                var descriptionAttributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                var description = descriptionAttributes.Length > 0
                    ? ((DescriptionAttribute)descriptionAttributes.First()).Description
                    : value.ToString();
                result.Add(new KeyValuePair<string, int>(description, (int)value));
            }

            return result;
        }
    }
}