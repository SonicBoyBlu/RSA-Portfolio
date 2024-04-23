namespace Acme.ViewModels.CRM
{
    public class ContactDetailViewModel : ICloneable
    {
        public ContactDetailViewModel()
        {
            Lists = new Models.CRM.Lists.TypeAndStatus();
            ContactDetail = new Models.CRM.Contacts.Contact();
            HeaderDetails = new ContactHeaderVewiewModel()
            {
                Phone = "[Phone]",
                Email = "[Email]",
                ContactStatus = "[Contact or Pipeline Status]",
                ContactType = "[Contact Type]"
            };
        }
        public virtual object Clone() { return this.MemberwiseClone(); }

        public ContactHeaderVewiewModel HeaderDetails { get; set; }
        public Models.CRM.Contacts.Contact ContactDetail { get; set; }
        public Models.CRM.Lists.TypeAndStatus Lists { get; set; }
    }

    public class ContactHeaderVewiewModel
    {
        public string ContactType { get; set; }
        public string ContactStatus { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }        
    }
}