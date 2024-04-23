using Dapper;
using Dapper.FastCrud;
using System;
using System.Data;

namespace Acme.Data.Repositories
{
    public class CrmRepository : BaseRepository
    {
        protected override string ConnectionString
        {
            get { return GlobalSettings.DateStores.CRM; }
        }

        public ViewModels.CRM.DashboardViewModel GetDashboard(DateTime DateStart, DateTime DateEnd)
        {
            return WithConnection<ViewModels.CRM.DashboardViewModel>(connection =>
            {
                var data = connection.QueryMultiple("spDashboardGet", new { DateStart, DateEnd }, commandType: CommandType.StoredProcedure);
                ViewModels.CRM.DashboardViewModel result = new ViewModels.CRM.DashboardViewModel()
                {
                    DateRanges = (Models.CRM.Dashboard.PipelineTotalsDates)data.ReadSingle<Models.CRM.Dashboard.PipelineTotalsDates>(),
                    PipelineTotals = (Models.CRM.Dashboard.PipelineTotals)data.ReadSingle<Models.CRM.Dashboard.PipelineTotals>(),
                    PipelineTotalsCurrent = (Models.CRM.Dashboard.PipelineTotals)data.ReadSingle<Models.CRM.Dashboard.PipelineTotals>(),
                    PipelineTotalsPrevious = (Models.CRM.Dashboard.PipelineTotals)data.ReadSingle<Models.CRM.Dashboard.PipelineTotals>(),
                };

                return result;
            });
        }

        public Models.CRM.Contacts.Contact Add(Models.CRM.Contacts.Contact data)
        {
            return WithConnection<Models.CRM.Contacts.Contact>(connection =>
            {
                connection.Insert(data);
                return data;
            });
        }


        /*
        public Models.CRM._formParts.PhoneNumber SavePhone(Models.CRM._formParts.PhoneNumber data)
        {
            try
            {
                if (data.ID == 0)
                    return WithConnection(connection =>
                    {
                        connection.Insert(data);
                        return data;
                    });
                else
                    return WithConnection(connection =>
                    {
                        var isSuccess = connection.Update(data);
                        return data;
                    });
            }
            catch(Exception ex) {
                return new Models.CRM._formParts.PhoneNumber();
            }
        }
        */


        public bool Delete(string type, int id)
        {
            try
            {
                switch (type)
                {
                    case "contact-phone":
                        return WithConnection(connection =>
                        {
                            var isSuccess = connection.Delete(new Models.CRM._formParts.PhoneNumber { ID = id });
                            return true;
                        });
                    case "contact-email":
                        return WithConnection(connection =>
                        {
                            var isSuccess = connection.Delete(new Models.CRM._formParts.EmailAddress { ID = id });
                            return true;
                        });
                    default: return false;
                }

            }
            catch(Exception ex) { return false; }
        }
        /*
        public Models.CRM._formParts.ContactName SaveName(Models.CRM._formParts.ContactName d)
        {
            try
            {
                if (d.PersonID == 0)
                    return WithConnection(connection =>
                    {
                        connection.Insert(d);
                        return d;
                    });
                else
                    return WithConnection(connection =>
                    {
                        var isSuccess = connection.Update(d);
                        return d;
                    });
            }
            catch(Exception ex) { }
            return d;
        }

        */



