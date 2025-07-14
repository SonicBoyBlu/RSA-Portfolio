using Global.Data.SQL;
namespace Global.Admin.Users
{
    public class Get
    {
        public static List<Models.User> Users(Models.User user)
        {
            var users = new List<Models.User>();
            QueryParameters p = new();
            p.Add(new QueryParameter() { Name = "@userid", Value = DBNull.Value });
            p.Add(new QueryParameter() { Name = "@isactive", Value = true });
            p.Add(new QueryParameter() { Name = "@marketid", Value = DBNull.Value });
            //p.Add(new QueryParameter() { Name = "@departmentid", Value = 5 });
            p.Add(new QueryParameter() { Name = "@username", Value = DBNull.Value });

            users = DbContext.ConvertToList<Models.User>(DbContext.SqlDataSet("spGetCpMainUsers", p).Tables[0]);
            return users;
        }

    }
}
