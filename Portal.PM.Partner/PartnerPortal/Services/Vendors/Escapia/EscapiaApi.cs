using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Acme.Services.Vendors.Escapia
{
    public class EscapiaApi
    {
        public static HttpClient GetHttpClientDefaults()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("x-homeaway-hasp-api-pmcid", GlobalSettings.Escapia.PMCID.ToString());
            httpClient.DefaultRequestHeaders.Add("x-homeaway-hasp-api-version", "10");
            httpClient.DefaultRequestHeaders.Add("x-homeaway-hasp-api-endsystem", "EscapiaVRS");
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + GlobalSettings.Escapia.HomeAwayCredentials.Token);
            return httpClient;
        }
    }
}