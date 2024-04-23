using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Acme.Models
{
    public class Toast
    {
        public Toast()
        {
            ToastrClass = "toast-info";
        }
        public string ToastrClass { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public bool IsProcessing { get; set; }
    }
}