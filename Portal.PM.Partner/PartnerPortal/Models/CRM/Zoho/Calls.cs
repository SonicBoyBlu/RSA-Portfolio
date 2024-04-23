using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Acme.Models.CRM.Calls
{
    public class Call
    {
        public string ActivityID { get; set; }
        public string Subject { get; set; }
        public string Purpose { get; set; }
        public string CallType { get; set; }
        public DateTime DateCallStart { get; set; }
        public string CallDuration { get; set; }
        public int CallDurationInSeconds { get; set; }
        public string Description { get; set; }
        public string CallResult { get; set; }
        public bool Billable { get; set; }
        public string CallOwner { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public string SMOwnerID { get; set; }
        public string CallStatus { get; set; }
        public string RelationToID { get; set; }
        public string ContactID { get; set; }
        public string ContactName { get; set; }
    }
}