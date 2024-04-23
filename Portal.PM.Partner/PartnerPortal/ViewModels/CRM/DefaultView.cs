using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Acme.ViewModels.CRM
{
    public class DefaultView
    {
        public string Tab { get; set; }
        public string Title { get; set; }
        public Models.CRM.Lists.TypeAndStatus Lists { get; set; }
    }
}