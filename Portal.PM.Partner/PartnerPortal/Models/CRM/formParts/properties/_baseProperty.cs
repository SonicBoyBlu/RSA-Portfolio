using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Acme.Models.CRM._formParts
{
    public class _baseProperty
    {
        public _baseProperty()
        {
            PropertyID = null;
            PersonID = null;
            ModifiedBy = Identity.Current.FullName;
            CreatedBy = Identity.Current.FullName;
            DateModified = DateTime.Now;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? PropertyID { get; set; }
        public int ?PersonID { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime DateModified { get; set; }
    }
}