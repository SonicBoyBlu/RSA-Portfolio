using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Acme.ViewModels
{
    public class MyProperty
    {
        public MyProperty()
        {
            //Photos = new List<string>();
        }
        public int ID { get; set; }
        public int PropertyID { get { return ID; } }
        public string UnitCode { get; set; }
        public string Url { get; set; }
        public string UnitName { get; set; }
        public Address Address { get; set; }

        public float Latitude { get; set; }
        public float Longitude { get; set; }

        public double Bathrooms { get; set; }
        public int Bedrooms { get; set; }
        public double Sleeps { get; set; }

        public string WebDescription { get; set; }

        public string ThumbnailImageURL { get; set; }
        public string KeyCode { get; set; }

        public DateTime NextArrival { get; set; }

        public string MaintenanceNotes { get; set; }
        public string HousekeepingNotes { get; set; }
        public bool IsActive { get; set; }

        /*
        public List<string> Photos { get; set; }
        public string Highlights { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Distance { get; set; }
        public bool DogsAllowed { get; set; }
        public double Price { get; set; }
        public int NumInterested { get; set; }
        */

        // TODO: address, favorite
    }

    public class Address
    {
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
    }
}