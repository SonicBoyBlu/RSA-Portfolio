using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Acme.ViewModels
{
    public class Common
    {
        public class IdTitlePair
        {
            public int Id { get; set; }
            public string Title { get; set; }
        }
        public class GuidTitlePair
        {
            public Guid Id { get; set; }
            public string Title { get; set; }
        }
        public class MessageResult
        {
            public bool success { get; set; }
            public string message { get; set; }
            public string url { get; set; }
        }
        public class OptionPair
        {
            public string value { get; set; }
            public string name { get; set; }
        }
    }
}