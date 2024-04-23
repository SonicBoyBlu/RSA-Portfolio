using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Acme.Models.CRM.Tasks
{
    public class Task
    {
        public string ActivityID { get; set; }
        public string TaskOwner { get; set; }
        public string Subject { get; set; }
        public DateTime DateDue { get; set; }
        public DateTime DateClosed { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public string Tag { get; set; }
        public bool IsSendNotificationEmail { get; set; }
        public string RecurringActivity { get; set; }
        public string Description { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public string SmOwnerID { get; set; }
        public string RelatedTo { get; set; }
        public string ContactID { get; set; }
        public string SeModule { get; set; }
    }
}