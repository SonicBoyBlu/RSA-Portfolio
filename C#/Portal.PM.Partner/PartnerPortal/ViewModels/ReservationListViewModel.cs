using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Acme.Models.ExternalApplication;

namespace Acme.ViewModels
{
    public class ReservationListViewModel
    {
        public string MetaID;
        public string ReservationNumber { get; set; }
        public string Status { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PaymentStatus { get; set; }
        public bool PaymentStatusFull { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public bool UploadGuestIdentification {get;set;}
        public bool HasConcierge { get; set; }
        public bool IsPSCityContract { get; set; }
        public bool SentArrivalInstructions { get; set; }
        public string BookingSource { get; set; }
        public string ReservationType { get; set; }
        public string ReservationTypeInitial { get; set; }

        public Models.Reservation Reservation { get; set; }

        public Escapia.APIReturn.Unit Unit { get; set; }

        public List<Acme.Models.ExternalApplication.Escapia.Note> Notes { get; set; }

        public List<string> PalmSpringsContractNumberOptions { get; set; }

        //Ad-Hoc API additionalProperties > name = 'Reservations Form'
        public string TOTNumber { get; set; }

        public DateTime? ReservationCreatedAt { get; set; }

    }

    public class ReservationTypeViewModel
    {
        public int ID { get; set; }
        // public int NativePMSID { get; set; }
        public string Name { get; set; }
        public string Initial { get; set; }
        // public string OwnerStayType { get; set; }
        // public DateTime Modified { get; set; }
    }

}
