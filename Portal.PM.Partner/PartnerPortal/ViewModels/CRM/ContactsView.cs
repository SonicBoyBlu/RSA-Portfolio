using System.Collections.Generic;

namespace Acme.ViewModels.CRM
{
    public class ContactsView
    {
        public ContactsView()
        {
            Contacts = new List<Models.CRM.Contacts.ContactListItem>();
        }
        public string View { get; set; }
        public Models.CRM.Search Query { get; set; }
        public List<Models.CRM.Contacts.ContactListItem> Contacts { get; set; }
        public Models.CRM.Lists.TypeAndStatus Lists { get; set; }
    }
}