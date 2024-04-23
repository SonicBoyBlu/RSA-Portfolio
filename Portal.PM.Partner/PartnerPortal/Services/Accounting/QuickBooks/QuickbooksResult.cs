using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Acme.Services.Accounting.QuickBooks
{
    public class QuickbooksResult<T>
    {
        public T Entity { get; set; }
        public bool OperationSucceeded { get; set; }
        public string ErrorMessage { get; set; }
    }
}