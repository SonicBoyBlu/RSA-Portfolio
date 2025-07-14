using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Acme.Methods;
using Acme.Models;
using Intuit.Ipp.Data;


namespace Acme.Models.ExternalApplication
{
    public class ExternalApplications
    {
        public Models.ExternalApplication.BambooHR BambooHR { get; set; }
        public Models.ExternalApplication.Escapia.Owner Escapia { get; set; }
        public Models.ExternalApplication.QuickBooks QuickBooks { get; set; }
        public Models.ExternalApplication.SmarterTrack SmarterTrack { get; set; }
        public Models.ExternalApplication.JitBit JitBit { get; set; }
    }
}