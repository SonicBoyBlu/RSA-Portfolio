using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Acme.Data.Maps;
using Acme.Models;
using Antlr.Runtime.Tree;
using DataTypes;
using GlobalSettings;
using Escapia = Acme.Models.ExternalApplication.Escapia;

namespace Acme.Methods
{
    public class UserService
    {
        public UserService()
        {
            
        }
        public int GetId(Guid guid)
        {
            int UserID = 0;

            using (SqlConnection conn = new SqlConnection(GlobalSettings.DateStores.AcmePortal))
            {
                var sql = "Select top 1 ID from [User] where Guid = @guid";
                SqlCommand cmd = new SqlCommand(sql, conn) { CommandType = CommandType.Text };
                cmd.Parameters.AddWithValue("@guid", guid);

                conn.Open();
                using (var row = cmd.ExecuteReader())
                {
                    while (row.Read())
                    {
                        try
                        {
                            UserID = row["ID"].ToInt32OrDefault();
                            break;
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message);
                        }
                    }
                }

                conn.Close();
            }

            return UserID;
        }

        public Task<Escapia.Owner> GetEscapiaOwner(int id)
        {
            var srvUser = new Services.Vendors.Escapia.EscapiaOwner();
            Task<Escapia.Owner> owner = srvUser.FetchOwnerFromEscapia(id);
            return owner;
        }

        public int GetId(string email)
        {
            int UserID = 0;

            using (SqlConnection conn = new SqlConnection(GlobalSettings.DateStores.AcmePortal))
            {
                var sql = "Select top 1 ID from [User] where Email = @email";
                SqlCommand cmd = new SqlCommand(sql, conn) { CommandType = CommandType.Text };
                cmd.Parameters.AddWithValue("@email", email.ToStringOrDefault().ToLower().Trim());

                conn.Open();
                using (var row = cmd.ExecuteReader())
                {
                    while (row.Read())
                    {
                        try
                        {
                            UserID = row["ID"].ToInt32OrDefault();
                            break;
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message);
                        }
                    }
                }

                conn.Close();
            }

