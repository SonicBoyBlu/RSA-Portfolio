using Global.Interfaces;

namespace Global.Location
{
    public class ContactCard : IContactCard
    {
        public ContactCard()
        {
            Address = new Address();
        }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Fax { get; set; }
        public Address Address { get; set; }
    }
}
