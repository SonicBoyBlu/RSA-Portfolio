using Intuit.Ipp.OAuth2PlatformClient;
using System.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Acme.Services.Vendors
{
    public class QuickBooksOnline
    {
        public static OAuth2Client auth2Client {
            get
            {
                return
                new OAuth2Client(
                    ConfigurationManager.AppSettings["qbo-clientid"],
                    ConfigurationManager.AppSettings["qbo-clientsecret"],
                    ConfigurationManager.AppSettings["qbo-redirectUrl"],
                    ConfigurationManager.AppSettings["qbo-environment"]
                );
            }
        }
    }
}