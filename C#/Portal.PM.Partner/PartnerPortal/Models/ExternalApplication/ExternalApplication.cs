using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Acme.Methods;
using Acme.Models;

namespace Acme.Models.ExternalApplication
{
    public class ExternalApplication
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public string ConfigurationMeta { get; set; }
        public bool IsActive { get; set; }
        public int ExtAppID { get; set; }
        public Guid ExtAppGuid { get; set; }
        public string ExtAppName { get; set; }
        public string ExtAppDescription { get; set; }
        public string ExtAppUrl { get; set; }
        public Guid RoleGuid { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
    }
}