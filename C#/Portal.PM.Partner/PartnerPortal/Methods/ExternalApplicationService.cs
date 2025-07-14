using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Acme.Models.ExternalApplication;
using Newtonsoft.Json;

namespace Acme.Methods
{
    public class ExternalApplicationService
    {
        
        public Models.ExternalApplication.ExternalApplications ForUser(int userID) 
        {

            var result = new Models.ExternalApplication.ExternalApplications();

            var applicationList = InitializeExternalApplicationsForUser(userID);

            foreach (var app in applicationList)
            {

                switch (app.ExtAppID)
                {
                    case GlobalSettings.ExternalApplication.BambooHR:
                        int BambooHrID = app.ConfigurationMeta.ToInt32OrDefault();
                        result.BambooHR = GetBambooHRUser(BambooHrID);
                        break;
                    case GlobalSettings.ExternalApplication.SmarterTrack:
                        int SmarterTrackID = app.ConfigurationMeta.ToInt32OrDefault();
                        result.SmarterTrack = GetQuickSmarterTrackUser(SmarterTrackID);
                        break;
                    case GlobalSettings.ExternalApplication.QuickBooks:
                        int QuickBooksID = app.ConfigurationMeta.ToInt32OrDefault();
                        result.QuickBooks = GetQuickBooksUser(app.ConfigurationMeta);
                        break;
                    case GlobalSettings.ExternalApplication.Escapia:
                        int EscapiaID = app.ConfigurationMeta.ToInt32OrDefault();
                        result.Escapia = GetEscapiaUser(EscapiaID);
                        break;
                    case GlobalSettings.ExternalApplication.JitBit:
                        int JitBitID = app.ConfigurationMeta.ToInt32OrDefault();
                        result.JitBit = GetJitBitUser(JitBitID);
                        break;
                    default:
                        break;
                }
            }

            return result;
        }

        public List<Models.ExternalApplication.ExternalApplication> ByExternalApplicationId(int externalApplicationID)
        {
            var list = new List<Models.ExternalApplication.ExternalApplication>();

            using (SqlConnection conn = new SqlConnection(GlobalSettings.DateStores.AcmePortal))
            {
                SqlCommand cmd = new SqlCommand("spUsersExternalApplication", conn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@externalapplicationid", externalApplicationID);

                conn.Open();
                using (var row = cmd.ExecuteReader())
                {
                    while (row.Read())
                    {
                        try
                        {
                            var item = new ExternalApplication();
                            item.UserID = row["UserID"].ToInt32OrDefault(); 
                            item.ExtAppID = row["ExternalApplicationID"].ToInt32OrDefault();
                            item.ConfigurationMeta = row["ConfigurationMeta"].ToStringOrDefault();
                            list.Add(item);
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message);
                        }
                    }
                }

                conn.Close();
            }

            return list;

        }

        public List<Models.ExternalApplication.ExternalApplication> InitializeExternalApplicationsForUser(int userID)
        {

            var list = new List<Models.ExternalApplication.ExternalApplication>();

            using (SqlConnection conn = new SqlConnection(GlobalSettings.DateStores.AcmePortal))
            {
                SqlCommand cmd = new SqlCommand("spUserExternalApplication", conn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@userid", userID);

                conn.Open();
                using (var row = cmd.ExecuteReader())
                {
                    while (row.Read())
                    {
                        try
                        {
                            var item = new ExternalApplication();

                            item.ID = row["ID"].ToInt32OrDefault();
                            item.UserID = row["UserID"].ToInt32OrDefault();
                            item.ConfigurationMeta = row["ConfigurationMeta"].ToStringOrDefault();
                            item.IsActive = row["IsActive"].ToBooleanOrDefault();
                            item.ExtAppID = row["ExtAppID"].ToInt32OrDefault();
                            item.ExtAppGuid = row["ExtAppGuid"].ToGuidOrDefault();
                            item.ExtAppName = row["ExtAppName"].ToStringOrDefault();
                            item.ExtAppDescription = row["ExtAppDescription"].ToStringOrDefault();
                            item.ExtAppUrl = row["ExtAppUrl"].ToStringOrDefault();
                            item.RoleGuid = row["RoleGuid"].ToGuidOrDefault();
                            item.RoleName = row["RoleName"].ToStringOrDefault();
                            item.RoleDescription = row["RoleDescription"].ToStringOrDefault();

                            list.Add(item);

                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message);
                        }
                    }
                }

                conn.Close();
            }

            return list;

        }

