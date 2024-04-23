namespace BlazorComms.Models
{
    public class _RecipientPrototype
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get
            {
                return FirstName + " " + LastName;
            } }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string PushID { get; set; }
    }
    public class RecipientFsi : _RecipientPrototype
    {
        public int TimeslotID { get; set; }
        public DateTime DateTimeslot { get; set; }
    }
}
