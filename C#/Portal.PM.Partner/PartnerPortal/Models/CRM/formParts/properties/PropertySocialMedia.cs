using System.ComponentModel.DataAnnotations.Schema;

namespace Acme.Models.CRM._formParts
{
    [Table("Properties")]
    public class PropertySocialMedia : _baseProperty
    {
        public string Website { get; set; }
        public string SocialFacebook { get; set; }
        public string SocialGooglePlus { get; set; }
        public string SocialInstagram { get; set; }
        public string SocialLinkedIn { get; set; }
        public string SocialTwitter { get; set; }
    }
}