        private Models.ExternalApplication.BambooHR GetBambooHRUser(int bambooHrID)
        {
            var api = new Services.Vendors.BambooHR.Api(); // Vendors.BambooHR.Api();

            if (api.IsNull())
                return null;

            var BambooUser = api.GetEmployee(bambooHrID);

            if (BambooUser.IsNull())
                return null;

            Models.ExternalApplication.BambooHR model = new Models.ExternalApplication.BambooHR();
            model.Department = BambooUser.Department.ToStringOrDefault();
            model.JobTitle = BambooUser.JobTitle.ToStringOrDefault();
            model.PhotoUrl = BambooUser.PhotoURL.ToStringOrDefault();
            

            return model;
        }

        private Escapia.Owner GetEscapiaUser(int escapiaID)
        {
            var srvUser = new Services.Vendors.Escapia.EscapiaOwner();
            var result = Task.Run(async () => await srvUser.FetchOwnerFromEscapia(escapiaID)).Result;
            
            List<Escapia.APIReturn.Unit> units = new List<Escapia.APIReturn.Unit>();
            if (result.ownsUnitNativePMSIDs.Any() || result.ownsUnitNativePMSIDs.IsNotNullOrEmpty())
            {
                var srvUnit = new Services.Vendors.Escapia.EscapiaUnit();
                units = Task.Run(async () =>await srvUnit.FetchUnitsByArrayFromEscapia(result.ownsUnitNativePMSIDs)).Result;
            }
            
            result.Unit = units;
            return result;

        }

        private Models.ExternalApplication.JitBit GetJitBitUser(int jitbitID)
        {
            return null;
            Models.ExternalApplication.JitBit model = new Models.ExternalApplication.JitBit();
            return model;
        }

        private Models.ExternalApplication.QuickBooks GetQuickBooksUser(string configMeta)
        {
            QuickBooks model = null;
            
            if (!string.IsNullOrEmpty(configMeta))
            {
                model = JsonConvert.DeserializeObject<Models.ExternalApplication.QuickBooks>(configMeta);
            }

            return model;
        }

        private Models.ExternalApplication.SmarterTrack GetQuickSmarterTrackUser(int smarterTrackID)
        {
            return null;
            Models.ExternalApplication.SmarterTrack model = new Models.ExternalApplication.SmarterTrack();
            return model;
        }


        #region Add Relationship to ExternalApplication
        
        public void UpdateUser(int externalApplicationId, int userID, int roleID, object model)
        {
            AddRelationshipUser(externalApplicationId, userID, JsonConvert.SerializeObject(model), roleID);
        }
        public void UpdateUser(int externalApplicationId, int userID, int roleID, string metavalue)
        {
            AddRelationshipUser(externalApplicationId, userID, metavalue, roleID);
        }

        private void AddRelationshipUser(int externalApplicationId, int userID, string configMetaData, int roleID)
        {
            int result = 0;

            string configurationmeta = configMetaData; 
            
            using (SqlConnection conn = new SqlConnection(GlobalSettings.DateStores.AcmePortal))
            {
                SqlCommand cmd = new SqlCommand("ExternalApplicationUpdate", conn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@userid", userID);
                cmd.Parameters.AddWithValue("@externalapplicationid", externalApplicationId);
                cmd.Parameters.AddWithValue("@configurationmeta", configurationmeta);
                cmd.Parameters.AddWithValue("@roleid", roleID);

                conn.Open();
                using (var row = cmd.ExecuteReader())
                {
                    while (row.Read())
                    {
                        try
                        {
                            result = row["id"].ToInt32OrDefault();
                            break;
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message);
                        }
                    }
                }

                conn.Close();
            }

            return;


        }
        
        #endregion


    }
}