        public Models.System.GenericJsonResponse Save(dynamic data)
        {
            var res = new Models.System.GenericJsonResponse();
            int? id = null;
            Type t = null;
            bool IsSuccess = false;
            try
            {
                /*
                dynamic d = null;
                switch (data)
                {
                    case Models.CRM._formParts.ContactName _d:
                        _d = data;
                        id = data.PersonID;
                        t = typeof(Models.CRM._formParts.ContactName);
                        break;
                }

                if (id == null)
                    WithConnection(conn =>
                    {
                        conn.Insert(d as t);
                        IsSuccess = true;
                        return IsSuccess;
                    });
                else
                    WithConnection(conn =>
                    {
                        IsSuccess = conn.Update(d);
                        return IsSuccess;
                    });
                data = d;
                //*/

                //*
                switch (data)
                {

                    #region Contacts
                    case Models.CRM._formParts.NewContact n:
                        WithConnection(conn =>
                        {
                            conn.Insert(n);
                            IsSuccess = true;
                            return IsSuccess;
                        });
                        data = n;
                        if (!string.IsNullOrEmpty(n.Email))
                        {
                            var email = new Models.CRM._formParts.EmailAddress()
                            {
                                PersonID = (int)n.PersonID,
                                EmailTypeID = 1,
                                IsPrimary = true,
                                Address = n.Email,
                                Label = "New"
                            };
                            WithConnection(conn =>
                            {
                                conn.Insert(email);
                                IsSuccess = true;
                                return IsSuccess;
                            });
                        }
                        if (!string.IsNullOrEmpty(n.Phone))
                        {
                            var phone = new Models.CRM._formParts.PhoneNumber()
                            {
                                PersonID = (int)n.PersonID,
                                PhoneNumberTypeID = 1,
                                IsPrimary = true,
                                Number = n.Phone,
                                Label = "New"
                            };
                            WithConnection(conn =>
                            {
                                conn.Insert(phone);
                                IsSuccess = true;
                                return IsSuccess;
                            });
                        }
                        break;
                    case Models.CRM._formParts.ContactName d:
                        if(d.PersonID == null)    
                            WithConnection(conn =>
                                {
                                    conn.Insert(d);
                                    IsSuccess = true;
                                    return IsSuccess;
                                });
                        else
                            WithConnection(conn =>
                            {
                                IsSuccess = conn.Update(d);
                                return IsSuccess;
                            });
                        data = d;
                        break;
                    case Models.CRM._formParts.PhoneNumber p:
                        if (p.ID == null)
                            WithConnection(conn =>
                            {
                                conn.Insert(p);
                                IsSuccess = true;
                                return IsSuccess;
                            });
                        else
                            WithConnection(conn =>
                            {
                                IsSuccess = conn.Update(p);
                                return IsSuccess;
                            });
                        data = p;
                        break;
                    case Models.CRM._formParts.EmailAddress e:
                        if (e.ID == null)
                            WithConnection(conn =>
                            {
                                conn.Insert(e);
                                IsSuccess = true;
                                return IsSuccess;
                            });
                        else
                            WithConnection(conn =>
                            {
                                IsSuccess = conn.Update(e);
                                return IsSuccess;
                            });
                        data = e;
                        break;
                    case Models.CRM._formParts.ContactDetails d:
                            WithConnection(conn =>
                            {
                                IsSuccess = conn.Update(d);
                                return IsSuccess;
                            });
                        data = d;
                        break;
                    case Models.CRM._formParts.ContactAddress d:
                            WithConnection(conn =>
                            {
                                IsSuccess = conn.Update(d);
                                return IsSuccess;
                            });
                        data = d;
                        break;
                    case Models.CRM._formParts.ContactMisc d:
                            WithConnection(conn =>
                            {
                                IsSuccess = conn.Update(d);
                                return IsSuccess;
                            });
                        data = d;
                        break;
                    #endregion

                    #region Vendors
                    case Models.CRM._formParts.VendorDetail d:
                        if (d.VendorID == null)
                            WithConnection(conn =>
                            {
                                conn.Insert(d);
                                IsSuccess = true;
                                return IsSuccess;
                            });
                        else
                            WithConnection(conn =>
                            {
                                IsSuccess = conn.Update(d);
                                return IsSuccess;
                            });
                        data = d;
                        break;
                    #endregion

                    #region Leads
                    case Models.CRM._formParts.LeadDetails d:
                        if (d.ExtendedLeadID == null)
                        {
                            WithConnection(conn =>
                            {
                                conn.Insert(d);
                                IsSuccess = true;
                                return IsSuccess;
                            });
                        }
                        else
                            WithConnection(conn =>
                            {
                                IsSuccess = conn.Update(d);
                                return IsSuccess;
                            });
                        data = d;
                        break;
                    case Models.CRM._formParts.LeadGoals d:
                        if (d.ExtendedLeadID == null)
                            WithConnection(conn =>
                            {
                                conn.Insert(d);
                                IsSuccess = true;
                                return IsSuccess;
                            });
                        else
                            WithConnection(conn =>
                            {
                                IsSuccess = conn.Update(d);
                                return IsSuccess;
                            });
                        data = d;
                        break;
                    case Models.CRM._formParts.LeadSites d:
                        if (d.ExtendedLeadID == null)
                            WithConnection(conn =>
                            {
                                conn.Insert(d);
                                IsSuccess = true;
                                return IsSuccess;
                            });
                        else
                            WithConnection(conn =>
                            {
                                IsSuccess = conn.Update(d);
                                return IsSuccess;
                            });
                        data = d;
                        break;
                    #endregion

                    #region Potentials
                    case Models.CRM._formParts.PotentialDetail d:
                        if (d.ExtendedPotentialID == null)
                            WithConnection(conn =>
                            {
                                conn.Insert(d);
                                IsSuccess = true;
                                return IsSuccess;
                            });
                        else
                            WithConnection(conn =>
                            {
                                IsSuccess = conn.Update(d);
                                return IsSuccess;
                            });
                        data = d;
                        break;
                    case Models.CRM._formParts.PotentialWeekly d:
                        if (d.ExtendedPotentialID == null)
                            WithConnection(conn =>
                            {
                                conn.Insert(d);
                                IsSuccess = true;
                                return IsSuccess;
                            });
                        else
                            WithConnection(conn =>
                            {
                                IsSuccess = conn.Update(d);
                                return IsSuccess;
                            });
                        data = d;
                        break;
                    case Models.CRM._formParts.PotentialRevenue d:
                        if (d.ExtendedPotentialID == null)
                            WithConnection(conn =>
                            {
                                conn.Insert(d);
                                IsSuccess = true;
                                return IsSuccess;
                            });
                        else
                            WithConnection(conn =>
                            {
                                IsSuccess = conn.Update(d);
                                return IsSuccess;
                            });
                        data = d;
                        break;
                    case Models.CRM._formParts.PotentialExterior d:
                        if (d.ExtendedPotentialID == null)
                            WithConnection(conn =>
                            {
                                conn.Insert(d);
                                IsSuccess = true;
                                return IsSuccess;
                            });
                        else
                            WithConnection(conn =>
                            {
                                IsSuccess = conn.Update(d);
                                return IsSuccess;
                            });
                        data = d;
                        break;
                    case Models.CRM._formParts.PotentialInterior d:
                        if (d.ExtendedPotentialID == null)
                            WithConnection(conn =>
                            {
                                conn.Insert(d);
                                IsSuccess = true;
                                return IsSuccess;
                            });
                        else
                            WithConnection(conn =>
                            {
                                IsSuccess = conn.Update(d);
                                return IsSuccess;
                            });
                        data = d;
                        break;
                    #endregion


                    #region Property
                    case Models.CRM._formParts.PropertyDetail d:
                        if (d.PropertyID == null)
                            WithConnection(conn =>
                            {
                                conn.Insert(d);
                                IsSuccess = true;
                                return IsSuccess;
                            });
                        else
                            WithConnection(conn =>
                            {
                                IsSuccess = conn.Update(d);
                                return IsSuccess;
                            });
                        data = d;
                        break;
                    case Models.CRM._formParts.PropertyAddress d:
                        if (d.PropertyID == null)
                            WithConnection(conn =>
                            {
                                conn.Insert(d);
                                IsSuccess = true;
                                return IsSuccess;
                            });
                        else
                            WithConnection(conn =>
                            {
                                IsSuccess = conn.Update(d);
                                return IsSuccess;
                            });
                        data = d;
                        break;
                    case Models.CRM._formParts.PropertyOwners d:
                        if (d.PropertyID == null)
                            WithConnection(conn =>
                            {
                                conn.Insert(d);
                                IsSuccess = true;
                                return IsSuccess;
                            });
                        else
                            WithConnection(conn =>
                            {
                                IsSuccess = conn.Update(d);
                                return IsSuccess;
                            });
                        data = d;
                        break;
                    case Models.CRM._formParts.PropertyGoals d:
                        if (d.PropertyID == null)
                            WithConnection(conn =>
                            {
                                conn.Insert(d);
                                IsSuccess = true;
                                return IsSuccess;
                            });
                        else
                            WithConnection(conn =>
                            {
                                IsSuccess = conn.Update(d);
                                return IsSuccess;
                            });
                        data = d;
                        break;
                    case Models.CRM._formParts.PropertyNotes d:
                        if (d.PropertyID == null)
                            WithConnection(conn =>
                            {
                                conn.Insert(d);
                                IsSuccess = true;
                                return IsSuccess;
                            });
                        else
                            WithConnection(conn =>
                            {
                                IsSuccess = conn.Update(d);
                                return IsSuccess;
                            });
                        data = d;
                        break;
                    case Models.CRM._formParts.PropertySpecialInstructions d:
                        if (d.PropertyID == null)
                            WithConnection(conn =>
                            {
                                conn.Insert(d);
                                IsSuccess = true;
                                return IsSuccess;
                            });
                        else
                            WithConnection(conn =>
                            {
                                IsSuccess = conn.Update(d);
                                return IsSuccess;
                            });
                        data = d;
                        break;
                    case Models.CRM._formParts.PropertyLogistics d:
                        if (d.PropertyID == null)
                            WithConnection(conn =>
                            {
                                conn.Insert(d);
                                IsSuccess = true;
                                return IsSuccess;
                            });
                        else
                            WithConnection(conn =>
                            {
                                IsSuccess = conn.Update(d);
                                return IsSuccess;
                            });
                        data = d;
                        break;
                    case Models.CRM._formParts.PropertyAutomation d:
                        if (d.PropertyID == null)
                            WithConnection(conn =>
                            {
                                conn.Insert(d);
                                IsSuccess = true;
                                return IsSuccess;
                            });
                        else
                            WithConnection(conn =>
                            {
                                IsSuccess = conn.Update(d);
                                return IsSuccess;
                            });
                        data = d;
                        break;
                    case Models.CRM._formParts.PropertyAlarms d:
                        if (d.PropertyID == null)
                            WithConnection(conn =>
                            {
                                conn.Insert(d);
                                IsSuccess = true;
                                return IsSuccess;
                            });
                        else
                            WithConnection(conn =>
                            {
                                IsSuccess = conn.Update(d);
                                return IsSuccess;
                            });
                        data = d;
                        break;
                    case Models.CRM._formParts.PropertyCodes d:
                        if (d.PropertyID == null)
                            WithConnection(conn =>
                            {
                                conn.Insert(d);
                                IsSuccess = true;
                                return IsSuccess;
                            });
                        else
                            WithConnection(conn =>
                            {
                                IsSuccess = conn.Update(d);
                                return IsSuccess;
                            });
                        data = d;
                        break;
                    case Models.CRM._formParts.PropertyWiFi d:
                        if (d.PropertyID == null)
                            WithConnection(conn =>
                            {
                                conn.Insert(d);
                                IsSuccess = true;
                                return IsSuccess;
                            });
                        else
                            WithConnection(conn =>
                            {
                                IsSuccess = conn.Update(d);
                                return IsSuccess;
                            });
                        data = d;
                        break;
                    case Models.CRM._formParts.PropertyFeatures d:
                        if (d.PropertyID == null)
                            WithConnection(conn =>
                            {
                                conn.Insert(d);
                                IsSuccess = true;
                                return IsSuccess;
                            });
                        else
                            WithConnection(conn =>
                            {
                                IsSuccess = conn.Update(d);
                                return IsSuccess;
                            });
                        data = d;
                        break;
                    case Models.CRM._formParts.PropertyServiceContacts d:
                        if (d.PropertyID == null)
                            WithConnection(conn =>
                            {
                                conn.Insert(d);
                                IsSuccess = true;
                                return IsSuccess;
                            });
                        else
                            WithConnection(conn =>
                            {
                                IsSuccess = conn.Update(d);
                                return IsSuccess;
                            });
                        data = d;
                        break;
                    case Models.CRM._formParts.PropertyImportantDates d:
                        if (d.PropertyID == null)
                            WithConnection(conn =>
                            {
                                conn.Insert(d);
                                IsSuccess = true;
                                return IsSuccess;
                            });
                        else
                            WithConnection(conn =>
                            {
                                IsSuccess = conn.Update(d);
                                return IsSuccess;
                            });
                        data = d;
                        break;
                    case Models.CRM._formParts.PropertyCityLegal d:
                        if (d.PropertyID == null)
                            WithConnection(conn =>
                            {
                                conn.Insert(d);
                                IsSuccess = true;
                                return IsSuccess;
                            });
                        else
                            WithConnection(conn =>
                            {
                                IsSuccess = conn.Update(d);
                                return IsSuccess;
                            });
                        data = d;
                        break;
                    case Models.CRM._formParts.PropertyInspections d:
                        if (d.PropertyID == null)
                            WithConnection(conn =>
                            {
                                conn.Insert(d);
                                IsSuccess = true;
                                return IsSuccess;
                            });
                        else
                            WithConnection(conn =>
                            {
                                IsSuccess = conn.Update(d);
                                return IsSuccess;
                            });
                        data = d;
                        break;
                    case Models.CRM._formParts.PropertySocialMedia d:
                        if (d.PropertyID == null)
                            WithConnection(conn =>
                            {
                                conn.Insert(d);
                                IsSuccess = true;
                                return IsSuccess;
                            });
                        else
                            WithConnection(conn =>
                            {
                                IsSuccess = conn.Update(d);
                                return IsSuccess;
                            });
                        data = d;
                        break;
                    case Models.CRM._formParts.PropertyMeta d:
                        if (d.PropertyID == null)
                            WithConnection(conn =>
                            {
                                conn.Insert(d);
                                IsSuccess = true;
                                return IsSuccess;
                            });
                        else
                            WithConnection(conn =>
                            {
                                IsSuccess = conn.Update(d);
                                return IsSuccess;
                            });
                        data = d;
                        break;
                        #endregion
                }
                //*/
                res = new Models.System.GenericJsonResponse() {
                    IsSuccess = IsSuccess,
                    Title = IsSuccess ? "Saved!" : "Oops!",
                    Message = IsSuccess ? "" : "An error has occurred.",
                    Data = data
                };

            }
            catch(Exception ex)
            {
                res.Message = ex.Message;
            }
            return res;
        }
    }
}