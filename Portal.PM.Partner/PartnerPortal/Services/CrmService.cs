using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using Acme.Data.Repositories;
using Acme.Models.CRM;
using Microsoft.Ajax.Utilities;

namespace Acme.Services
{
    public class CrmService
    {
        internal static CrmRepository Repo;
        public CrmService()
        {
            Repo = new CrmRepository();
        }

        /*
        public Models.CRM.Contacts.Contact All(Models.CRM.Contacts.Contact data)
        {
            Models.CRM.Contacts.Contact result = new Models.CRM.Contacts.Contact();
            if (data.PersonID == 0)
                result = Repo.Add(data);

            return result;
        }

        public Models.CRM.FormParts.PhoneNumber SavePhone(Models.CRM.FormParts.PhoneNumber data)
        {
            var result = new Models.CRM.FormParts.PhoneNumber();
            result = Repo.SavePhone(data);
            return result;
        }
        
        public Models.CRM.formParts.ContactName SaveContactName(Models.CRM.formParts.ContactName data)
        {
            var result = Repo.SaveName(data);
            return result;
        }
        */
        public bool Delete(string Type, int ID)
        {
            return Repo.Delete(Type, ID);
        }

        public Models.System.GenericJsonResponse Save(dynamic data)
        {
            return Repo.Save(data);
        }

        public ViewModels.CRM.DashboardViewModel GetDashboard(DateTime DateStart, DateTime DateEnd)
        {
            return Repo.GetDashboard(DateStart, DateEnd);
        }
    }
}