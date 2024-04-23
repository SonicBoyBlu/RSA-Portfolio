using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace Acme.Services.Vendors.Escapia
{
    public class EscapiaToken
    {
        public static async Task SetEscapiaToken()
        {
            using (var httpClient = new HttpClient())
            {

                httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + @GlobalSettings.Escapia.HomeAwayCredentials.Encoded);
                httpClient.DefaultRequestHeaders.Add("Host", "hsapi.escapia.com");
                //httpClient.DefaultRequestHeaders.Add("Host", "dispatch.homeaway.com");
                httpClient.DefaultRequestHeaders.Add("x-homeaway-hasp-api-version", "10");
                httpClient.DefaultRequestHeaders.Add("x-homeaway-hasp-api-endsystem", "EscapiaVRS");
                var tokenURL = "https://hsapi.escapia.com/dragomanadapter/hsapi/auth/token";
                var tokenResponse = await httpClient.GetAsync(tokenURL); 
                var tokenJsonString = await tokenResponse.Content.ReadAsStringAsync();

                // Escapia API sends $id which is an internal escapia and is not needed
                // to parse $id we use MetadataPropertyHandling and applied [JsonProperty("$id")] and name to metaid in class
                // ---------------------------------------------
                JsonSerializerSettings tokenSettings = new JsonSerializerSettings();
                tokenSettings.MetadataPropertyHandling = MetadataPropertyHandling.Ignore;

                TokenRequestJson tokenObject = JsonConvert.DeserializeObject<TokenRequestJson>(tokenJsonString, tokenSettings);

                if (tokenObject.IsNull())
                    throw new Exception("SetEscapiaToken FAILED Acme.Services.Vendors.Escapia()");

                GlobalSettings.Escapia.HomeAwayCredentials.Token = tokenObject.token;

            }
        }

        private class TokenRequestJson
        {
            [JsonProperty("$id")]
            public string metaid { get; set; }
            public string expiration { get; set; }
            public string id { get; set; }
            [JsonProperty("encodedId")]
            public string token { get; set; }
            public string authorizationHeaderValue { get; set; }
        }
    }
}