using Dapper.FluentMap.Mapping;

namespace Acme.Data.Maps
{
    internal class CrmPhoneNumberMap : EntityMap<Models.CRM._formParts.PhoneNumber>
    {
        internal CrmPhoneNumberMap()
        {
            Map(x => x.Number).ToColumn("PhoneNumber");
        }
    }
}