using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Acme.Models.CRM
{
    public class Search
    {
        public Search()
        {
            //var me = Identity.Current;
            var lastMonth = DateTime.Now.AddMonths(-1);
            var dateStart = new DateTime(lastMonth.Year, lastMonth.Month, 1);
            var dateEnd = dateStart.AddMonths(1).AddDays(-1);

            DateStart = dateStart;
            DateEnd = dateEnd;

            ItemOwner = string.Empty;
            //Status = string.Empty;
            StatusID = 0;
            Query = string.Empty;
        }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public string ItemOwner { get; set; }
        //public string Status { get; set; }
        public int StatusID { get; set; }
        public int ContactTypeID { get; set; }
        public string Query { get; set; }
        public string ItemID { get; set; }
        public bool ShowInactive { get; set; }
    }
}