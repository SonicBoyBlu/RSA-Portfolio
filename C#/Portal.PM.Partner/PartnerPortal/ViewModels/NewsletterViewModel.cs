using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Acme.ViewModels
{
    public class NewsletterViewModel
    {
        public NewsletterViewModel()
        {
            Articles = new List<Models.NewletterItem>();
        }
        public List<Models.NewletterItem> Articles { get; set; }
    }
}