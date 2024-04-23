using System.Web.Mvc;

namespace Acme.Controllers
{
    public class JsonErrorHandler : ActionFilterAttribute, IExceptionFilter
    {
        public string ErrorMessage {get; set;}
        public void OnException(ExceptionContext filterContext)
        {
           if (filterContext.ExceptionHandled) 
                return;

            //_logger.Error("Uncaught exception", filterContext.Exception);

            filterContext.Result =  new JsonNetResult(new { Error = ErrorMessage });

            // Prepare the response code.
            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Clear();
            filterContext.HttpContext.Response.StatusCode = 500;
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
        }
    }
}