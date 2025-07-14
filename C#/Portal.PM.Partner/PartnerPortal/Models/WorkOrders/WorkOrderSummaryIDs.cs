using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Acme.Models
{
    public class WorkOrderSummaryID
    {
        public int ID { get; set; }
        public bool IsLocked { get; set; }
        public bool IsApproved { get; set; }
    }
}