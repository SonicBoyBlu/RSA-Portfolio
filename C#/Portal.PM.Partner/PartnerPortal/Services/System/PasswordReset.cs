using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using Acme.Data.Repositories;
using Acme.Methods;
using Acme.Models;

namespace Acme.Services
{
    public class PasswordReset
    {
        private PasswordResetRequests _repoPasswordReset;
        private TrackingLog _tracking;

        public PasswordReset()
        {
            _repoPasswordReset = new Data.Repositories.PasswordResetRequests();
            _tracking = new Models.TrackingLog();
        }

        public ViewModels.Common.MessageResult SendPasswordRest(string email)
        {
            var _userService = new Methods.UserService();
            User user = _userService.GetUserByEmail(email);

            if(user.IsNull())
                return new ViewModels.Common.MessageResult { message = "Password Reset failed", success = false };

            return SendPasswordRest(user.UserID);
        }


        public ViewModels.Common.MessageResult SendPasswordRest(int userId)
        {
            var _userService = new Methods.UserService();
            User user = _userService.GetUserById(userId);

            // If user requested a password already
            var pwRecentRequest = RecentRequestId(userId);

            // Create new Password Request
            if (pwRecentRequest.IsNull())
                pwRecentRequest = DeleteAndCreateNewPasswordResetRequest(user);
            
            var url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
            var passwordResetLink = (url + "/ResetPassword?id=" + pwRecentRequest.Token.ToStringOrDefault().ToUpper());
            
            List<string> marketingCategories = new List<string> { "PasswordReset", "Authentication" };
            var email = new Services.EmailService();
            // var send = email.Send(user.Email, "Password Reset", passwordResetLink , marketingCategories);
            var templateValues = new ViewModels.Email.SendGridDynamicTemplate.ForgotPassword{resetpasswordurl = passwordResetLink,to = user.Email};
            _ = email.SendUsingDynamicTemplateData(user.Email, templateValues, marketingCategories);
            _tracking.Add("Password reset email sent to " + user.Email, user.UserID, 0, "User_PasswordToken");

            return new ViewModels.Common.MessageResult{message = "Password Reset sent", success = true};
        }

        private User_PasswordToken DeleteAndCreateNewPasswordResetRequest(User user)
        {
            //Delete any leftover password requests
            DeleteOldPasswordResetRequestsByMemId(user.UserID);
            DeleteOldPasswordResetRequests();

            // Create Password Reset Request
            User_PasswordToken model = InitializeDbUser_PasswordToken(user.UserID);
            var pwRecentRequestToken = _repoPasswordReset.CreateUser_PasswordToken(model);

            return pwRecentRequestToken;
        }

        
        private User_PasswordToken InitializeDbUser_PasswordToken(int userId)
        {
            var model = new User_PasswordToken();
            model.HasClicked = false;
            model.IsActive = true;
            model.Created = DateTime.Now;
            model.UserID = userId;
            model.Token = Guid.NewGuid().ToStringOrDefault().ToUpper();
            
            return model;
        }

        private void DeleteOldPasswordResetRequests()
        {
            _repoPasswordReset.DeleteOldPasswordResetRequests();
        }

        private void DeleteOldPasswordResetRequestsByMemId(int userId)
        {
            _repoPasswordReset.DeleteOldPasswordResetRequests(userId);
        }

        private User_PasswordToken RecentRequestId(int userId)
        {
            return _repoPasswordReset.RecentRequestGuid(userId);
        }

        internal User_PasswordToken ValidateIncomingResetPasswordRequest(Guid token)
        {
            if (token.IsEmpty())
                return null;
            
            User_PasswordToken model = _repoPasswordReset.GetResetPasswordByToken(token);

            // Acme.Models.User_PasswordToken model = _repoPasswordReset.GetResetPasswordUrl(userId);
            // BLL.Controllers.MemberToClientController.UpdateIsAuthProperty(memId, 0);

            if (model.IsNull())
                return null;
            
            // _repoPasswordReset.DeleteOldPasswordResetRequests(model.UserID);
            return model;
            
        }

        public ViewModels.Common.MessageResult ResetPassword(Guid token, string password1, string password2, string ipaddress)
        {
            
            ViewModels.Common.MessageResult result = UpdatePasswordErrorChecking(token, password1, password2);
            
            if (!result.success)
            {
                _tracking.Add("Password reset failed " + result.message, 0, 0, "User_PasswordToken");
                result.url = "/ResetPassword?id=" + token + "&st=" + result.message;
                return result;
            }
            
            User user = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(result.message);
            
            UserService userService = new UserService();
            Acme.Models.UserUpdate data = new UserUpdate { ipaddress = ipaddress, userid = user.UserID, password = password1};
            userService.UpdatePassword(data);

            _tracking.Add("Password reset success for " + user.Email, user.UserID, 0, "User_PasswordToken");


            List<string> marketingCategories = new List<string> { "PasswordReset", "Authentication" };
            var email = new Services.EmailService();
            // var send = email.Send(user.Email, "Password Reset", passwordResetLink , marketingCategories);
            var templateValues = new ViewModels.Email.SendGridDynamicTemplate.PasswordChangedNotice { to = user.Email };
            _ = email.SendUsingDynamicTemplateData(user.Email, templateValues, marketingCategories);
            _tracking.Add("Password reset email sent to " + user.Email, user.UserID, 0, "User_PasswordToken");



            DeleteOldPasswordResetRequestsByMemId(user.UserID);
            DeleteOldPasswordResetRequests();

            return result;
        }

        private ViewModels.Common.MessageResult UpdatePasswordErrorChecking(Guid token, string password1, string password2)
        {
            ViewModels.Common.MessageResult result = new ViewModels.Common.MessageResult();
            result.success = true;

            if (token.IsEmpty())
                return new ViewModels.Common.MessageResult { success = false, message = "Password reset not found" };

            if (password1.IsNullOrEmpty() || password2.IsNullOrEmpty() || password1 != password2)
                return new ViewModels.Common.MessageResult { success = false, message = "Passwords did not match" };

            User_PasswordToken model = _repoPasswordReset.GetResetPasswordByToken(token);

            if (model.IsNull())
                return new ViewModels.Common.MessageResult { success = false, message = "Password reset expired" };

            UserService userService = new UserService();
            User user = userService.GetUserById(model.UserID);

            if(user.IsNull())
                return new ViewModels.Common.MessageResult { success = false, message = "User not found" };

            if (!user.IsActive)
                return new ViewModels.Common.MessageResult { success = false, message = "InActive User" };
            
            if(result.success)
                result.message = Newtonsoft.Json.JsonConvert.SerializeObject(user);
            
            return result;
        }
    }
}
