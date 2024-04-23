using System;
using System.Web;
using System.Web.Security;
using Acme.Models.System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using Acme.Methods;

namespace Acme.Services
{
    public class IdentityService
    {
        public IdentityService() { }

        public LoginResponse SignIn(string username, string password)
        {

            var response = new LoginResponse();


            //var tracking = new Models.TrackingLog();
            //AuthenticateUserResponse authenticateUserResponse = new AuthenticateUserResponse();
            //Models.User u = null;

            //try
            //{
            //    SqlDataReader r = null;
            //    // pass value of un/pwd wit pwd as MD5hash
            //    // if fail, pass value withouth hash
            //    // has should only be able to be sent if can be decrypted with logged in users UserID (admin silent login?)
            //    using(SqlConnection conn = new SqlConnection(GlobalSettings.DateStores.Default))
            //    {
            //        try
            //        {
            //            SqlCommand cmd = new SqlCommand("spLogin", conn)
            //            {
            //                CommandType = CommandType.StoredProcedure
            //            };
            //            cmd.Parameters.AddWithValue("@username", username);
            //            cmd.Parameters.AddWithValue("@password", password);
            //            cmd.Parameters.AddWithValue("instanceid", 0);
            //            conn.Open();
            //            r = cmd.ExecuteReader();
            //            if (!r.HasRows)
            //            {
            //                r.Close();
            //                cmd.Parameters["@password"].Value = Security.Legacy.MD5Hash(password);
            //                r = cmd.ExecuteReader();
            //            }
            //            while (r.Read())
            //            {
            //                //u = new Models.User()
            //                //{
            //                //    UserID = (int)r["UserID"],
            //                //    /*
            //                //    JitBitUserID = (int)r["JitbitUserID"],
            //                //    SmarterTrackUserID = (int)r["SmarterTrackUserID"],
            //                //    EscapiaUserID = (int)r["EscapiaUserID"],
            //                //    BambooHrUserID = (int)r["BambooHrUserID"],
            //                //    QuickBooksUserID = (int)r["QuickBooksUserID"],
            //                //    UserType = (DataTypes.UserType)((int)r["UserType"]),
            //                //    Username = (string)r["Username"],
                                
            //                //    FirstName = (string)r["FirstName"],
            //                //    LastName = (string)r["LastName"],
            //                //    Phone = (string)r["Phone"],
            //                //    Email = (string)r["Email"],
            //                //    Department = (string)r["Department"],
            //                //    Notes = (string)r["Notes"],

            //                //    IsAdmin = (bool)r["IsAdmin"],
            //                //    IsActive = (bool)r["IsDisabled"] == true ? false : true,
            //                //    IsDepartmentAdmin = (bool)r["IsDepartmentAdmin"],
            //                //    DateLastLogin = (DateTime)r["DateLastLogin"]
            //                //    */
            //                //};

            //                // u = Methods.Users.GetUser(u.UserID);
            //            }

            //            /*
            //            if(u.UserType == DataTypes.UserType.Owner)
            //            {
            //                Services.Vendors.Escapia.GetAuthToken();
            //            }
            //            */

            //            //authenticateUserResponse = new AuthenticateUserResponse()
            //            //{
            //            //    User = u,
            //            //    IsSuccess = u != null
            //            //};
            //        }
            //        catch (Exception ex)
            //        {
            //            r.Close();
            //            conn.Close();
            //            throw;
            //        }
            //    }

            //    if (!authenticateUserResponse.IsSuccess)
            //    {
            //        tracking.Add("Failed login failed for " + username, 0, 0, "User");
            //        response.Fail("Unable to authenticate");
            //        return response;
            //    } else {


            //        CreateFormsAuthenticationTicket(username, authenticateUserResponse);

            //        // Mark the response as successful
            //        response.Success();
            //        tracking.Add("Successful login failed for " + username, u.JitBitUserID, u.JitBitUserID, "JitBitUser");
            //        //response.Data = Security.Encrypt(string.Format("{0}|{1}", username, password));
            //        response.Data = Newtonsoft.Json.JsonConvert.SerializeObject(u);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    response.Fail(ex.Message);
            //}


            //if (authenticateUserResponse.IsSuccess)
            //    HttpContext.Current.Session["IsSPA"] = true;
            //    HttpContext.Current.Session["IsSPA"] = false;

            return response;
        }


