using Global.Data.SQL;

namespace Global.Candidates
{
    public partial class Accounts
    {
        public static string GenerateTempPassword()
        {
            string strPwd = string.Empty;
            try
            {
                //string strPwdchar = "abcdefghijklmnopqrstuvwxyz0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                string strPwdchar = "0123456789";
                Random rnd = new Random();
                for (int i = 0; i < 6; i++)
                {
                    int iRandom = rnd.Next(0, strPwdchar.Length - 1);
                    strPwd += strPwdchar.Substring(iRandom, 1);
                }
                return strPwd;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return strPwd;
        }
        public static bool UpdatePassword(int UserID, string Password, bool Notify = true)
        {
            List<QueryParameter> qp =
            [
                new() { Name = "@UserID", Value = UserID },
                new() { Name = "@Password", Value = Password },
            ];
            QueryParameters p = [.. qp];

            var userid = DbContext.SqlDataGetValue<int>("spPasswordUpdate", p);
            return userid > 0;

            /*
            if (Notify && User.DateLastLogin != DateTime.MinValue)
                NotifyUser(User.UserID, DataTypes.AccountNotificaitonType.UpdatePassword);
            //*/

            /*
            var set = false;
            if (!User.IsBanned)
            {
                try
                {

                    string query = string.Format(@"
                            Declare @userid int             = '{0}'                
                            Declare @passsword nvarchar(64) = '{1}'  

                            Update Users Set userPassword = @passsword, userIsActive = 1 Where userID = @userid  

                            -- turn back on deactivated contact details
                            update StudentContactInfo
                            set	contactIsActive = 1
                            where contactUserID = {0}
                            ", User.UserID, Password);

                    using (SqlConnection sqlConnect = new SqlConnection(ConnectionString))
                    {
                        SqlCommand sqlCommand = new SqlCommand(query, sqlConnect);
                        sqlConnect.Open();
                        sqlCommand.ExecuteNonQuery();
                        if (sqlConnect != null) sqlConnect.Close();
                    }

                    set = true;
                    if (Notify && User.DateLastLogin != DateTime.MinValue)
                        NotifyUser(User.UserID, DataTypes.AccountNotificaitonType.UpdatePassword);

                }
                catch (Exception ex) { set = false; }
            }
            return set;
            //*/
        }


        public static Models.RequestVerifyAccountRecovery VerifyAccountRecovery(int UserID, string Token)
        {
            var v = new Models.RequestVerifyAccountRecovery();
            List<QueryParameter> qp =
            [
                new() { Name = "@UserID", Value = UserID },
                new() { Name = "@Token", Value = Token }
            ];
            QueryParameters p = [.. qp];
            v = DbContext.SqlDataGetValue<Models.RequestVerifyAccountRecovery>("spLogAccountRecoveryAttempts", p);
            /*
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("spLogAccountRecoveryAttempts", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@userid", UserID);
                    cmd.Parameters.AddWithValue("@token", Token);
                    conn.Open();
                    SqlDataReader r = cmd.ExecuteReader();
                    while (r.Read())
                    {
                        v = new RequestVerifyAccountRecovery()
                        {
                            UserID = UserID,
                            Username = (string)r["Username"],
                            Token = (string)r["token"],
                            NumAttempts = (int)r["Attempts"],
                            DateLogged = (DateTime)r["datelogged"],
                            RecoveryCode = (int)r["RecoveryCode"]
                        };
                    }
                }
            }
            catch (Exception ex)
            {
            }
            //*/
            return v;
        }

        public static Models.User VerifyUserAccount(string Username)
        {
            Models.User u = new Models.User();

            QueryParameters p = new();
            p.Add(new QueryParameter() { Name = "@username", Value = Username });

            var users = DbContext.ConvertToList<Models.User>(DbContext.SqlDataSet("spUserVerify", p).Tables[0]);
            if (users != null)
            {
                u = users[0];
            }
            return u;
        }

    }
}