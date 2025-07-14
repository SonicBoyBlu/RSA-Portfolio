using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Acme.Models
{
    [Table("Reservation")]
    public class Reservation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public bool AddTravelAgent { get; set; }
        public bool AdjustRateMatchPayout { get; set; }
        public bool AttachCopyPayment { get; set; }
        public bool CreateVirtualGuestFolder { get; set; }
        public bool InputEscapia { get; set; }
        public bool IntakeChannelRequestGuestInfo { get; set; }
        public bool PrintCreditCardAuth { get; set; }
        public bool SaveOTAItinerary { get; set; }
        public bool PSCityContractCancellation { get; set; }
        public bool PSCityContractRevision { get; set; }
        public string PSCityContractNumber { get; set; }
        public string PSCitySummaryIdNumber { get; set; }
        public bool SendCopyPaymentAccounting { get; set; }
        public bool UpdateEscapia { get; set; }
        public bool UploadGuestIdentification { get; set; }
        public DateTime Modified { get; set; }
        public DateTime? ConciergeNotified { get; set; }
        public DateTime? GuestArrivalInstructionsSent { get; set; }
        public bool ExcludePayment { get; set; }
        public bool LongTermRentalAgreement { get; set; }
        public bool FinalPaymentDueDateAddCalendar { get; set; }
        public bool SendConfirmationOTA { get; set; }
        public bool ExcludeAutomaticPayments { get; set; }
        public bool ContractSigned { get; set; }

        // Fields are incoming from Esacpia API
        // Stored in DB
        public string ReservationNumber { get; set; }
        public string Status { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public bool PaymentStatusFull { get; set; }
        public string BookingSource { get; set; }
        public string ReservationType { get; set; }
        public DateTime? ReservationCreatedAt { get; set; }


        //Not stored in DB
        [NotMappedAttribute]
        public string ConciergeName { get; set; }
        [NotMappedAttribute]
        public string PSCityIdNumber { get; set; }
        [NotMappedAttribute]
        public bool PostPayment { get; set; }
        [NotMappedAttribute]
        public string GuestArrivalInstructionsSentString { get; set; }
        [NotMappedAttribute]
        public string ConciergeNotifiedString { get; set; }
        
    }
    [Table("ReservationCategory")]
    public class ReservationCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Name { get; set; }
        public int NativePMSID { get; set; }
        public string CorrespondingInquirySource { get; set; }
        public DateTime Modified { get; set; }

    }
    [Table("ReservationSource")]
    public class ReservationSource
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Name { get; set; }
        public int NativePMSID { get; set; }
        public int ReservationCategoryID { get; set; }
        public string CorrespondingInquirySource { get; set; }
        public DateTime Modified { get; set; }
    }
    [Table("ReservationEscapiaJson")]
    public class ReservationEscapiaJson
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string JsonRaw { get; set; }
        public string JsonModel { get; set; }
        public int TotalReservations { get; set; }
        public string Duration { get; set; }
        public DateTime Modified { get; set; }

    }
    [Table("ReservationType")]
    public class ReservationType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int NativePMSID { get; set; }
        public string Name { get; set; }
        public string Initial { get; set; }
        public string OwnerStayType { get; set; }
        public DateTime Modified { get; set; }
    }
}