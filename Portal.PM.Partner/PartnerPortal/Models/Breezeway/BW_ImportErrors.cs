using System;

namespace Acme.Models.Breezeway
{
    [Serializable]
    public class ImportErrors
    {
        public int ErrorID { get; set; }
        public int TaskID { get; set; }
        public string Source { get; set; }
        public string Error { get; set; }
        public string Meta { get; set; }
        public DateTime DateLogged { get; set; }
    }
}