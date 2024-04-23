using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Acme.Models.ExternalApplication
{
    public class QuickBooks
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public string RealmId { get; set; }
    }
}