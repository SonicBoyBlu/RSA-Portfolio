namespace Global.Interfaces
{
    interface IAddress
    {
        string Region { get; set; }
        string Address1 { get; set; }
        string Address2 { get; set; }
        string City { get; set; }
        string State { get; set; }
        int StateID { get; set; }
        string Zip { get; set; }
        string Country { get; set; }
    }
}
