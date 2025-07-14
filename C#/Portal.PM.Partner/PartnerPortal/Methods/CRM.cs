using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace Acme.Methods
{
    public class CRM
    {
        #region Properties
        public static List<Models.CRM.Properties.PropertyListItem> GetProperties(Models.CRM.Search q)
        {
            var list = new List<Models.CRM.Properties.PropertyListItem>();
            using(SqlConnection conn = new SqlConnection(GlobalSettings.DateStores.CRM))
            {
                using(SqlCommand cmd = new SqlCommand("spPropertiesListGet", conn) { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.AddWithValue("@showinactive", q.ShowInactive);
                    cmd.Parameters.AddWithValue("@status", q.StatusID == 0 ? DBNull.Value : (object)q.StatusID);
                    conn.Open();
                    using(var r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            try
                            {
                                string res = (string)r[1];
                                list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Models.CRM.Properties.PropertyListItem>>(res);
                            }
                            catch(Exception ex) { }
                        }
                    }
                }
            }
            return list;
        }
        public static Models.CRM.Zoho.Properties.Property GetProperty(string PropertyID = "", string UnitCode = "")
        {
            var unit = new Models.CRM.Zoho.Properties.Property();
            using (SqlConnection conn = new SqlConnection(GlobalSettings.DateStores.CRM))
            {
                using (SqlCommand cmd = new SqlCommand("spPropertyGet", conn) { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.AddWithValue("@propertyid", string.IsNullOrEmpty(PropertyID) ? DBNull.Value : (object)PropertyID);
                    cmd.Parameters.AddWithValue("@unitcode", string.IsNullOrEmpty(UnitCode) ? DBNull.Value : (object)UnitCode);
                    conn.Open();
                    using (var r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            try
                            {
                                unit = new Models.CRM.Zoho.Properties.Property()
                                {
                                    AccountID = (string)r["AccountID"],
                                    EscapiaID = (string)r["EscapiaID"],
                                    RentalStatus = (DataTypes.RentalStatus)r["RentalStatus"],
                                    AccountName = (string)r["Account Name"],
                                    PropertyName = (string)r["PropertyName"],
                                    UnitCode = (string)r["UnitCode"],
                                    PropertyType = (string)r["PropertyType"],

                                    SquareFootage = (int)r["SquareFootage"],
                                    NumBaths = (int)r["NumBaths"],
                                    NumHalfBaths = (int)r["NumHalfBaths"],
                                    NumBeds = (int)r["NumBeds"],

                                    Neighborhood = (string)r["Neighborhood"],
                                    Street = (string)r["Street"],
                                    City = (string)r["City"],
                                    State = (string)r["State"],
                                    Zip = (string)r["Zip"],
                                    HousePhone = (string)r["HousePhone"],
                                    CityPropertyID = (string)r["CityPropertyID"],

                                    MiscComplexes = (string)r["MiscComplexes"],
                                    SpecialInstructions1 = (string)r["SpecialInstructions1"],
                                    SpecialInstructions2 = (string)r["SpecialInstructions2"],

                                    Website = (string)r["Website"],
                                    SocialFacebook = (string)r["SocialFacebook"],
                                    SocialInstagram = (string)r["SocialInstagram"],
                                    MatterPortLink = (string)r["MatterPortLink"],

                                    ExpectationsGoals = (string)r["ExpectationsGoals"],
                                    Incentives = (string)r["Incentives"],
                                    PersonalUsage = (string)r["PersonalUsage"],

                                    Owner1 = (string)r["Owner1"],
                                    Owner2 = (string)r["Owner2"],
                                    Tag = (string)r["Tag"],

                                    IsPointCentralPaid = (bool)r["IsPointCentralPaid"],
                                    IsPointCentralLock = (bool)r["IsPointCentralLock"],
                                    IsGoZoneWifi = (bool)r["IsGoZoneWifi"],
                                    IsPointCentralThermostat = (bool)r["IsPointCentralThermostat"],
                                    IsNoiseAware = (bool)r["IsNoiseAware"],
                                    IsEarthquakeValue = (bool)r["IsEarthquakeValue"],
                                    GateCode = (string)r["GateCode"],
                                    WifiNameSSID = (string)r["WifiNameSSID"],
                                    WifiPassword = (string)r["WifiPassword"],
                                    LockBoxCode = (string)r["LockBoxCode"],
                                    AlarmCode = (string)r["AlarmCode"],
                                    AlarmPassword = (string)r["AlarmPassword"],
                                    AlarmCompanyContact = (string)r["AlarmCompanyContact"],
                                    GarageCode = (string)r["GarageCode"],
                                    KeylessDoorCode = (string)r["KeylessDoorCode"],
                                    MiscCodes = (string)r["MiscCodes"],

                                    RenewalServiceProgram = (string)r["RenewalServiceProgram"],
                                    PoolContact = (string)r["PoolContact"],
                                    PoolSchedule = (string)r["PoolSchedule"],
                                    LandscapingContact = (string)r["LandscapingContact"],
                                    LandscapingSchedule = (string)r["LandscapingSchedule"],
                                    PestContact = (string)r["PestContact"],
                                    Handyman = (string)r["Handyman"],
                                    TvInternet = (string)r["TvInternet"],

                                    ActiveCitations = (int)r["ActiveCitations"],
                                    CitationDates = (string)r["CitationDates"],

                                    InspectorName = (string)r["InspectorName"],
                                    IsDoNotInspect = (bool)r["IsDoNotInspect"],
                                    DateLastInspected = r["DateLastInspected"] == DBNull.Value ? DateTime.MinValue : (DateTime)r["DateLastInspected"],

                                    DatePcBatteryChanged = r["DatePcBatteryChanged"] == DBNull.Value ? DateTime.MinValue : (DateTime)r["DatePcBatteryChanged"],
                                    DateMinHouseholdItemCheck = r["DateMinHouseholdItemCheck"] == DBNull.Value ? DateTime.MinValue : (DateTime)r["DateMinHouseholdItemCheck"],
                                    DateApplicationSubmitted = r["DateApplicationSubmitted"] == DBNull.Value ? DateTime.MinValue : (DateTime)r["DateApplicationSubmitted"],
                                    DateOffBoard = r["DateOffBoard"] == DBNull.Value ? DateTime.MinValue : (DateTime)r["DateOffBoard"],
                                    DateOnBoard = r["DateOnBoard"] == DBNull.Value ? DateTime.MinValue : (DateTime)r["DateOnBoard"],
                                    DatePermitRenewal = r["DatePermitRenewal"] == DBNull.Value ? DateTime.MinValue : (DateTime)r["DatePermitRenewal"],


                                    IsPool = (bool)r["IsPool"],
                                    PoolType = (string)r["PoolType"],
                                    PoolDimensions = (string)r["PoolDimensions"],
                                    IsSpa = (bool)r["IsSpa"],
                                    SpaType = (string)r["SpaType"],
                                    SpaDimensions = (string)r["SpaDimensions"],
                                    IsGarage = (bool)r["IsGarage"],
                                    IsFireFeature = (bool)r["IsFireFeature"],
                                    IsWaterFeatures = (bool)r["IsWaterFeatures"],
                                    IsOutdoorKitchen = (bool)r["IsOutdoorKitchen"],
                                    IsOutdoorShower = (bool)r["IsOutdoorShower"],
                                    IsCabana = (bool)r["IsCabana"],
                                    IsCoveredPatio = (bool)r["IsCoveredPatio"],
                                    IsPingPongTable = (bool)r["IsPingPongTable"],
                                    IsPoolTable = (bool)r["IsPoolTable"],
                                    IsPuttingGreen = (bool)r["IsPuttingGreen"],
                                    IsSystemVideo = (bool)r["IsSystemVideo"],
                                    Casita = (string)r["Casita"],
                                    OtherAmenities = (string)r["OtherAmenities"],
                                    AppliancesName = (string)r["AppliancesName"],

                                    GeneralNotes = (string)r["GeneralNotes"],

                                    DateLastActive = r["DateLastActive"] == DBNull.Value ? DateTime.MinValue : (DateTime)r["DateLastActive"],
                                    DateCreated = r["DateCreated"] == DBNull.Value ? DateTime.MinValue : (DateTime)r["DateCreated"],
                                    DateModified = r["DateModified"] == DBNull.Value ? DateTime.MinValue : (DateTime)r["DateModified"],
                                    CreatedBy = (string)r["CreatedBy"],
                                    AccountOwner = (string)r["Account Owner"],
                                };
                            }
                            catch (Exception ex) { }
                        }
                    }
                }
            }
            return unit;
        }
        #endregion Properties


        #region Calls
        public static List<Models.CRM.Calls.Call> GetCalls(Models.CRM.Search q)
        {
            var list = new List<Models.CRM.Calls.Call>();
            using (SqlConnection conn = new SqlConnection(GlobalSettings.DateStores.CRM))
            {
                using (SqlCommand cmd = new SqlCommand("spCallsGetList", conn) { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.AddWithValue("@datestart", q.DateStart == DateTime.MinValue ? DBNull.Value : (object)q.DateStart);
                    cmd.Parameters.AddWithValue("@dateend", q.DateEnd == DateTime.MinValue ? DBNull.Value : (object)q.DateEnd);
                    cmd.Parameters.AddWithValue("@itemowner", string.IsNullOrEmpty(q.ItemOwner) ? DBNull.Value : (object)q.ItemOwner);
                    cmd.Parameters.AddWithValue("@query", string.IsNullOrEmpty(q.Query) ? DBNull.Value : (object)q.Query);
                    cmd.Parameters.AddWithValue("@status", q.StatusID == 0 ? DBNull.Value : (object)q.StatusID);
                    conn.Open();
                    using (var r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            try
                            {
                                list.Add(new Models.CRM.Calls.Call()
                                {
                                    ActivityID = (string)r["ActivityID"],
                                    Subject = (string)r["Subject"],
                                    Purpose = (string)r["CallPurpose"],
                                    CallStatus = (string)r["Call Status"],
                                    CallType = (string)r["CallType"],
                                    DateCallStart = (DateTime)r["DateCallStart"],
                                    CallDuration = (string)r["CallDuration"],
                                    CallDurationInSeconds = (int)r["CallDurationInSeconds"],
                                    Description = (string)r["Description"],
                                    CallResult = (string)r["CallResult"],
                                    Billable = (bool)r["Billable"],
                                    CallOwner = (string)r["Call Owner"],
                                    ContactName = (string)r["ContactName"]
                                });
                            }
                            catch (Exception ex) { }
                        }
                    }
                }
            }
            return list;
        }
        #endregion Calls


        #region Campaigns (?) is this needed?
        public static List<Models.CRM.Campaigns.Campaign> GetCampaigns(Models.CRM.Search q)
        {
            var list = new List<Models.CRM.Campaigns.Campaign>();
            return list;
        }
        #endregion Campaigns


        #region Contacts
        public static List<Models.CRM.Contacts.ContactListItem> GetContacts(Models.CRM.Search q)
        {
            var list = new List<Models.CRM.Contacts.ContactListItem>();
            using (SqlConnection conn = new SqlConnection(GlobalSettings.DateStores.CRM))
            {
                using (SqlCommand cmd = new SqlCommand("spContactsListGet", conn) { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.AddWithValue("@type", q.ContactTypeID == 0 ? DBNull.Value : (object)q.ContactTypeID);
                    conn.Open();
                    using (var r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            try
                            {
                                string res = (string)r[0];
                                list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Models.CRM.Contacts.ContactListItem>>(res);
                            }
                            catch (Exception ex) { }
                        }
                    }
                }

                using (SqlCommand cmd = new SqlCommand("spContactsGetList", conn) { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.AddWithValue("@datestart", q.DateStart == DateTime.MinValue ? DBNull.Value : (object)q.DateStart);
                    cmd.Parameters.AddWithValue("@dateend", q.DateEnd == DateTime.MinValue ? DBNull.Value : (object)q.DateEnd);
                    cmd.Parameters.AddWithValue("@itemowner", string.IsNullOrEmpty(q.ItemOwner) ? DBNull.Value : (object)q.ItemOwner);
                    cmd.Parameters.AddWithValue("@query", string.IsNullOrEmpty(q.Query) ? DBNull.Value : (object)q.Query);
                    cmd.Parameters.AddWithValue("@status", q.StatusID == 0 ? DBNull.Value : (object)q.StatusID);
                    conn.Open();
                    using (var r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            try
                            {
                                list.Add(new Models.CRM.Contacts.Contact()
                                {
                                    ContactID = (string)r["ContactID"],
                                    AccountName = (string)r["AccountName"],
                                    ContactType = (string)r["ContactType"],
                                    Phone = (string)r["Phone"],
                                    MobilePhone = (string)r["Mobile"],
                                    FirstName = (string)r["FirstName"],
                                    LastName = (string)r["LastName"],
                                    Email = (string)r["Email"],
                                    IsEmailOptOut = (bool)r["Email Opt Out"],
                                    IsPartnershipProperty = (bool)r["Partnership Property"],
                                    PartnerFirstName = (string)r["PartnerFirstName"],
                                    PartnerLastName = (string)r["PartnerLastName"],
                                    DateLastActive = r["Last Activity Time"] == DBNull.Value ? DateTime.MinValue : (DateTime)r["Last Activity Time"],
                                    ContactOwner = (string)r["Contact Owner"]
                                });
                            }
                            catch(Exception ex) { }
                        }
                    }
                }
                */
            }
            return list;
        }

        public static Models.CRM.Contacts.Contact GetContact(int ContactID)
        {
            var model = new Models.CRM.Contacts.Contact();
            using (SqlConnection conn = new SqlConnection(GlobalSettings.DateStores.CRM))
            {
                using (SqlCommand cmd = new SqlCommand("spContactGet", conn) { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.AddWithValue("@contactid", ContactID);
                    conn.Open();
                    using (var r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            try
                            {
                                model = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.CRM.Contacts.Contact>((string)r[0]);
                            }
                            catch (Exception ex) { }
                        }
                    }
                }
                return model;
            }
        }
        #endregion Contacts

        #region Lists
        public static Models.CRM.Lists.TypeAndStatus GetListTypeAndStatus(bool? IsActive = null)
        {
            var model = new Models.CRM.Lists.TypeAndStatus();
            using (SqlConnection conn = new SqlConnection(GlobalSettings.DateStores.CRM))
            {
                using (SqlCommand cmd = new SqlCommand("spGetListStatusAndTypes", conn) { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.AddWithValue("@isactive", IsActive == null ? DBNull.Value : (object)IsActive);
                    conn.Open();
                    using (var r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            try
                            {
                                model = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.CRM.Lists.TypeAndStatus>((string)r[0]);
                            }
                            catch (Exception ex) { }
                        }
                    }
                }
            }
            return model;
        }
        #endregion  Lists


        #region Events
        public static List<Models.CRM.Events.Event> GetEvents(Models.CRM.Search q)
        {
            var list = new List<Models.CRM.Events.Event>();
            using (SqlConnection conn = new SqlConnection(GlobalSettings.DateStores.CRM))
            {
                using (SqlCommand cmd = new SqlCommand("spEventsGetList", conn) { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.AddWithValue("@datestart", q.DateStart == DateTime.MinValue ? DBNull.Value : (object)q.DateStart);
                    cmd.Parameters.AddWithValue("@dateend", q.DateEnd == DateTime.MinValue ? DBNull.Value : (object)q.DateEnd);
                    cmd.Parameters.AddWithValue("@itemowner", string.IsNullOrEmpty(q.ItemOwner) ? DBNull.Value : (object)q.ItemOwner);
                    cmd.Parameters.AddWithValue("@query", string.IsNullOrEmpty(q.Query) ? DBNull.Value : (object)q.Query);
                    cmd.Parameters.AddWithValue("@status", q.StatusID == 0 ? DBNull.Value : (object)q.StatusID);
                    conn.Open();
                    using (var r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            try
                            {
                                list.Add(new Models.CRM.Events.Event()
                                {
                                    ActivityID = (string)r["ActivityID"],
                                    Subject = (string)r["Subject"],
                                    Description = (string)r["Description"],
                                    Venue = (string)r["Venue"],
                                    EventOwner = (string)r["Event Owner"],
                                    DateStart = (DateTime)r["Start DateTime"],
                                    DateEnd = (DateTime)r["End DateTime"]
                                });
                            }
                            catch(Exception ex) { }
                        }
                    }
                }
            }
            return list;
        }
        #endregion


        #region Leads
        public static List<Models.CRM.Leads.Lead> GetLeads(Models.CRM.Search q)
        {
            var list = new List<Models.CRM.Leads.Lead>();
            using (SqlConnection conn = new SqlConnection(GlobalSettings.DateStores.CRM))
            {
                using (SqlCommand cmd = new SqlCommand("spLeadsGetList", conn) { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.AddWithValue("@datestart", q.DateStart == DateTime.MinValue ? DBNull.Value : (object)q.DateStart);
                    cmd.Parameters.AddWithValue("@dateend", q.DateEnd == DateTime.MinValue ? DBNull.Value : (object)q.DateEnd);
                    cmd.Parameters.AddWithValue("@itemowner", string.IsNullOrEmpty(q.ItemOwner) ? DBNull.Value : (object)q.ItemOwner);
                    cmd.Parameters.AddWithValue("@query", string.IsNullOrEmpty(q.Query) ? DBNull.Value : (object)q.Query);
                    cmd.Parameters.AddWithValue("@status", q.StatusID == 0 ? DBNull.Value : (object)q.StatusID);
                    conn.Open();
                    using (var r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            try
                            {
                                list.Add(new Models.CRM.Leads.Lead()
                                {
                                    LeadID = (string)r["LeadID"],
                                    PropertyAddress = (string)r["PropertyAddress"],
                                    FirstName = (string)r["FirstName"],
                                    LastName = (string)r["LastName"],
                                    PartnerFirstName = (string)r["PartnerFirstName"],
                                    PartnerLastName = (string)r["PartnerLastName"],
                                    Phone = (string)r["Phone"],
                                    MobilePhone = (string)r["Mobile"],
                                    Email = (string)r["Email"],
                                    LeadSource = (string)r["LeadSource"],
                                    LeadStatus = (string)r["LeadStatus"],
                                    LeadType = (string)r["LeadType"],
                                    DateLastActive = r["Last Activity Time"] == DBNull.Value ? DateTime.MinValue : (DateTime)r["Last Activity Time"],
                                    LeadOwner = (string)r["Lead Owner"]
                                });
                            }
                            catch (Exception ex) { }
                        }
                    }
                }
            }
            return list;
        }
        public static Models.CRM.Leads.Lead GetLead(string LeadID)
        {
            var lead = new Models.CRM.Leads.Lead();
            using (SqlConnection conn = new SqlConnection(GlobalSettings.DateStores.CRM))
            {
                using (SqlCommand cmd = new SqlCommand("spLeadGet", conn) { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.AddWithValue("@LeadID", string.IsNullOrEmpty(LeadID) ? DBNull.Value : (object)LeadID);
                    conn.Open();
                    using (var r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            try
                            {
                                lead = new Models.CRM.Leads.Lead()
                                {
                                    LeadID = (string)r[""],

                                };
                            }
                            catch (Exception ex) { }
                        }
                    }
                }
            }
            return lead;
        }
        #endregion


        #region Potentials
        public static List<Models.CRM.Potentials.Potential> GetPotentials(Models.CRM.Search q)
        {
            var list = new List<Models.CRM.Potentials.Potential>();
            using (SqlConnection conn = new SqlConnection(GlobalSettings.DateStores.CRM))
            {
                using (SqlCommand cmd = new SqlCommand("spPotentialsGetList", conn) { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.AddWithValue("@datestart", q.DateStart == DateTime.MinValue ? DBNull.Value : (object)q.DateStart);
                    cmd.Parameters.AddWithValue("@dateend", q.DateEnd == DateTime.MinValue ? DBNull.Value : (object)q.DateEnd);
                    cmd.Parameters.AddWithValue("@itemowner", string.IsNullOrEmpty(q.ItemOwner) ? DBNull.Value : (object)q.ItemOwner);
                    cmd.Parameters.AddWithValue("@query", string.IsNullOrEmpty(q.Query) ? DBNull.Value : (object)q.Query);
                    cmd.Parameters.AddWithValue("@status", q.StatusID == 0 ? DBNull.Value : (object)q.StatusID);
                    conn.Open();
                    using (var r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            try
                            {
                                list.Add(new Models.CRM.Potentials.Potential()
                                {
                                    PotentialID = (string)r["PotentialID"],
                                    AccountName = (string)r["AccountName"],
                                    PotentialName = (string)r["PotentialName"],
                                    PartnerFirstName = (string)r["PartnerFirstName"],
                                    PartnerLastName = (string)r["PartnerLastName"],
                                    ExpectedRevenue = (double)r["ExpectedRevenue"],
                                    Stage = (string)r["Stage"],
                                    Type = (string)r["LeadType"],
                                    LeadSource = (string)r["LeadSource"],
                                    DateLastActive = r["Last Activity Time"] == DBNull.Value ? DateTime.MinValue : (DateTime)r["Last Activity Time"],
                                    PotentialOwner = (string)r["Potential Owner"],
                                });
                            }
                            catch(Exception ex) { }
                        }
                    }
                }
            }
            return list;
        }
        #endregion Potentials


        #region Tasks
        public static List<Models.CRM.Tasks.Task> GetTasks(Models.CRM.Search q)
        {
            var list = new List<Models.CRM.Tasks.Task>();
            using (SqlConnection conn = new SqlConnection(GlobalSettings.DateStores.CRM))
            {
                using (SqlCommand cmd = new SqlCommand("spTasksGetList", conn) { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.AddWithValue("@datestart", q.DateStart == DateTime.MinValue ? DBNull.Value : (object)q.DateStart);
                    cmd.Parameters.AddWithValue("@dateend", q.DateEnd == DateTime.MinValue ? DBNull.Value : (object)q.DateEnd);
                    cmd.Parameters.AddWithValue("@itemowner", string.IsNullOrEmpty(q.ItemOwner) ? DBNull.Value : (object)q.ItemOwner);
                    cmd.Parameters.AddWithValue("@query", string.IsNullOrEmpty(q.Query) ? DBNull.Value : (object)q.Query);
                    cmd.Parameters.AddWithValue("@status", q.StatusID == 0 ? DBNull.Value : (object)q.StatusID);
                    conn.Open();
                    using (var r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            try
                            {
                                list.Add(new Models.CRM.Tasks.Task()
                                {
                                    ActivityID = (string)r["ActivityID"],
                                    Subject = (string)r["Subject"],
                                    TaskOwner = (string)r["Task Owner"],
                                    DateDue = r["Due Date"] == DBNull.Value ? DateTime.MinValue : (DateTime)r["Due Date"],
                                    Status = (string)r["Status"],
                                    Priority = (string)r["Priority"],
                                    IsSendNotificationEmail = (bool)r["Send Notification Email"],
                                    Description = (string)r["Description"],
                                    DateCreated = (DateTime)r["Created Time"]
                                });
                            }
                            catch(Exception ex) { }
                        }
                    }
                }
            }
            return list;
        }
        #endregion Tasks


        #region Vendors
        public static List<Models.CRM.Vendors.Vendor> GetVendors(Models.CRM.Search q)
        {
            var list = new List<Models.CRM.Vendors.Vendor>();
            using (SqlConnection conn = new SqlConnection(GlobalSettings.DateStores.CRM))
            {
                using (SqlCommand cmd = new SqlCommand("spVendorsGetList", conn) { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.AddWithValue("@vendorid", string.IsNullOrEmpty(q.ItemID) ? DBNull.Value : (object)q.ItemID);
                    /*
                    cmd.Parameters.AddWithValue("@datestart", q.DateStart == DateTime.MinValue ? DBNull.Value : (object)q.DateStart);
                    cmd.Parameters.AddWithValue("@dateend", q.DateEnd == DateTime.MinValue ? DBNull.Value : (object)q.DateEnd);
                    cmd.Parameters.AddWithValue("@itemowner", string.IsNullOrEmpty(q.ItemOwner) ? DBNull.Value : (object)q.ItemOwner);
                    cmd.Parameters.AddWithValue("@query", string.IsNullOrEmpty(q.Query) ? DBNull.Value : (object)q.Query);
                    cmd.Parameters.AddWithValue("@status", string.IsNullOrEmpty(q.Status) ? DBNull.Value : (object)q.Status);
                    */
                    conn.Open();
                    using (var r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            try
                            {
                                list.Add(new Models.CRM.Vendors.Vendor()
                                {
                                    VendorID = (string)r["VendorID"],
                                    VendorType = (string)r["VendorType"],
                                    VendorName = (string)r["Vendor Name"],
                                    VendorOwner = (string)r["Vendor Owner"],
                                    Email = (string)r["Email"],
                                    Phone = (string)r["Phone"],
                                    PhoneSecondary = (string)r["PhoneSecondary"],
                                    Website = (string)r["Website"],
                                    Category = (string)r["Category"],
                                    Tag = (string)r["Tag"],
                                    State = (string)r["State"],
                                    City = (string)r["City"],
                                    Street = (string)r["Street"],
                                    Zip = (string)r["Zip"],
                                    Country = (string)r["Country"],
                                    CreatedBy = (string)r["Created By"],
                                    ModifiedBy = (string)r["Modified By"],
                                    Description = (string)r["Description"],
                                    DateModified = (DateTime)r["Modified Time"],
                                    DateCreated = (DateTime)r["Created Time"],
                                    SmOwnerID = (string)r["SmOwnerID"]
                                });
                            }
                            catch (Exception ex) { }
                        }
                    }
                }
            }
            return list;
        }
        #endregion
    }
}