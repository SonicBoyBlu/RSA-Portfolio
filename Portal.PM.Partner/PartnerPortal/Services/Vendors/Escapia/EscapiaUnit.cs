using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace Acme.Services.Vendors.Escapia
{
    public class EscapiaUnit
    {
        public EscapiaUnit()
        {
            
        }

        public async Task<List<Models.ExternalApplication.Escapia.APIReturn.Unit>> FetchUnitsByArrayFromEscapia(List<string> ownsUnitNativePMSIDs)
        {
            await Escapia.EscapiaToken.SetEscapiaToken();

            using (var httpClient = new HttpClient())
            {
                var jsonUnit = JsonConvert.SerializeObject(ownsUnitNativePMSIDs);
                var data = new StringContent(jsonUnit, Encoding.UTF8, "application/json");

                foreach(var h in EscapiaApi.GetHttpClientDefaults().DefaultRequestHeaders)
                {
                    httpClient.DefaultRequestHeaders.Add(h.Key, h.Value);
                }
                /*
                httpClient.DefaultRequestHeaders.Add("x-homeaway-hasp-api-pmcid",
                    GlobalSettings.Escapia.PMCID.ToString());
                httpClient.DefaultRequestHeaders.Add("x-homeaway-hasp-api-version", "10");
                httpClient.DefaultRequestHeaders.Add("x-homeaway-hasp-api-endsystem", "EscapiaVRS");
                httpClient.DefaultRequestHeaders.Add("Authorization",
                    "Bearer " + GlobalSettings.Escapia.HomeAwayCredentials.Token);
                */
                
                string url = "https://hsapi.escapia.com/dragomanadapter/hsapi/GetUnitsById";

                // var response = await httpClient.GetAsync(url).ConfigureAwait(false);
                // var jsonString = await response.Content.ReadAsStringAsync();

                var response = await httpClient.PostAsync(url, data).ConfigureAwait(false);
                var jsonString = await response.Content.ReadAsStringAsync();


                // Escapia API sends $id which is an internal escapia and is not needed
                // to parse $id we use MetadataPropertyHandling and applied [JsonProperty("$id")] and name to metaid in class
                // ---------------------------------------------
                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.MetadataPropertyHandling = MetadataPropertyHandling.Ignore;
                settings.NullValueHandling = NullValueHandling.Ignore;
                settings.MissingMemberHandling = MissingMemberHandling.Ignore;

                var rootObject = JsonConvert.DeserializeObject<List<Models.ExternalApplication.Escapia.APIReturn.Unit>>(jsonString, settings);

                return rootObject;

            }
        }

        public void ParseJson()
        {
            string json = string.Empty;
            using (StreamReader r = new StreamReader(@"c:\temp\Units.json"))
            {
                json = r.ReadToEnd();
                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.MetadataPropertyHandling = MetadataPropertyHandling.Ignore;
                settings.NullValueHandling = NullValueHandling.Ignore;
                settings.MissingMemberHandling = MissingMemberHandling.Ignore;
                var rootObject = JsonConvert.DeserializeObject<List<Models.ExternalApplication.Escapia.APIReturn.Unit>>(json, settings);
            }
        }

    }
}