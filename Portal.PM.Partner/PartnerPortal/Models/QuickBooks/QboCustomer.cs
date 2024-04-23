using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Acme.Models.QuickBooks
{
    public class Customer
    {
        public int QboID { get; set; }
        public int ID { get; set; }
        public bool IsActive { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; } 
        public string MiddleName { get; set; } 
        public string LastName { get; set; } 
        public string FullyQualifiedName { get; set; } 
        public string CompanyName { get; set; } 
        public string DisplayName { get; set; } 
        public string PrimaryPhone { get; set; } 
        public string AlertnatePhone { get; set; } 
        public string MobilePhone { get; set; } 
        public string Email { get; set; } 
        public string Notes { get; set; } 
    }
}