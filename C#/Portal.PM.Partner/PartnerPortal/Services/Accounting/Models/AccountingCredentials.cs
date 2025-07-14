using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Acme.Services.Accounting
{
    public class AccountingCredentials
    {
        public string RealmId { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public long Expiration { get; set; }

        public bool HaveExpired()
        {
            //TDB: Need to check the Expiration against current time.
            return false;
        }
    }
}