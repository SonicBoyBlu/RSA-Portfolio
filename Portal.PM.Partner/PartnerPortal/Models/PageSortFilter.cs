using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Acme.Models
{
    public class PageSortFilter
    {
        public string SortField { get; set; }
        public bool SortAscending { get; set; }

        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}