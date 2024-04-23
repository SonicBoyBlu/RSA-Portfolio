using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Acme.Services.Vendors.Escapia
{
    public class AdditionalPropertyValues
    {
        public static ViewModels.ReservationListViewModel FillAdHockAdditionalPropertyValues(ViewModels.ReservationListViewModel data)
        {
            if (data.IsNull() || data.Unit.IsNull() || data.Unit.additionalProperties.IsNullOrEmpty())
                return data;

            var strAdditionalPropertiesForReservationsForm = (from x in data.Unit.additionalProperties
                where x.name.ToLower() == "reservations form"
                select x.value).FirstOrDefault().ToStringOrDefault();

            if (strAdditionalPropertiesForReservationsForm.IsNullOrEmpty())
                return data;

            // "CITY ID: ABC123\nConcierge: Steve" sample
            string[] rows = strAdditionalPropertiesForReservationsForm.Split(
                new[] { "\r\n", "\r", "\n", "," },
                StringSplitOptions.None
            );

            // "CITY ID: ABC123" sample
            foreach (var line in rows)
            {
                if (line.IsNullOrEmpty())
                    continue;

                string[] row = line.Split(
                    new[] { ":" },
                    StringSplitOptions.None
                );

                if (row.IsNullOrEmpty() || row.Length < 2)
                    continue;

                if (line.ToLower().Contains("city"))
                {
                    data.Reservation.PSCityIdNumber = row[1].ToStringOrDefault().Trim();
                }

                if (line.ToLower().Contains("tot"))
                {
                    data.TOTNumber = row[1].ToStringOrDefault().Trim();
                }

                if (line.ToLower().Contains("conc"))
                {
                    data.Reservation.ConciergeName = row[1].ToStringOrDefault().Trim();
                }

            }

            return data;
        }
    }
}