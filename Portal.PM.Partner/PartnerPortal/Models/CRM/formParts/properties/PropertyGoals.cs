using System.ComponentModel.DataAnnotations.Schema;

namespace Acme.Models.CRM._formParts
{
    [Table("Properties")]
    public class PropertyGoals : _baseProperty
    {
        public string PersonalUsage { get; set; }
        public string Incentives { get; set; }
        public string ExpectationsGoals { get; set; }
    }
}