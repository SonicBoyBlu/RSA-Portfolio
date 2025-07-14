using Global.Interfaces;

namespace Global.Location
{

    public class Addresses : List<Addresses>;
    public class Address : IAddress
    {
        public Address()
        {
            Region = "USA";
            Address1 = string.Empty;
            Address2 = string.Empty;
            City = string.Empty;
            State = string.Empty;
            Zip = string.Empty;
            Country = "USA";
        }
        public string Region { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int StateID { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
    }
}
