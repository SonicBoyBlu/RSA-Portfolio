using Acme.Services.Tracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Acme.Controllers
{
    public class BaseController : Controller
    {
        protected IUserActionService _userActionService;

        public BaseController()
        {
            _userActionService = new UserActionService();
        }

        protected JsonNetResult WithJsonErrorHandler(string errorMessage, Func<JsonNetResult> runControllerAction)
        {
            try
            {
                return runControllerAction(); // Execute getData, which has been passed in as a Func<IDBConnection, Task<T>>
            }
            catch (Exception ex)
            {
                //Log Exception
                return new JsonNetResult(new { error = errorMessage });
            }
        }



    }
}