using Global.Location;

namespace Global.Interfaces
{
    interface IContactCard
    {
        string Phone1 { get; set; }
        string Phone2 { get; set; }
        string Fax { get; set; }
        Address Address { get; set; }

    }
}
