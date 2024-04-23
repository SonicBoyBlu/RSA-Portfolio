using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Acme.Models
{
    public class NewletterItem
    {
        public int NewsItemID { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime DatePublished { get; set; }
    }
}