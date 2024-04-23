using Global.Location;
/// <summary>
/// Summary description for Events
/// </summary>
namespace Global.Events.Models
{
    public class EventsList : List<Event>
    {
        public EventsList()
        {
        }
    }
    public class Event
    {
        public Event()
        {
            DateStart = DateTime.MinValue;
            DateEnd = DateTime.MinValue;
            Address = new Address();
            IsActive = true;
        }
        public int EventID { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public string Location { get; set; }
        public int SchoolID { get; set; }
        public bool HasAddress { get; set; }
        public Address Address { get; set; }
        public string Description { get; set; }
        public string EventBuilding { get; set; }
        public string EventRoom { get; set; }
        public string Name { get; set; }
        public string NameShort { get; set; }
        public string Url { get; set; }
        public string AdditionalNotes { get; set; }
        public int MarketID { get; set; }
        public DataTypes.Markets Market { get; set; }
        public string MarketName { get; set; }
        public int SignupSheetID { get; set; }
        public int SignupSheetCount { get; set; }
        public int SignupSheetPreRegCount { get; set; }
        public bool isPublic { get; set; }
        public bool IsActive { get; set; }
        public bool IsOnline { get; set; }
    }

    public class RequestEventsList
    {
        public RequestEventsList()
        {
            IsActive = true;
            DateStart = DateTime.MinValue;
            DateEnd = DateTime.MinValue;
            /*
            UserID = Identity.Current.UserID;
            NumMonthsBack = 12;
            SortOrder = 3;
            SortDir = 1;
            */
        }

        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public int MarketID { get; set; }
        public bool IsActive { get; set; }
        /*
        public int UserID { get; set; }
        public int NumMonthsBack { get; set; }
        public int SortOrder { get; set; }
        public int SortDir { get; set; }
        */
    }

    public class ResponseEventsList
    {
        public ResponseEventsList()
        {
            Events = new EventsList();
            Status = string.Empty;
            Message = string.Empty;
        }
        public EventsList Events { get; set; }
        public bool Success { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
    }

    public class SaveResponseEvent
    {
        public SaveResponseEvent()
        {
            Event = new Event();
            Status = string.Empty;
            Message = string.Empty;
        }
        public Event Event { get; set; }
        public bool Success { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
    }

    /*
    public class RequestEvents
    {
        public int EventID { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public int MarketID { get; set; }
        public DataTypes.Markets Market { get; set; }
        public string MarketName { get; set; }
        public int SignupSheetID { get; set; }
        public int SignupSheetCount { get; set; }
        public int SignupSheetPreRegCount { get; set; }
    }
    */

    public class EventAdminDetails
    {
        public EventAdminDetails()
        {
            DateEntered = DateTime.Now;
            DateLastUpdated = DateTime.Now;
            //EnteredByID = Identity.Current.UserID;
            //LastUpdatedByID = Identity.Current.UserID;
        }
        public int ID { get; set; }
        public int EventID { get; set; }
        public int SignupSheetID { get; set; }

        public bool IsAutoYes { get; set; }
        public bool IsTry { get; set; }
        public bool IsSend2People { get; set; }
        public int SignupGoal { get; set; }
        public int SignupsLastYear { get; set; }
        public int SignupsThisYear { get; set; }
        public int TotalRegistrations { get; set; }
        public int Cost { get; set; }
        public bool IsRegistered { get; set; }
        public bool? IsPaid { get; set; }
        public bool IsCalendar { get; set; }

        public string Notes { get; set; }
        public string NotesAttendAgain { get; set; }
        public string NotesAfter { get; set; }

        public DateTime DateEntered { get; set; }
        public DateTime DateLastUpdated { get; set; }

        public int EnteredByID { get; set; }
        public int LastUpdatedByID { get; set; }
    }

    public class EventAdminDetailsReport : List<EventAdminDetailsItem> { }
    public class EventAdminDetailsItem : EventAdminDetails
    {
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public string EventName { get; set; }
        public string EventLocation { get; set; }
        public int MarketID { get; set; }
        public string MarketCode { get; set; }
        public string EnteredByName { get; set; }
        public string LastUpdatedByName { get; set; }
    }
}