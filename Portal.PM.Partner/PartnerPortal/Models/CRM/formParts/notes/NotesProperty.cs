using System.ComponentModel.DataAnnotations.Schema;

namespace Acme.Models.CRM._formParts
{
    [Table("NotesProperties")]
    public class NotesProperty : _baseNotes
    {
        public int PropertyID { get; set; }
    }
}