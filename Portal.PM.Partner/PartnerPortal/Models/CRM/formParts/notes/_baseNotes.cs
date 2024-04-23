using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Acme.Models.CRM._formParts
{
    public class _baseNotes
    {
        public _baseNotes()
        {
            NoteID = null;
            ModifiedBy = Identity.Current.FullName;
            CreatedBy = Identity.Current.FullName;
            DateModified = DateTime.Now;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? NoteID { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime DateModified { get; set; }
    }
}