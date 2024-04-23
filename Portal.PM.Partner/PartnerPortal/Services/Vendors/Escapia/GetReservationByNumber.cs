using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Acme.Models;
using Acme.ViewModels;
using Newtonsoft.Json;

namespace Acme.Services.Vendors.Escapia
{
    public class GetReservationByNumber
    {
        private string _reservationNumber;

        public GetReservationByNumber(string reservationNumber)
        {
            _reservationNumber = reservationNumber.ToStringOrDefault();
        }

        public async Task<ReservationListViewModel> Get(string reservationNumber)
        {

            var repo = new Data.Repositories.ReservationRepository();
            Reservation data = repo.GetReservation(reservationNumber);

            var initReservationType = new Services.ReservationTypeService();
            var reservationTypes = initReservationType.GetReservationTypes();

            if (data.IsNull())
            {
                // The resv was not found in our database. Maybe it was newly added to Escapia
                // Check Escapia for the reservationNumber
                Models.ExternalApplication.Escapia.APIReturn.ReservationDetail foundResvUsingApiRawJson = await GetReservationDetail(reservationNumber);

                if (foundResvUsingApiRawJson.IsNull())
                    return null;

                // We found a newly added resv in Escapia api
                // Add it to Resv table

                ReservationListViewModel model = EscapiaMap.InitializeViewModelFromSearchReservationDetail(foundResvUsingApiRawJson);

                var resv = new Services.ReservationService();
                var modelresv = resv.InitializePostData(model);
                resv.Update(modelresv);
                
                data = repo.GetReservation(model.ReservationNumber);

            }

            ReservationTypeViewModel reservationType = (from t in reservationTypes where t.Name.ToLower() == data.ReservationType.ToLower() select t).FirstOrDefault();

            var reservation = Acme.Services.Vendors.Escapia.EscapiaMap.InitializeViewModelFromDB(data, reservationType);

            // ReservationListViewModel result = EscapiaMap.InitializeViewModelFromReservationDetail(apiRawJson);

            Models.ExternalApplication.Escapia.APIReturn.ReservationDetail apiRawJson = await GetReservationDetail(reservation.ReservationNumber);
            
            Models.ExternalApplication.Escapia.APIReturn.Unit apiUnitRawJson = await GetUnitDetail(apiRawJson.unitNativePMSID);

            reservation.Unit = apiUnitRawJson;
            reservation.Notes = apiRawJson.notes;
            reservation.PalmSpringsContractNumberOptions = Services.Vendors.Escapia.EscapiaEnum.PalmSpringsContractNumberOptions();

            reservation = AdditionalPropertyValues.FillAdHockAdditionalPropertyValues(reservation);

            return reservation;
        }

        private async Task<Models.ExternalApplication.Escapia.APIReturn.Unit> GetUnitDetail(string unitNativePMSID)
        {
            if (unitNativePMSID.IsNullOrEmpty())
                return null;

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

                string url = "https://hsapi.escapia.com/dragomanadapter/hsapi/GetUnitById?id=" + unitNativePMSID;
                var response = await httpClient.GetAsync(url).ConfigureAwait(false); 

                var jsonString = await response.Content.ReadAsStringAsync();

                // Escapia API sends $id which is an internal escapia and is not needed
                // to parse $id we use MetadataPropertyHandling and applied [JsonProperty("$id")] and name to metaid in class
                // ---------------------------------------------
                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.MetadataPropertyHandling = MetadataPropertyHandling.Ignore;
                settings.NullValueHandling = NullValueHandling.Ignore;
                settings.MissingMemberHandling = MissingMemberHandling.Ignore;

                var rootObject = JsonConvert.DeserializeObject<Models.ExternalApplication.Escapia.APIReturn.Unit>(jsonString, settings);

                return rootObject;
            }
        }

        public async Task<Models.ExternalApplication.Escapia.APIReturn.ReservationDetail> GetReservationDetail(string reservationNumber)
        {
            if (reservationNumber.IsNullOrEmpty())
                return null;

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

                string url = "https://hsapi.escapia.com/dragomanadapter/hsapi/GetReservationByNumber?reservationNumber=" + reservationNumber;
                
                var response = await httpClient.GetAsync(url).ConfigureAwait(false);

                if (!response.IsSuccessStatusCode)
                {
                    var failure = new Models.ExternalApplication.Escapia.APIReturn.ReservationDetail();

                    failure.failureDiagnosis = new Models.ExternalApplication.Escapia.Diagnosis();
                    failure.failureDiagnosis.displayMessage = "Escapia API Failure";
                    failure.failureDiagnosis.internalMessage = response.ReasonPhrase;

                    return failure;
                }
                

                var jsonString = await response.Content.ReadAsStringAsync();
                

                if (jsonString.IsNullOrEmpty())
                    return null;

                // Escapia API sends $id which is an internal escapia and is not needed
                // to parse $id we use MetadataPropertyHandling and applied [JsonProperty("$id")] and name to metaid in class
                // ---------------------------------------------
                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.MetadataPropertyHandling = MetadataPropertyHandling.Ignore;
                settings.NullValueHandling = NullValueHandling.Ignore;
                settings.MissingMemberHandling = MissingMemberHandling.Ignore;

                var rootObject = JsonConvert.DeserializeObject<Models.ExternalApplication.Escapia.APIReturn.ReservationDetail>(jsonString, settings);

                rootObject.failureDiagnosis = null;

                return rootObject;
            }
        }
        
        public void ParseJson()
        {
            string json = string.Empty;
            using (StreamReader r = new StreamReader(@"c:\temp\GetReservationDetail.json"))
            {
                json = r.ReadToEnd();
                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.MetadataPropertyHandling = MetadataPropertyHandling.Ignore;
                settings.NullValueHandling = NullValueHandling.Ignore;
                settings.MissingMemberHandling = MissingMemberHandling.Ignore;
                var rootObject = JsonConvert.DeserializeObject<Models.ExternalApplication.Escapia.APIReturn.ReservationDetail>(json, settings);
            }
        }
    }
}