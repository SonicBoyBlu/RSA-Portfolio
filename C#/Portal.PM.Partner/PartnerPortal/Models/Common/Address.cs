using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Acme.Models.Common
{
    public class Address
    {
        public int PropertyID { get { return NativePMSID; } }
        public int NativePMSID { get; set; }
        public string Name { get; set; }
        public string UnitCode { get; set; }
        public string UnitHeadline { get; set; }
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
    }
}