            return UserID;
        }
        internal User GetUserByEmail(string email)
        {
            int UserID = GetId(email);
            return GetUserById(UserID);
        }
        public int GetId(string username, string password)
        {
            int userId = 0;

            if (username.IsNullOrEmpty() || password.IsNullOrEmpty())
                return userId;

            using (SqlConnection conn = new SqlConnection(GlobalSettings.DateStores.AcmePortal))
            {
                SqlDataReader datareader = null;

                try
                {
                    SqlCommand cmd = new SqlCommand("spLogin", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@password", Security.Legacy.MD5Hash(password));
                    cmd.Parameters.AddWithValue("@username", username);
                    conn.Open();
                    datareader = cmd.ExecuteReader();


                    while (datareader.Read())
                    {
                        userId = datareader["ID"].ToInt32OrDefault();
                        break;
                    }

                    datareader.Close();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    if (datareader != null)
                        datareader.Close();
                    else throw ex;
                    if (conn != null)
                        conn.Close();
                    else throw ex;
                    throw ex;
                }
            }

            return userId;
        }
        public void Deactivate(int userid)
        {
            ActiveToggle(userid, false);
        }
        public void Activate(int userid)
        {
            ActiveToggle(userid, true);
        }
        private void ActiveToggle (int userid, bool isActive)
        {
            using (SqlConnection conn = new SqlConnection(GlobalSettings.DateStores.AcmePortal))
            {
                
                SqlCommand cmd = new SqlCommand("UserActiveToggle", conn) { CommandType = System.Data.CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@userid", userid);
                cmd.Parameters.AddWithValue("@active", isActive);
                conn.Open();
                using (SqlDataReader datarow = cmd.ExecuteReader())
                {
                    while (datarow.Read())
                    {
                        try
                        {
                            var success = datarow["result"].ToBooleanOrDefault();
                            break;
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Acme.Methods.Delete" + ex.Message);
                        }
                    }
                    conn.Close();
                }
            }
        }
        public Acme.Models.User GetUserById(int userid = 0)
        {

            Acme.Models.User user = null;

            using (SqlConnection conn = new SqlConnection(GlobalSettings.DateStores.AcmePortal))
            {
                SqlCommand cmd = new SqlCommand("spUserGet", conn) { CommandType = System.Data.CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@userid", userid);
                conn.Open();
                using (SqlDataReader datarow = cmd.ExecuteReader())
                {
                    while (datarow.Read())
                    {
                        try
                        {
                            user = Acme.Methods.UserMapper.FromDB(datarow);
                            var extAppService = new Methods.ExternalApplicationService();
                            user.ExternalApplications = extAppService.ForUser(user.UserID);
                            
                            break;
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Acme.Methods.GetUserById" + ex.Message);
                        }
                    }
                    conn.Close();
                }
            }

            var mapper = new Data.Maps.UserMap();
            user = mapper.FillExternalApplications(user, user.ExternalApplications);

            return user;

        }
        public List<Acme.Models.User> GetList(DataTypes.UserType usertype = 0)
        {
            // Fetch Owners from Escapia API
            // Move to ajax call somewhere else
            var srvUser = new Services.Vendors.Escapia.EscapiaOwner();
            srvUser.UpdateOwnersFromEscapia();
            // -------------------------------
            

            List<Acme.Models.User> listOfUsers = new List<Acme.Models.User>();
            using (SqlConnection conn = new SqlConnection(GlobalSettings.DateStores.AcmePortal))
            {
                SqlCommand cmd = new SqlCommand("spUsersGet", conn) { CommandType = System.Data.CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@usertype", usertype.ToInt32OrDefault());
                conn.Open();
                using (var datarow = cmd.ExecuteReader())
                {
                    try
                    {
                        while (datarow.Read())
                        {
                                var user = Acme.Methods.UserMapper.FromDB(datarow);
                            
                                listOfUsers.Add(user);
                        }
                        conn.Close();
                    }
                    catch (Exception ex) { }
                }
            }

            return listOfUsers.OrderBy(x => x.FirstName).ToList();
        }
        public List<ViewModels.Common.OptionPair> GetUserTypes()
        {
            List<ViewModels.Common.OptionPair> items = new List<ViewModels.Common.OptionPair>();

            foreach (var t in Enum.GetValues(typeof(DataTypes.UserType)))
            {
                ViewModels.Common.OptionPair item = new ViewModels.Common.OptionPair();
                item.value = t.ToInt32OrDefault().ToStringOrDefault();
                item.name = ((DataTypes.UserType)t).ToStringOrDefault();
                items.Add(item);
            }

            return items.OrderBy(x => x.name).ToList();
        }
        private User GetUserByGuid(Guid id)
        {
            int UserID = GetId(id);
            return GetUserById(UserID);
        }

        #region Commands

        public Models.User Update(Acme.Models.UserUpdate data)
        {

            Models.UserDB model = new UserDB();

            model.ID = data.userid;
            model.LastName = data.lastname.ToStringOrDefault().Trim();
            model.FirstName = data.firstname.ToStringOrDefault().Trim();
            model.Email = data.email.ToStringOrDefault().Trim().ToLower();
            
            var securePassword = Security.Legacy.MD5Hash(data.password);
            model.Password = securePassword;
            model.PasswordSalt = securePassword;

            model.UserTypeID = data.usertypeid.ToInt32OrDefault();
            model.IPAddress = data.ipaddress;
            model.Modified = DateTime.Now;

            var repo = new Data.Repositories.UserRepository();

            repo.Update(model);

            ExternalApplicationService externalApplication = new ExternalApplicationService();
            externalApplication.UpdateUser(GlobalSettings.ExternalApplication.BambooHR,model.ID, 1, data.bambooid.ToStringOrDefault());


            var user = GetUserById(model.ID);
            return user;

        }

        internal Models.UserDB AddUser(Acme.Models.UserUpdate data)
        {
            var userMap = new Data.Maps.UserMap();
            var user = userMap.FromForm(data);
            return AddUser(user);
        }

        internal Models.UserDB AddOwnerUserEscapia(Models.ExternalApplication.Escapia.Owner escapiaUser)
        {
            if (escapiaUser.IsNull() || escapiaUser.nativePMSID.IsNullOrEmpty())
                return null;

            var userMap = new UserMap();
            Models.UserDB user = userMap.FromEscapia(escapiaUser);
            
            var newuser = AddUser(user);
            
            if (newuser.IsNull())
                return null;
            
            ExternalApplicationService appService = new ExternalApplicationService();
            appService.UpdateUser(GlobalSettings.ExternalApplication.Escapia, newuser.ID, 1, escapiaUser.nativePMSID);
            
            return newuser;
        }

        private Models.UserDB AddUser(Models.UserDB data)
        {
            if (data.FirstName.IsNullOrEmpty() || data.LastName.IsNullOrEmpty() || data.Email.IsNullOrEmpty())
                return null;

            var repo = new Data.Repositories.UserRepository();
            var existinguser = repo.GetUserByEmail(data.Email);

            if (existinguser.IsNotNull())
                return null;

            data.Modified = DateTime.Now;
            data.Guid = Guid.NewGuid();
            
            var newuser = repo.Add(data);
            return newuser;
        }
        
        public bool UpdatePassword(Acme.Models.UserUpdate data)
        {

            if (data.password.IsNullOrEmpty() || data.userid == 0)
                return false;
            
            var securePassword = Security.Legacy.MD5Hash(data.password);

            using (SqlConnection conn = new SqlConnection(GlobalSettings.DateStores.AcmePortal))
            {
                SqlCommand cmd = new SqlCommand("UserUpdatePassword", conn) { CommandType = System.Data.CommandType.StoredProcedure};
                cmd.Parameters.AddWithValue("@userid", data.userid);
                cmd.Parameters.AddWithValue("@password", securePassword);
                cmd.Parameters.AddWithValue("@ipaddress", data.ipaddress);

                conn.Open();

                try
                {
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception)
                {
                    return false;

                }
                finally
                {
                    conn.Close();
                }
            }
        }

        #endregion

        public List<Acme.Models.User> GetUsersFromEmails(List<string> emails)
        {
            var repo = new Data.Repositories.UserRepository();
            return repo.GetUsersFromEmails(emails).Select(u => UserMapper.ToModel(u)).ToList();
        }
    }

    public class UserMapper {

        public static Acme.Models.User FromDB(SqlDataReader datarow) {

            var user = new Acme.Models.User();

            user.UserID = datarow["ID"].ToInt32OrDefault();
            user.Guid = datarow["Guid"].ToGuidOrDefault();
            user.LastName = datarow["LastName"].ToStringOrDefault();
            user.FirstName = datarow["FirstName"].ToStringOrDefault();
            user.Email = datarow["Email"].ToStringOrDefault();
            user.IsActive = datarow["IsActive"].ToBooleanOrDefault();
            user.UserType = (DataTypes.UserType)(datarow["UserTypeID"].ToInt32OrDefault());
            user.UserTypeID = datarow["UserTypeID"].ToInt32OrDefault();
            user.UserTypeName = datarow["UserTypeName"].ToStringOrDefault();

            // hardcoded temp work around
            //                  Jay                 Ryan                    Boris
            user.IsAdmin = (user.UserID == 1602 || user.UserID == 1603 || user.UserID == 1642);

            var extAppService = new Methods.ExternalApplicationService();
            user.ExternalApplications = extAppService.ForUser(user.UserID);
            

            if (user.ExternalApplications.BambooHR.IsNotNull())
            {
                user.PhotoUrl = user.ExternalApplications.BambooHR.PhotoUrl;
                user.Department = user.ExternalApplications.BambooHR.Department;
                user.JobTitle = user.ExternalApplications.BambooHR.JobTitle;
            }


            if (user.PhotoUrl.IsNullOrEmpty())
                user.PhotoUrl = GlobalSettings.Images.DefaultUser;
            return user;
           
        }

        public static Acme.Models.User ToModel(Models.UserDB userDB)
        {
            return new Acme.Models.User()
            {
                FirstName = userDB.FirstName,
                LastName = userDB.LastName,
                Email = userDB.Email
            };
        }
    }
        
}