using System;
using System.Net;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using WebMarkupMin.AspNet4.Common;
using WebMarkupMin.Sample.AspNet4.Mvc4;

namespace Acme
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static DateTime ApplicationStartDate;

        protected void Application_Start()
        {
            // turn off SSLv3 and TLSv1.0
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            WebMarkupMinConfig.Configure(WebMarkupMinConfiguration.Instance);
            //AuthConfig.RegisterAuth();

            // Set the application's start date for easy reference
            ApplicationStartDate = DateTime.Now;
        }

        public override void Init()
        {
            this.PostAuthenticateRequest += new EventHandler(MvcApplication_PostAuthenticateRequest);
            base.Init();
        }

        void MvcApplication_PostAuthenticateRequest(object sender, EventArgs e)
        {
            var authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                var identity = UserIdentity.Deserialize(authCookie.Value);
                if (identity == null)
                {
                    FormsAuthentication.SignOut();
                }
                else
                {
                    var principal = new GenericPrincipal(identity, null);
                    HttpContext.Current.User = principal;
                    System.Threading.Thread.CurrentPrincipal = principal;
                    Context.User = new GenericPrincipal(identity, null);

                    /*
                    HttpContext.Current.User = new GenericPrincipal(identity, null);
                    Context.User = new GenericPrincipal(identity, null);
                    */
                }
            }
        }

    }
}
