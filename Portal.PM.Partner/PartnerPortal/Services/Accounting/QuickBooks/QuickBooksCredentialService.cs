using Acme.Methods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Acme.Services.Accounting.QuickBooks
{
    public class QuickBooksCredentialService : IAccountingCredentialsService
    {
        //TBD: Read and write these to DB.
        int userId = 1602;
        int roleId = 6;

        public AccountingCredentials GetCredentials()
        {
            try
            {
                AccountingCredentials creds = null;
                ExternalApplicationService appService = new ExternalApplicationService();
                var appUser = appService.ForUser(userId);
                if (appUser != null && appUser.QuickBooks != null)
                {
                    creds = new AccountingCredentials();
                    creds.RealmId = appUser.QuickBooks.RealmId;
                    creds.Token = appUser.QuickBooks.Token;
                    creds.RefreshToken = appUser.QuickBooks.RefreshToken;
                }

                return creds;
            }
            catch (Exception ex)
            {
                throw new AccessViolationException("Access denied, make sure SetToken has been called", ex);
            }
        }

        public void SaveCredentials(AccountingCredentials credentials)
        {
            ExternalApplicationService appService = new ExternalApplicationService();
            Models.ExternalApplication.QuickBooks model = new Models.ExternalApplication.QuickBooks()
            {
                RealmId = credentials.RealmId,
                Token = credentials.Token,
                RefreshToken = credentials.RefreshToken
            };
            appService.UpdateUser(GlobalSettings.ExternalApplication.QuickBooks, userId, roleId, model);
        }
    }
}