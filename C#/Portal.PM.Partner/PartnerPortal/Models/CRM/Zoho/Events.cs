using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Acme.Models.CRM.Events
{
    public class Event
    {
        public string ActivityID { get; set; }
        public string EventOwner { get; set; }
        public string Subject { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public string Venue { get; set; }
        public string Description { get; set; }
        public string Tag { get; set; }
        public string RecurringActivity { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public string SmOwnerID { get; set; }
        public string RelatedTOID { get; set; }
        public string SeModule { get; set; }
    }
}