        /*
        public LoginResponse GoogleSignIn(Models.Google.User u)
        {
            var a = new AuthenticateUserResponse();
            if (u == null)
            {
                a.Message = "Unable to authenticate.";
            }

            a = new AuthenticateUserResponse()
            {
                //AuthToken = zohoToken,
                IsSuccess = true,
                Message = "ok",
                User = new Models.User()
                {
                    FirstName = u.firstName,
                    LastName = u.lastName,
                    Email = u.email,
                    Phone = u.phoneNumber,
                    PhotoUrl = u.photoURL,
                    Username = u.email
                }
            };
            var r = new LoginResponse()
            {
                Data = a
            };
            CreateFormsAuthenticationTicket(u.email, a);
            return r;
        }
        */
        public JsonNetResult SetSpaTrue()
        {
            //HttpContext.Current.Session["IsSPA"] = true;
            return new JsonNetResult();
        }

        public void SignOut()
        {
            //if(HttpContext.Current.Session != null)
            //    HttpContext.Current.Session["IsSPA"] = false;
            FormsAuthentication.SignOut();
        }

        public bool CreateFormsAuthenticationTicket(string loginName, AuthenticateUserResponse response)
        {
            //var zohoToken = Vendors.Zoho.Authenticate.GetAuthToken(new AuthenticateUserRequest() { Username = loginName, Password = string.Empty });
            //GlobalSettings.Zoho.Credentials.AuthToken = zohoToken;

            // If we got here, we are authorized. Let's attempt to get the identity.
            var u = response.User;

            //var api = new Vendors.BambooHR.Api();
            //u.PhotoUrl = api.GetEmployeePhotoUrl(u.Email);
            //var BbUser = api.GetEmployee(loginName);
            //u.PhotoUrl = BbUser.PhotoURL;
            //u.Department = BbUser.Department;

            var identity = new UserIdentity
            {
                UserID = u.UserID,
                EscapiaUserID = u.EscapiaUserID,
                JitBitUserID = u.JitBitUserID,
                SmarterTrackUserID = u.SmarterTrackUserID,
                BambooHrUserID = u.BambooHrUserID,
                QuickBooksUserID = u.QuickBooksCustomerID,

                FirstName = u.FirstName,
                LastName = u.LastName,
                Username = loginName,
                AvatarUrl = u.PhotoUrl,

                Phone = u.Phone,
                Email = u.Email,
                JobTitle = u.JobTitle,
                Department = u.Department,
                //UserType = u.UserType
                UserTypeID = (int)u.UserType,
                IsAdmin = u.IsAdmin,
                IsDepartmentAdmin = u.IsDepartmentAdmin,
                DateLastLogin = (DateTime)u.DateLastLogin

                //AuthToken = response.AuthToken,
                //AuthToken = zohoToken
            };

            //var test = identity.SerializeProperties();

            // Create the ticket
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                1,
                "AcmeHouseCo_" + loginName,
                DateTime.Now,
                DateTime.Now.AddMinutes(30),
                //DateTime.Now.AddMinutes(GlobalSettings.Zoho.SessionTimeout),
                false,
                identity.SerializeProperties());


            // Encrypt the ticket
            string encTicket = FormsAuthentication.Encrypt(ticket);


            // Create the cookie.
            HttpCookie cookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName]; //saved user
            if (cookie == null)
            {
                HttpContext.Current.Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
            }
            else
            {
                cookie.Value = encTicket;
                HttpContext.Current.Response.Cookies.Set(cookie);
            }


            return true;
        }

        public bool CreateUser(object user)
        {
            return true;
        }

        public LoginResponse AcmePortalSignIn(string username, string password)
        {
            var response = new LoginResponse();
            var tracking = new Models.TrackingLog();

            var userService = new UserService();
            int UserId = userService.GetId(username, password);
            
            if (UserId == 0) {
                tracking.Add("Login failed for " + username, UserId, UserId, "User");
                response.Fail("Unable to authenticate");
                return response;
            }
            
            var user = userService.GetUserById(UserId);
            

            // Methods.User user = new Methods.User(UserId);                                
            
            if (user.IsNull())
            {
                tracking.Add("Login failed GetUser() for " + username, UserId, UserId, "User");
                response.Fail("Unable to authenticate");
                return response;
            }
            
            //var externalApplication = new Services.ExternalApplication.ExternalApplication(UserId);
            //user = externalApplication.MapToUser(user);
            
            var authenticateUserResponse = new AuthenticateUserResponse()
            {
                User = user,
                IsSuccess = false //true
            };

            // Auth User Success
            // Mark the response as successful

            response.Success();
            tracking.Add("Login success for " + username, UserId, UserId, "User");
                                   
            CreateFormsAuthenticationTicket(username, authenticateUserResponse);
            response.Data = Newtonsoft.Json.JsonConvert.SerializeObject(user);

            return response;

        }

        
    }
}