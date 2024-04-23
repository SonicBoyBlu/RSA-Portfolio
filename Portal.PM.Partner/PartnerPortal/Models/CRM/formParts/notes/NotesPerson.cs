using System.ComponentModel.DataAnnotations.Schema;

namespace Acme.Models.CRM._formParts
{
    [Table("NotesPeople")]
    public class NotesPerson : _baseNotes
    {
        public int PersonID { get; set; }
    }
}