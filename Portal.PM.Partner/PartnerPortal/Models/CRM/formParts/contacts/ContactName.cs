using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Acme.Interfaces.CRM
{
    public interface IContactName
    {
        int? PersonID { get; set; }
        //int ContactTypeID { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Salutation { get; set; }
    }
}
namespace Acme.Models.CRM._formParts
{
    [Table("People")]
    public class ContactName : _baseContacts
    {
        public ContactName()
        {
            //ContactTypeID = 1;
        }
        //public int ContactTypeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Salutation { get; set; }
    }

    [Table("People")]
    public class NewContact : _baseContacts
    {
        public NewContact()
        {
            //ContactTypeID = 1;
        }
        public int ContactTypeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Editable(false)]
        public string Phone { get; set; }
        [Editable(false)]
        public string Email { get; set; }
    }

}