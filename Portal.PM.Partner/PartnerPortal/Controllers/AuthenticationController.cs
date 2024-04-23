using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Services.Description;
using Acme.Models.ExternalApplication;
using Acme.Models.System;
using Acme.Services;
using Acme.ViewModels.Authentication;

namespace Acme.Controllers
{
    [AllowAnonymous]
    public class AuthenticationController : Controller
    {
        #region Login

        // GET: Authentication
        [AllowAnonymous]
        [Route("~/Login")]
        public ActionResult Login()
        {
            var model = new LoginViewModel();
            return View(model);
        }

        [HttpPost]
        [Route("~/Login")]
        public async System.Threading.Tasks.Task<JsonNetResult> LoginAsync(LoginViewModel model)
        {
            await Services.Vendors.Escapia.EscapiaToken.SetEscapiaToken();
            
            var service = new IdentityService();

            if (model.IsGoogleAuthorized)
            {
                return null;
            }
            else
            {
                // Models.System.LoginResponse response = service.SignIn(model.Username, model.Password);
                LoginResponse resultAcmePortalSignIn = new LoginResponse();

                //#if DEBUG

                resultAcmePortalSignIn = service.AcmePortalSignIn(model.Username, model.Password);

                //if (resultAcmePortalSignIn.Status)
                //    System.Web.HttpContext.Current.Session["IsSPA"] = false; // true;

                //#endif

                // var pw = new Services.PasswordReset();
                // pw.SendPasswordRest(1603);


                return new JsonNetResult(resultAcmePortalSignIn);
            }
        }

        /*
        [HttpPost]
        [Route("~/GoogleSignIn")]
        public JsonNetResult GoogleSignIn(Models.Google.User user)
        {
            var srv = new IdentityService();
            var r = srv.GoogleSignIn(user);
            return new JsonNetResult(r);
        }
        */

        #endregion Login

        #region Logout

        [AllowAnonymous]
        [Route("~/logout")]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        #endregion Logout

        #region Auth Tokens

        [AllowAnonymous]
        [Route("~/token/set")]
        public JsonNetResult GetToken(string credentials)
        {
            return new JsonNetResult(Security.Encrypt(credentials));
        }

        [AllowAnonymous]
        [Route("~/AuthToken")]
        public JsonNetResult AuthToken(string token)
        {
            return new JsonNetResult(Security.Decrypt(token));
            ;
        }

        #endregion Auth Tokens

        #region Escapia

        [Route("~/token/escapia/set")]
        public JsonNetResult SetEscapiaToken(string token)
        {
            GlobalSettings.Escapia.HomeAwayCredentials.Token = token;
            return null;
        }

        #endregion

        #region ResetPassowrd

        [HttpGet]
        [Route("~/ResetPassword")]
        public ActionResult ResetPassword(Guid id)
        {
            FormsAuthentication.SignOut();
            Guid token = id.ToGuidOrDefault();
            
            if (token.IsEmpty())
                return RedirectToAction("Login", new { st = "invalidtoken" });

            var service = new Services.PasswordReset();
            Models.User_PasswordToken pwRequest = service.ValidateIncomingResetPasswordRequest(token);
            
            // ResetPassword page accessed with no token found > Redirect user
            if (pwRequest.IsNull())
                return RedirectToAction("Login", new { st = "pwrestnotfound" }); 
            // --------------------------------------------------

            return View(token);

        }

        [HttpPost]
        [Route("~/SendResetPassword")]
        public async Task<JsonResult> ResetPassword(string username)
        {
            string email = username.ToStringOrDefault().ToLower().Trim();

            if(email.IsNullOrEmpty())
                return new JsonResult();

            var service = new Services.PasswordReset();
            ViewModels.Common.MessageResult message = service.SendPasswordRest(username);

            return Json(message);

        }

        [HttpPost]
        [Route("~/ResetPassword")]
        public async Task<JsonResult> ResetPassword(Guid id, string password1, string password2)
        {
            FormsAuthentication.SignOut();
            Guid token = id.ToGuidOrDefault();

            var ipaddress = Request.UserHostAddress.ToStringOrDefault();
            var service = new Services.PasswordReset();
            
            ViewModels.Common.MessageResult message = service.ResetPassword(token, password1, password2, ipaddress);
            
            return Json(message);

        }

        #endregion

    }
}