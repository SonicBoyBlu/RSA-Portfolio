using System.Web.Mvc;
using Acme.Filters;
using WebMarkupMin.AspNet4.Mvc;

namespace Acme
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new RequireAuthorizationAttribute());
            filters.Add(new RequireSecureConnectionFilter());
            filters.Add(new HandleErrorAttribute());

            filters.Add(new CompressContentAttribute());
            filters.Add(new MinifyHtmlAttribute());
            filters.Add(new MinifyXmlAttribute());
        }
    }
}
