using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc.Html;
using Newtonsoft.Json;

namespace Acme.Services.Vendors.Escapia
{
    public class GetMarketingCategory
    {
        
        public GetMarketingCategory()
        {

        }

        public async Task<List<Models.ExternalApplication.Escapia.APIReturn.MarketingCategory>> getAPI()
        {
            await Escapia.EscapiaToken.SetEscapiaToken();

            using (var httpClient = new HttpClient())
            {

                //var postJson = Newtonsoft.Json.JsonConvert.SerializeObject(postData);
                //var data = new StringContent(postJson, Encoding.UTF8, "application/json");

                httpClient.DefaultRequestHeaders.Add("x-homeaway-hasp-api-pmcid",
                    GlobalSettings.Escapia.PMCID.ToString());
                httpClient.DefaultRequestHeaders.Add("x-homeaway-hasp-api-version", "10");
                httpClient.DefaultRequestHeaders.Add("x-homeaway-hasp-api-endsystem", "EscapiaVRS");
                httpClient.DefaultRequestHeaders.Add("Authorization",
                    "Bearer " + GlobalSettings.Escapia.HomeAwayCredentials.Token);

                string url = "https://hsapi.escapia.com/dragomanadapter/hsapi/ListMarketingCategories";
                var response = await httpClient.GetAsync(url).ConfigureAwait(false); 

                var jsonString = await response.Content.ReadAsStringAsync();

                // Escapia API sends $id which is an internal escapia and is not needed
                // to parse $id we use MetadataPropertyHandling and applied [JsonProperty("$id")] and name to metaid in class
                // ---------------------------------------------
                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.MetadataPropertyHandling = MetadataPropertyHandling.Ignore;
                settings.NullValueHandling = NullValueHandling.Ignore;
                settings.MissingMemberHandling = MissingMemberHandling.Ignore;

                var rootObject = JsonConvert.DeserializeObject<List<Models.ExternalApplication.Escapia.APIReturn.MarketingCategory>>(jsonString, settings);

                return rootObject;
            }
        }

        /// <summary>
        /// Get data from API and store into DB.
        /// Should run once a month to keep DB sync'd with Escapia
        /// </summary>
        /// <param name="listCat"></param>
        internal void importIntoDB(List<Models.ExternalApplication.Escapia.APIReturn.MarketingCategory> apiMarketingCategories)
        {
            
            // Loop through parent Marketing Category

            foreach (var apiMarketingCategory in apiMarketingCategories)
            {
                var repoCat = new Data.Repositories.ReservationCategoryRepository();
                Models.ReservationCategory category = repoCat.GetByNativePMSID(apiMarketingCategory.nativePMSID);

                if (category.IsNull())
                {
                    var srvMarketingCategory = new Acme.Services.ReservationCategoryService();

                    // We dont have it in DB yet  ADD IT
                    var dbMarketingCategory = srvMarketingCategory.InitializeDBModel(apiMarketingCategory);

                    if (dbMarketingCategory.IsNotNull())
                    {
                        int marketingCategoryID = repoCat.Add(dbMarketingCategory).ToInt32OrDefault();

                        var repoSource = new Data.Repositories.ReservationSourceRepository();
                        var sources = repoSource.GetByCategoryID(marketingCategoryID);


                        foreach (var apiSource in apiMarketingCategory.marketingSources)
                        {
                            var srvMarketingSource = new Acme.Services.ReservationSourceService();

                            Models.ReservationSource source = (from s in sources where s.NativePMSID == apiSource.nativePMSID.ToInt32OrDefault() select s).FirstOrDefault();

                            //if (source.IsNotNull())
                            //{
                               Models.ReservationSource dbMarketingSource = srvMarketingSource.InitializeDBModel(marketingCategoryID, apiSource);

                                if (dbMarketingSource.IsNotNull())
                                {
                                    repoSource.Add(dbMarketingSource);
                                }
                            //}


                        }
                    }


                }

                
                

            }


        }
    }
}