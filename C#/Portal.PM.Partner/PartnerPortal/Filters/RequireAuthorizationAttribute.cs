using System;
using System.Web.Mvc;

namespace Acme.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class RequireAuthorizationAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true) || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
                return;

            if (filterContext.HttpContext.Request.IsAuthenticated)
            {
                var identity = filterContext.HttpContext.User.Identity as UserIdentity;
                if (identity == null)
                {
                    base.OnAuthorization(filterContext);
                    return;
                }
            }
            else
            {
                filterContext.Result = new RedirectResult("~/Login");
                //base.OnAuthorization(filterContext);
                return;
            }
            //base.OnAuthorization(filterContext);
        }
    }
}