using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using Acme.Data.Repositories;
using Acme.Models;
using Acme.Models.ExternalApplication;
using Acme.ViewModels;
using Newtonsoft.Json;
using ReservationEscapiaJson = Acme.Data.Repositories.ReservationEscapiaJson;

namespace Acme.Services.Vendors.Escapia 
{
    public class SearchReservationSummaries 
    {
        public SearchReservationSummaries()
        {

        }

        public async Task<ViewModels.Common.MessageResult> getApi()
        {
            // Update all the ReservationStatus after API refresh
            await PostSearchReservationSummaries();

            return new ViewModels.Common.MessageResult{message = "completed", success = true };
        }
        public async Task<List<ViewModels.ReservationListViewModel>> GetReservations(
            Models.ExternalApplication.Escapia.Post.Data postData)
        {
            List<ReservationListViewModel> model = PostSearchReservationSummaries(postData);
            return model;
        }
        private List<ReservationListViewModel> PostSearchReservationSummaries(
            Models.ExternalApplication.Escapia.Post.Data postData)
        {
            var repoReservations = new Data.Repositories.ReservationRepository();
            List<Reservation> reservations = repoReservations.GetReservations(postData);
            List<ReservationListViewModel> model = Services.Vendors.Escapia.EscapiaMap.InitializeViewModelFromDB(reservations);
            return model;
        }
        private async Task<RawJson> PostSearchReservationSummaries()
        {
            var repoJson = new Data.Repositories.ReservationEscapiaJson();
            DateTime durationBegining = DateTime.Now;
            DateTime durationStart = DateTime.Now;
            DateTime durationEnd = DateTime.Now;
            try
            {
                int updatedCreatedAt = new ReservationService().UpdateReservationCreatedAt();
                TimeSpan interval = durationEnd - durationStart;
                string durationTotal = ("Minutes: " + interval.TotalMinutes + " Seconds: " + interval.Seconds).ToStringOrDefault();
                repoJson.Insert("Update Reservation Type", "[]", durationTotal, updatedCreatedAt);
            //return new RawJson();
            
            

            durationEnd = DateTime.Now;
            interval = durationEnd - durationStart;
            durationTotal = ("Minutes: " + interval.TotalMinutes + " Seconds: " + interval.Seconds).ToStringOrDefault();
            repoJson.Insert("Started", "[]", durationTotal,0);
            // ----------------------------------------

            
            durationStart = DateTime.Now;

            // Fill DB with Escapia MarketingCategories
            var cat = new Services.Vendors.Escapia.GetMarketingCategory();
            var marketingCategories = await cat.getAPI();
            cat.importIntoDB(marketingCategories);
            
            durationEnd = DateTime.Now;
            interval = durationEnd - durationStart;
            durationTotal = ("Minutes: " + interval.TotalMinutes + " Seconds: " + interval.Seconds).ToStringOrDefault();
            repoJson.Insert("Marketing Categories", "[]", durationTotal, 0);
            // ----------------------------------------
            

            durationStart = DateTime.Now;

            // Fill DB with Escapia ReservationTypes
            var resvtype = new Services.Vendors.Escapia.GetReservationTypes();
            var reservationTypes = await resvtype.getAPI();
            resvtype.importIntoDB(reservationTypes);

            durationEnd = DateTime.Now;
            interval = durationEnd - durationStart;
            durationTotal = ("Minutes: " + interval.TotalMinutes + " Seconds: " + interval.Seconds).ToStringOrDefault();
            repoJson.Insert("Reservation Types", "[]", durationTotal, reservationTypes.Count);
            // ----------------------------------------

            
            durationStart = DateTime.Now;

            var postData = EscapiaMap.InitializePostData();
            await Escapia.EscapiaToken.SetEscapiaToken();

            durationEnd = DateTime.Now;
            interval = durationEnd - durationStart;
            durationTotal = ("Minutes: " + interval.TotalMinutes + " Seconds: " + interval.Seconds).ToStringOrDefault();
            repoJson.Insert("SetEscapiaToken", "[]", durationTotal, 0);
                // ----------------------------------------



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

                    string url = "https://hsapi.escapia.com/dragomanadapter/hsapi/SearchReservationSummaries";
                    var response = await httpClient.PostAsync(url, data).ConfigureAwait(false);


                    durationStart = DateTime.Now;
                    var jsonString = await response.Content.ReadAsStringAsync();
                    durationEnd = DateTime.Now;
                    interval = durationEnd - durationStart;
                    durationTotal = ("Minutes: " + interval.TotalMinutes + " Seconds: " + interval.Seconds).ToStringOrDefault();
                    repoJson.Insert("Response.Content.ReadAsStringAsync", "[]", durationTotal, 0);
                    // ----------------------------------------



                    JsonSerializerSettings settings = new JsonSerializerSettings();
                    settings.MetadataPropertyHandling = MetadataPropertyHandling.Ignore;
                    settings.NullValueHandling = NullValueHandling.Ignore;
                    settings.MissingMemberHandling = MissingMemberHandling.Ignore;

                    RawJson rootObject = JsonConvert.DeserializeObject<RawJson>(jsonString, settings);

                    durationEnd = DateTime.Now;
                    interval = durationEnd - durationStart;
                    durationTotal = ("Minutes: " + interval.TotalMinutes + " Seconds: " + interval.Seconds).ToStringOrDefault();
                    repoJson.Insert("DeserializeObject", "[]", durationTotal, rootObject.results.Count);
                    // ----------------------------------------



                    durationStart = DateTime.Now;
                    List<ReservationListViewModel> model = EscapiaMap.InitializeViewModelFromSearchReservationSummaries(rootObject.results);
                    durationEnd = DateTime.Now;
                    interval = durationEnd - durationStart;
                    durationTotal = ("Minutes: " + interval.TotalMinutes + " Seconds: " + interval.Seconds).ToStringOrDefault();
                    repoJson.Insert("InitializeViewModelFromSearchReservationSummaries", "[]", durationTotal, model.Count);
                    // ----------------------------------------



                    durationStart = DateTime.Now;
                    string jsonStringmodel = JsonConvert.SerializeObject(model);
                    durationEnd = DateTime.Now;
                    interval = durationEnd - durationStart;
                    durationTotal = ("Minutes: " + interval.TotalMinutes + " Seconds: " + interval.Seconds).ToStringOrDefault();
                    repoJson.Insert("SerializeObject", "[]", durationTotal, 0);
                    // ----------------------------------------


                    durationStart = DateTime.Now;


                    var resv = new Services.ReservationService();
                    foreach (var reservationListViewModel in model)
                    {
                        var modelresv = resv.InitializePostData(reservationListViewModel);
                        resv.Update(modelresv);
                    }


                    durationEnd = DateTime.Now;
                    interval = durationEnd - durationStart;
                    durationTotal = ("Minutes: " + interval.TotalMinutes + " Seconds: " + interval.Seconds).ToStringOrDefault();
                    repoJson.Insert("Update entire Reservation table", "[]", durationTotal, model.Count);
                    // ----------------------------------------

                    durationStart = DateTime.Now;
                    int updatedReservationStatus = new ReservationService().UpdateReservationStatus();
                    durationEnd = DateTime.Now;
                    interval = durationEnd - durationStart;
                    durationTotal = ("Minutes: " + interval.TotalMinutes + " Seconds: " + interval.Seconds).ToStringOrDefault();
                    repoJson.Insert("Update Reservation Status", "[]", durationTotal, updatedReservationStatus);
                    // ----------------------------------------


                    durationStart = DateTime.Now;
                    int updatedReservationTypes = new ReservationService().UpdateReservationType();
                    durationEnd = DateTime.Now;
                    interval = durationEnd - durationStart;
                    durationTotal = ("Minutes: " + interval.TotalMinutes + " Seconds: " + interval.Seconds).ToStringOrDefault();
                    repoJson.Insert("Update Reservation Type", "[]", durationTotal, updatedReservationTypes);
                    // ----------------------------------------


                    //durationStart = DateTime.Now;
                    //int updatedCreatedAt = new ReservationService().UpdateReservationCreatedAt();
                    //durationEnd = DateTime.Now;
                    //interval = durationEnd - durationStart;
                    //durationTotal = ("Minutes: " + interval.TotalMinutes + " Seconds: " + interval.Seconds).ToStringOrDefault();
                    //repoJson.Insert("Update CreatedAt", "[]", durationTotal, updatedCreatedAt);
                    //// ----------------------------------------







                    durationEnd = DateTime.Now;
                    interval = durationEnd - durationBegining;
                    durationTotal = ("Minutes: " + interval.TotalMinutes + " Seconds: " + interval.Seconds).ToStringOrDefault();
                    repoJson.Insert("Completed", "[]", durationTotal, model.Count);
                    // ----------------------------------------

                    //await new EmailService().Send("ravi@dhali.com", "Eacapia API Refresh", "durationTotal: " + durationTotal);
                    await new EmailService().Send("ralexander.web@gmail.com", "Eacapia API Refresh", "durationTotal: " + durationTotal);
                //return null;
                return new RawJson(){
                    logs = repoJson 
                };
            }                
        }
            catch(Exception ex)
            {
                return null;
            }

        }
        private class RawJson
        {

            [JsonProperty("$id")]
            public string id { get; set; }
            public int totalCount { get; set; }
            public int pageSize { get; set; }
            public int pageNumber { get; set; }
            public int fromIndex { get; set; }
            public int toIndex { get; set; }
            public List<Acme.Models.ExternalApplication.Escapia.APIReturn.SearchReservationSummaries> results { get; set; }
            public object logs { get; set; }
        }
        /// used for testing parsing json to poco
        public void ParseJson()
        {
            string json = string.Empty;
            using (StreamReader r = new StreamReader(@"c:\temp\text.json"))
            {
                json = r.ReadToEnd();
                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.MetadataPropertyHandling = MetadataPropertyHandling.Ignore;
                settings.NullValueHandling = NullValueHandling.Ignore;
                settings.MissingMemberHandling = MissingMemberHandling.Ignore;
                RawJson rootObject = JsonConvert.DeserializeObject<RawJson>(json, settings);
            }
        }

    }
}