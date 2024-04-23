using Global.Data.SQL;
using Microsoft.Data.SqlClient;
//using Global.Resumes.Models;
namespace Global.Resumes
{
    public static class Majors
    {
        public static List<Models.Major> GetMajors()
        {
            List<Models.Major> m = DbContext.ConvertToList<Models.Major>(DbContext.SqlDataSet("spGetMajors").Tables[0]);
            return m;
        }
    }
}
