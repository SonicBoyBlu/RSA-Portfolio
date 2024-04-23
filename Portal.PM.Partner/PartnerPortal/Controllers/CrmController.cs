using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Acme.Controllers
{
    [WebMarkupMin.AspNet4.Mvc.CompressContent]
    [WebMarkupMin.AspNet4.Mvc.MinifyHtml]
    [RoutePrefix("CRM")]
    public class CrmController : Controller
    {
        // GET: Crm
        [Route("{view?}")]
        public ActionResult Index(string view = null)
        {
            view = view == null ? string.Empty : view.ToLower();
            var model = new ViewModels.CRM.DefaultView()
            {
                Tab = "#tab-Dashboard",
                //Title = "CRM - Home"
                Title = "CRM"
            };
            switch (view)
            {
                case "properties":
                    model.Tab = "Properties";
                    break;
                case "calls":
                    model.Tab = "Calls";
                    break;
                case "campaings":
                    model.Tab = "Campaings";
                    break;
                case "contacts":
                    model.Tab = "Contacts";
                    break;
                case "events":
                    model.Tab = "Events";
                    break;
                case "leads":
                    model.Tab = "Leads";
                    break;
                case "potentials":
                    model.Tab = "Potentials";
                    break;
                case "tasks":
                    model.Tab = "Tasks";
                    break;
                default:
                    model.Tab = "Dashboard";
                    break;
            }
            model = new ViewModels.CRM.DefaultView()
            {
                Tab = "#tab-" + model.Tab,
                Title = "CRM", // "CRM - " + model.Tab,
                Lists = Methods.CRM.GetListTypeAndStatus()
            };

            return View("CrmHome", model);
        }

        #region Dashboard
        [Route("Fetch/Dashboard")]
        public async Task<ActionResult> FetchDashboard(Models.CRM.Search query)
        {
            
            var srv = new Services.CrmService();
            var model = await Task.FromResult(srv.GetDashboard(query.DateStart, query.DateEnd));

            /*
            var prop = await Task.FromResult(Methods.CRM.GetProperties(query));
            var model = new ViewModels.CRM.PropertiesViewViewModel()
            {
                Query = query,
                //Properties = prop
            };
            */
            return PartialView("views/_FetchDashboard", model);
        }
        #endregion Dashboard


        #region Properties
        [Route("Fetch/Properties")]
        public ActionResult FetchProperties(Models.CRM.Search query)
        {
            var model = new ViewModels.CRM.PropertiesViewViewModel()
            {
                Query = query,
                Lists = Methods.CRM.GetListTypeAndStatus()
                //Properties = Methods.CRM.GetProperties(query)
            };
            return PartialView("views/_FetchProperties", model);
        }
        [Route("Property/{ID}")]
        public async Task<ActionResult> FetchProperty(string ID)
        {
            var model = await Task.FromResult(Methods.CRM.GetProperty(PropertyID: ID));
            return PartialView("partials/_propertyDetail", model);
        }

        [Route("Data/JSON/Properties")]
        public async Task<JsonNetResult> JsonDataProperties(Models.CRM.Search q)
        {
            var data = await Task.FromResult(Methods.CRM.GetProperties(q));
            //var lists = await Task.FromResult(Methods.CRM.GetListTypeAndStatus());
            /*
            foreach (var d in data)
            {
                d.Owner1 = string.IsNullOrEmpty(d.Owner1) ? string.Empty : "<i class='far fa-star text-warning'></i> " + d.Owner1;
                d.Owner2 = "<i class='fa fa-fw'></i> " + d.Owner2;
                d.AccountName = d.PropertyName == "-" ? d.AccountName : d.PropertyName;
                d.PropertyName = d.AccountName;
                //d.Phone = "<a href='tel:" + d.Phone + "'>" + d.Phone + "</a>";
                //d.Email = "<a href='mailto:" + d.Email + "'>" + d.Email + "</a>";
                d.Website = "<a href='" + d.Website + "' target='_blank'>" + d.Website + "</a>";
            }

            if (q.ShowInactive)
                data = data.OrderBy(x => (int)x.RentalStatusID).ThenByDescending(x => x.DateLastActive).ToList();
            */
            return new JsonNetResult(new
            {
                //lists,
                rows = data, // Newtonsoft.Json.JsonConvert.SerializeObject(data),
                total = data.Count,
                totalNotFiltered = data.Count
            });
        }
        #endregion Properties


        #region Contacts
        [Route("Fetch/{view}")]
        public async Task<ActionResult> FetchCrmView(string view, Models.CRM.Search query)
        {
            var typeID = DataTypes.CRM.ContactType.All;
            switch (view.ToLower())
            {
                case "owners": typeID = DataTypes.CRM.ContactType.Owner; break;
                case "leads": typeID = DataTypes.CRM.ContactType.Lead; break;
                case "potentials": typeID = DataTypes.CRM.ContactType.Potential; break;
                case "vendors": typeID = DataTypes.CRM.ContactType.Vendor; break;
                default: typeID = DataTypes.CRM.ContactType.All; break;
            }
            query.ContactTypeID = (int)typeID;
            
            var model = new ViewModels.CRM.ContactsView()
            {
                Query = query,
                View = view,
                Lists = await Task.FromResult(Methods.CRM.GetListTypeAndStatus())
                //Contacts = Methods.CRM.GetContacts(query)
            };
            return PartialView("views/_FetchContacts", model);
        }

        // JSON data for our grid
        [Route("ContactList/{Contacts}")]
        public async Task<JsonNetResult> JsonDataContactsList(Models.CRM.Search q)
        {
            var data = await Task.FromResult(Methods.CRM.GetContacts(q));
            return new JsonNetResult(new
            {
                rows = data,
                total = data.Count,
                totalNotFiltered = data.Count
            });
        }

        [Route("Fetch/Contact/{ContactID}")]
        public async Task<ActionResult> FetchContact(int ContactID)
        {
            var contact = await Task.FromResult(Methods.CRM.GetContact(ContactID));
            var lists = await Task.FromResult( Methods.CRM.GetListTypeAndStatus());

            if (contact.LeadDetail.Count == 0) { contact.LeadDetail.Add(new Models.CRM.Contacts.Details.LeadDetail() { PersonID = ContactID }); }
            if (contact.PotentialDetail.Count == 0) { contact.PotentialDetail.Add(new Models.CRM.Contacts.Details.PotentialDetail() { PersonID = ContactID }); }
            if (contact.VendorDetail.Count == 0) { contact.VendorDetail.Add(new Models.CRM.Contacts.Details.VendorDetail() { PersonID = ContactID }); }
            if (contact.Properties.Count() == 0) { contact.Properties.Add(new Acme.Models.CRM.Properties.Property() { PersonID = ContactID }); }

            var model = new ViewModels.CRM.ContactDetailViewModel()
            {
                ContactDetail = contact,
                Lists =lists
            };

            // Add empty items for form entry (New items)
            model.ContactDetail.EmailAddresses.Add(new Models.CRM.Contacts.Details.EmailAddress());
            model.ContactDetail.PhoneNumbers.Add(new Models.CRM.Contacts.Details.PhoneNumber() { PhoneNumberType = "New" });

            model.HeaderDetails = new ViewModels.CRM.ContactHeaderVewiewModel()
            {
                ContactType = contact.ContactType.ToString(),
                ContactStatus = contact.PipelineStage
            };
            try
            {
                model.HeaderDetails.Phone = string.Format("<a href='tel:{0}'>{0}</a>", contact.PhoneNumbers.Where(p => p.IsPrimary == true).FirstOrDefault().Number);
            }
            catch { model.HeaderDetails.Phone = "N/A"; }
            try
            {
                model.HeaderDetails.Email = string.Format("<a href='mailto:{0}'>{0}</a>", contact.EmailAddresses.Where(p => p.IsPrimary == true).FirstOrDefault().Address);
            }
            catch { model.HeaderDetails.Email = "N/A"; }

            //return new JsonNetResult(new { model });
            return PartialView("partials/_fetchContactDetail", model);
        }

        [HttpPost]
        [Route("Save/Contact")]
        public JsonNetResult SaveContact(string type, string form)
        {

            Models.System.GenericJsonResponse res = new Models.System.GenericJsonResponse();
            dynamic data = null;
            switch (type)
            {
                case "contact-new":
                    data = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.CRM._formParts.NewContact>(form);
                    break;
                case "contact-name":
                    data = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.CRM._formParts.ContactName>(form);
                    break;
                case "contact-phone":
                    data = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.CRM._formParts.PhoneNumber>(form);
                    break;
                case "contact-email":
                    data = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.CRM._formParts.EmailAddress>(form);
                    break;
                case "contact-address":
                    data = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.CRM._formParts.ContactAddress>(form);
                    break;
                case "contact-details":
                    data = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.CRM._formParts.ContactDetails>(form);
                    break;
                case "contact-misc":
                    data = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.CRM._formParts.ContactMisc>(form);
                    break;


                case "vendor-details":
                    data = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.CRM._formParts.VendorDetail>(form);
                    break;


                case "lead-details":
                    data = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.CRM._formParts.LeadDetails>(form);
                    break;
                case "lead-goals":
                    data = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.CRM._formParts.LeadGoals>(form);
                    break;
                case "lead-sites":
                    data = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.CRM._formParts.LeadSites>(form);
                    break;


                case "potential-details":
                    data = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.CRM._formParts.PotentialDetail>(form);
                    break;
                case "potential-weekly":
                    data = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.CRM._formParts.PotentialWeekly>(form);
                    break;
                case "potential-revenue":
                    data = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.CRM._formParts.PotentialRevenue>(form);
                    break;
                case "potential-exterior":
                    data = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.CRM._formParts.PotentialExterior>(form);
                    break;
                case "potential-interior":
                    data = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.CRM._formParts.PotentialInterior>(form);
                    break;


                case "property-details":
                    data = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.CRM._formParts.PropertyDetail>(form);
                    break;
                case "property-address":
                    data = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.CRM._formParts.PropertyAddress>(form);
                    break;
                case "property-owners":
                    data = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.CRM._formParts.PropertyOwners>(form);
                    break;
                case "property-goals":
                    data = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.CRM._formParts.PropertyGoals>(form);
                    break;
                case "property-notes":
                    data = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.CRM._formParts.PropertyNotes>(form);
                    break;
                case "property-special-instructions":
                    data = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.CRM._formParts.PropertySpecialInstructions>(form);
                    break;
                case "property-logistics":
                    data = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.CRM._formParts.PropertyLogistics>(form);
                    break;
                case "property-automation":
                    data = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.CRM._formParts.PropertyAutomation>(form);
                    break;
                case "property-alarms":
                    data = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.CRM._formParts.PropertyAlarms>(form);
                    break;
                case "property-codes":
                    data = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.CRM._formParts.PropertyCodes>(form);
                    break;
                case "property-wifi":
                    data = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.CRM._formParts.PropertyWiFi>(form);
                    break;
                case "property-features":
                    data = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.CRM._formParts.PropertyFeatures>(form);
                    break;
                case "property-service-contacts":
                    data = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.CRM._formParts.PropertyServiceContacts>(form);
                    break;
                case "property-important-dates":
                    data = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.CRM._formParts.PropertyImportantDates>(form);
                    break;
                case "property-city-legal":
                    data = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.CRM._formParts.PropertyCityLegal>(form);
                    break;
                case "property-inspections":
                    data = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.CRM._formParts.PropertyInspections>(form);
                    break;
                case "property-social-media":
                    data = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.CRM._formParts.PropertySocialMedia>(form);
                    break;
                case "property-meta":
                    data = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.CRM._formParts.PropertyMeta>(form);
                    break;
            }

            if (data != null)
            {
                var srv = new Services.CrmService();
                res = srv.Save(data);

                return new JsonNetResult(res);
                /*    
                new
                {
                    res.Title,
                    Body = res.Message,
                    res.IsSuccess
                });
                */
            } else
                return new JsonNetResult(new
                {
                    Title = "Unable to Save",
                    Body = "No suitable object was found to save this data.",
                    IsSuccess = false
                });
        }
        [Route("Delete/{Type}")]
        public bool DeleteContact(int ID, string Type)
        {
            var srv = new Services.CrmService();
            switch (Type)
            {
                case "contact-phone": return srv.Delete(Type, ID);
                case "contact-email": return srv.Delete(Type, ID);
                default: return false;
            }
            
        }
        #endregion Contacts

        /*
        #region Leads
        [Route("CRM/Fetch/Leads")]
        public ActionResult FetchLeads(Models.CRM.Search query)
        {
            var model = new ViewModels.CRM.LeadsView()
            {
                Query = query,
                //Leads = Methods.CRM.GetLeads(query)
            };
            return PartialView("views/_FetchLeads", model);
        }

        [Route("CRM/Data/JSON/Leads")]
        public JsonNetResult JsonDataLeads(Models.CRM.Search q)
        {
            var data = Methods.CRM.GetContacts(q);
            /*
            var data = Methods.CRM.GetLeads(q);
            foreach (var d in data)
            {
                d.FirstName = string.IsNullOrEmpty(d.FullName) ? string.Empty : "<i class='far fa-star text-warning'></i> " + d.FirstName;
                d.PartnerFirstName = "<i class='fa fa-fw'></i> " + d.PartnerFirstName;
                //d.Phone = "<a href='tel:" + d.Phone + "'>" + d.Phone + "</a>";
                //d.Email = "<a href='mailto:" + d.Email + "'>" + d.Email + "</a>";
                d.Website = "<a href='" + d.Website + "' target='_blank'>" + d.Website + "</a>";
            }
            * /
            return new JsonNetResult(new
            {
                rows = data, // Newtonsoft.Json.JsonConvert.SerializeObject(data),
                total = data.Count,
                totalNotFiltered = data.Count
            });
        }
        #endregion Leads


        #region Potentials
        [Route("CRM/Fetch/Potentials")]
        public ActionResult FetchPotentials(Models.CRM.Search query)
        {
            var model = new ViewModels.CRM.PotentialsView()
            {
                Query = query,
                //Potentials = Methods.CRM.GetPotentials(query)
            };
            return PartialView("views/_FetchPotentials", model);
        }

        [Route("CRM/Data/JSON/Potentials")]
        public JsonNetResult JsonDataPotentials(Models.CRM.Search q)
        {
            var data = Methods.CRM.GetContacts(q);
            /*
            var data = Methods.CRM.GetPotentials(q);
            foreach (var d in data)
            {
                d.PotentialName = string.IsNullOrEmpty(d.PotentialName) ? string.Empty : "<i class='far fa-star text-warning'></i> " + d.PotentialName;
                d.PartnerFirstName = "<i class='fa fa-fw'></i> " + d.PartnerFirstName;
                //d.Phone = "<a href='tel:" + d.Phone + "'>" + d.Phone + "</a>";
                //d.Email = "<a href='mailto:" + d.Email + "'>" + d.Email + "</a>";
                //d.Website = "<a href='" + d.Website + "' target='_blank'>" + d.Website + "</a>";
            }
            * /
            return new JsonNetResult(new
            {
                rows = data, // Newtonsoft.Json.JsonConvert.SerializeObject(data),
                total = data.Count,
                totalNotFiltered = data.Count
            });
        }

        #endregion Potentials
        */

        #region Calls
        [Route("CRM/Fetch/Calls")]
        public ActionResult FetchCalls(Models.CRM.Search query)
        {
            var model = new ViewModels.CRM.CallsView()
            {
                Query = query,
                Calls = Methods.CRM.GetCalls(query)
            };
            return PartialView("views/_FetchCalls", model);
        }
        #endregion Calls


        #region Vendors
        /*
        [Route("CRM/Fetch/Vendors")]
        public ActionResult FetchVendors(Models.CRM.Search query)
        {
            var model = new ViewModels.CRM.VendorsView()
            {
                Query = query,
                //Vendors = Methods.CRM.GetVendors(query)
            };
            if (!string.IsNullOrEmpty(query.ItemID))
            {
                var vendors = Methods.CRM.GetVendors(query);
                var vendor = vendors.Count > 0 ? vendors.FirstOrDefault() : new Models.CRM.Vendors.Vendor();
                return PartialView("partials/_vendorDetail", vendor);
            }
            else
                return PartialView("views/_FetchVendors", model);
        }

        [Route("CRM/Data/JSON/Vendors")]
        public JsonNetResult JsonDataVendors(Models.CRM.Search q)
        {
            var data = Methods.CRM.GetContacts(q);
            /*
            var data = Methods.CRM.GetVendors(q);
            foreach(var d in data)
            {
                d.Phone = "<a href='tel:" + d.Phone + "'>" + d.Phone + "</a>";
                d.Email = "<a href='mailto:" + d.Email + "'>" + d.Email + "</a>";
                d.Website = "<a href='" + d.Website + "' target='_blank'>" + d.Website + "</a>";
            }
            * /
            return new JsonNetResult(new
            {
                rows = data, // Newtonsoft.Json.JsonConvert.SerializeObject(data),
                total = data.Count,
                totalNotFiltered = data.Count
            });
        }
        */
        [Route("CRM/Save/Vendor")]
        public JsonNetResult SaveVendor(Models.CRM.Vendors.Vendor f)
        {
            var json = new JsonNetResult(
                new
                {
                    success = true,
                    message = "Vendor has been saved."
                }
                );
            using (SqlConnection conn = new SqlConnection(GlobalSettings.DateStores.CRM))
            {
                using (SqlCommand cmd = new SqlCommand("spVendorsSave", conn) { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.AddWithValue("@vendorid", string.IsNullOrEmpty(f.VendorID) ? DBNull.Value : (object)f.VendorID);
                    cmd.Parameters.AddWithValue("@VendorName", string.IsNullOrEmpty(f.VendorName) ? DBNull.Value : (object)f.VendorName);
                    cmd.Parameters.AddWithValue("@Phone", string.IsNullOrEmpty(f.Phone) ? DBNull.Value : (object)f.Phone);
                    cmd.Parameters.AddWithValue("@PhoneSecondary", string.IsNullOrEmpty(f.PhoneSecondary) ? DBNull.Value : (object)f.PhoneSecondary);
                    cmd.Parameters.AddWithValue("@Email", string.IsNullOrEmpty(f.Email) ? DBNull.Value : (object)f.Email);
                    cmd.Parameters.AddWithValue("@Website", string.IsNullOrEmpty(f.Website) ? DBNull.Value : (object)f.Website);
                    cmd.Parameters.AddWithValue("@Street", string.IsNullOrEmpty(f.Street) ? DBNull.Value : (object)f.Street);
                    cmd.Parameters.AddWithValue("@City", string.IsNullOrEmpty(f.City) ? DBNull.Value : (object)f.City);
                    cmd.Parameters.AddWithValue("@State", string.IsNullOrEmpty(f.State) ? DBNull.Value : (object)f.State);
                    cmd.Parameters.AddWithValue("@Zip", string.IsNullOrEmpty(f.Zip) ? DBNull.Value : (object)f.Zip);
                    cmd.Parameters.AddWithValue("@Description", string.IsNullOrEmpty(f.Description) ? DBNull.Value : (object)f.Description);
                    cmd.Parameters.AddWithValue("@ModifiedBy", Identity.Current.FullName);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();

                    }
                    catch(Exception ex)
                    {
                        json = new JsonNetResult(new {
                            success = false,
                            message = ex.Message
                        });
                    }
                }
            }
            return json;
        }
        #endregion Vendors


        #region Unused
        [Route("CRM/Fetch/Feeds")]
        public ActionResult FetchFeeds(Models.CRM.Search query)
        {
            var model = new ViewModels.CRM.PropertiesViewViewModel()
            {
                Query = query,
                //Properties = Methods.CRM.GetProperties(query)
            };
            return PartialView("views/_FetchFeeds", model);
        }
        [Route("CRM/Fetch/Campaigns")]
        public ActionResult FetchCampaigns(Models.CRM.Search query)
        {
            var model = new ViewModels.CRM.CampaignsView()
            {
                Query = query,
                Campaigns = Methods.CRM.GetCampaigns(query)
            };
            return PartialView("views/_FetchCampaigns", model);
        }
        [Route("CRM/Fetch/Events")]
        public ActionResult FetchEvents(Models.CRM.Search query)
        {
            var model = new ViewModels.CRM.EventsView()
            {
                Query = query,
                Events = Methods.CRM.GetEvents(query)
            };
            return PartialView("views/_FetchEvents", model);
        }
        [Route("CRM/Fetch/Tasks")]
        public ActionResult FetchTasks(Models.CRM.Search query)
        {
            var model = new ViewModels.CRM.TasksView()
            {
                Query = query,
                Tasks = Methods.CRM.GetTasks(query)
            };
            return PartialView("views/_FetchTasks", model);
        }
        #endregion Unused
    }
}