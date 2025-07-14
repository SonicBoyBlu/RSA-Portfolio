using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Acme.Data.Maps;
using Acme.Methods;
using Newtonsoft.Json;

namespace Acme.Services.Vendors.Escapia
{
    public class EscapiaOwner
    {
        public EscapiaOwner()
        {
            
        }
        public async Task<Models.ExternalApplication.Escapia.Owner> FetchOwnerFromEscapia(int userNativePMSID)
        {
            await Escapia.EscapiaToken.SetEscapiaToken();

            using (var httpClient = new HttpClient())
            {

                httpClient.DefaultRequestHeaders.Add("x-homeaway-hasp-api-pmcid",
                    GlobalSettings.Escapia.PMCID.ToString());
                httpClient.DefaultRequestHeaders.Add("x-homeaway-hasp-api-version", "10");
                httpClient.DefaultRequestHeaders.Add("x-homeaway-hasp-api-endsystem", "EscapiaVRS");
                httpClient.DefaultRequestHeaders.Add("Authorization",
                    "Bearer " + GlobalSettings.Escapia.HomeAwayCredentials.Token);
                
                string url = "https://hsapi.escapia.com/dragomanadapter/hsapi/GetOwnerById?ownerNativePMSID=" + userNativePMSID;

                var response = await httpClient.GetAsync(url).ConfigureAwait(false);
                var jsonString = await response.Content.ReadAsStringAsync();

                // Escapia API sends $id which is an internal escapia and is not needed
                // to parse $id we use MetadataPropertyHandling and applied [JsonProperty("$id")] and name to metaid in class
                // ---------------------------------------------
                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.MetadataPropertyHandling = MetadataPropertyHandling.Ignore;
                settings.NullValueHandling = NullValueHandling.Ignore;
                settings.MissingMemberHandling = MissingMemberHandling.Ignore;

                var rootObject = JsonConvert.DeserializeObject<Models.ExternalApplication.Escapia.Owner>(jsonString, settings);

                return rootObject;


            }
        }
        public async Task<Models.ExternalApplication.Escapia.APIReturn.SearchOwnersResult> GetOwnersFromEscapia()
        {
            await Escapia.EscapiaToken.SetEscapiaToken();

            var postData = new Models.ExternalApplication.Escapia.PostSearchUser();
            postData.pageSize = 100000;
            postData.pageNumber = 1;
            postData.Specification = new Models.ExternalApplication.Escapia.PostSearchUserSpecification();
            
            using (var httpClient = new HttpClient())
            {

                var postJson = Newtonsoft.Json.JsonConvert.SerializeObject(postData);
                var data = new StringContent(postJson, Encoding.UTF8, "application/json");

                httpClient.DefaultRequestHeaders.Add("x-homeaway-hasp-api-pmcid",
                    GlobalSettings.Escapia.PMCID.ToString());
                httpClient.DefaultRequestHeaders.Add("x-homeaway-hasp-api-version", "10");
                httpClient.DefaultRequestHeaders.Add("x-homeaway-hasp-api-endsystem", "EscapiaVRS");
                httpClient.DefaultRequestHeaders.Add("Authorization",
                    "Bearer " + GlobalSettings.Escapia.HomeAwayCredentials.Token);


                string url = "https://hsapi.escapia.com/dragomanadapter/hsapi/SearchOwners";

                var response = await httpClient.PostAsync(url, data).ConfigureAwait(false);
                var jsonString = await response.Content.ReadAsStringAsync();

                // Escapia API sends $id which is an internal escapia and is not needed
                // to parse $id we use MetadataPropertyHandling and applied [JsonProperty("$id")] and name to metaid in class
                // ---------------------------------------------
                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.MetadataPropertyHandling = MetadataPropertyHandling.Ignore;
                settings.NullValueHandling = NullValueHandling.Ignore;
                settings.MissingMemberHandling = MissingMemberHandling.Ignore;

                var rootObject = JsonConvert.DeserializeObject<Models.ExternalApplication.Escapia.APIReturn.SearchOwnersResult>(jsonString, settings);

                return rootObject;


            }
        }
        public async Task UpdateOwnersFromEscapia()
        {
            Models.ExternalApplication.Escapia.APIReturn.SearchOwnersResult data =
                await GetOwnersFromEscapia().ConfigureAwait(false);

            List<Models.ExternalApplication.Escapia.Owner> escapiaUsers = data.results;

            var srvUser = new UserService();

            foreach (var escapiaUser in escapiaUsers)
            {
                srvUser.AddOwnerUserEscapia(escapiaUser);
            }
        }
        public void ParseJson()
        {
            string json = string.Empty;
            using (StreamReader r = new StreamReader(@"c:\temp\GetOwner.json"))
            {
                json = r.ReadToEnd();
                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.MetadataPropertyHandling = MetadataPropertyHandling.Ignore;
                settings.NullValueHandling = NullValueHandling.Ignore;
                settings.MissingMemberHandling = MissingMemberHandling.Ignore;
                var rootObject = JsonConvert.DeserializeObject<Models.ExternalApplication.Escapia.APIReturn.SearchOwnersResult>(json, settings);
            }
        }
    }
}