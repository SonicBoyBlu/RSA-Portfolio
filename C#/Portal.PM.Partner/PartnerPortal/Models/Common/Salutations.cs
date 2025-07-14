using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Acme.Models.Common
{
    public class Salutation
    {
        public int SalutationTypeID { get; set; }
        public string SalutationType { get; set; }
        public bool IsActive { get; set; }
    }
}