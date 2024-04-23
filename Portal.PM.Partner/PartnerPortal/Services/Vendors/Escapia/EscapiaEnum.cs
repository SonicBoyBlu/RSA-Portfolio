using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Acme.Services.Vendors.Escapia
{
    public class EscapiaEnum
    {
        /// <summary>
        /// Used for PSCityContractNumber take from
        /// https://form.jotform.us/CityofPalmSprings/AgencyContract
        /// </summary>
        /// <returns></returns>
        public static List<string> PalmSpringsContractNumberOptions()
        {
            return new List<string>()
            {
                "3rd Q - #1",
                "3rd Q - #2",
                "3rd Q - #3",
                "3rd Q - #4",
                "1",
                "2",
                "3",
                "4",
                "5",
                "6",
                "7",
                "8",
                "9",
                "10",
                "11",
                "12",
                "13",
                "14",
                "15",
                "16",
                "17",
                "18",
                "19",
                "20",
                "21",
                "22",
                "23",
                "24",
                "25",
                "26",
                "27",
                "28",
                "29",
                "30",
                "31",
                "32"
            };
        }
    }
}