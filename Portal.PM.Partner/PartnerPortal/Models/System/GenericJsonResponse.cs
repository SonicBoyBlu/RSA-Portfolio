using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Acme.Models.System
{
    public class GenericJsonResponse
    {
        public GenericJsonResponse()
        {
            Title = "Unknown Error!";
            Message = "Oops, an unknown error has occurred.";
        }
        public string CssClass { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public bool IsProcessing { get; set; }
        public bool IsSuccess { get; set; }
        public dynamic Data { get; set; }
    }
}