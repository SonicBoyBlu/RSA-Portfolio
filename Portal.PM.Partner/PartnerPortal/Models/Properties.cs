using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Acme.Models
{
    public class Property
    {
        public int BwPropertyID { get; set; }
        public int EscapiaID { get; set; }
        public string UnitCode { get; set; }
        public string PropertyName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public int UnitID { get; set; }
    }
}