namespace Global.Interfaces
{
    interface IState
    {
        int StateID { get; set; }
        string StateName { get; set; }
        string StateCode { get; set; }
        //string StateAbbrev { get; set; }

    }
}
