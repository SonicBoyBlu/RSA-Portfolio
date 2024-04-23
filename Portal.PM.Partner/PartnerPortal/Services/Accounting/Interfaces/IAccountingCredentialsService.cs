using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.Services.Accounting
{
    public interface IAccountingCredentialsService
    {
        AccountingCredentials GetCredentials();
        void SaveCredentials(AccountingCredentials credentials);
    }
}
