using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;

namespace Global.Data.SQL
{
    public class DbContext
    {
        public static SqlConnection SqlConnectionDefault
        {
            get
            {
                return new SqlConnection(Global.Settings.DataStores.DefaultConnectionString);
            }
        }
        internal static SqlCommand SqlCommandDefault(string StoredProcedureName)
        {
            using (SqlConnection sqlConnection = SqlConnectionDefault)
            {
                sqlConnection.Open();
                return new SqlCommand(StoredProcedureName, sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

            }
        }

        public static DataSet SqlDataSet(string StoredProcedureName, QueryParameters? sqlParameters)
        {
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection sqlConnection = SqlConnectionDefault)
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand(StoredProcedureName, sqlConnection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    if (sqlParameters != null)
                        cmd.Parameters.AddRange(ConvertQueryParameters(sqlParameters).ToArray());
                    //cmd.Parameters.AddRange(sqlParameters.ToArray());

                    SqlDataAdapter sda = new SqlDataAdapter(cmd);

                    sda.Fill(ds);
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
            }
            return ds;
        }

        public static DataSet SqlDataSet(string StoredProcedureName)
        {
            return SqlDataSet(StoredProcedureName, null);
        }

        public static T SqlDataGetValue<T>(string StoredProcedureName, QueryParameters? sqlParameters)
        {
            T item = default;

            try
            {
                using (SqlConnection sqlConnection = SqlConnectionDefault)
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand(StoredProcedureName, sqlConnection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    if (sqlParameters != null)
                        cmd.Parameters.AddRange(ConvertQueryParameters(sqlParameters).ToArray());

                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            item = (T)r[0];
                        }
                    }
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
            return item;
        }

        public static T SqlDataGetObject<T>(string StoredProcedureName, QueryParameters? sqlParameters)
        {
            var ds = SqlDataSet(StoredProcedureName, sqlParameters);
            var list = ConvertToList<T>(ds.Tables[0]);
            return (T)list[0];
        }

        public static List<T> ConvertToList<T>(DataTable dt)
        {
            var columnNames = dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName.ToLower()).ToList();
            var properties = typeof(T).GetProperties();
            return dt.AsEnumerable().Select(row =>
            {
                var objT = Activator.CreateInstance<T>();
                foreach (var pro in properties)
                {
                    if (columnNames.Contains(pro.Name.ToLower()))
                    {
                        try
                        {
                            //*
                            if (row[pro.Name] == DBNull.Value)
                            {
                                switch (pro.PropertyType.Name.ToLower())
                                {
                                    case "string": pro.SetValue(objT, string.Empty); break;
                                    case "datetime": pro.SetValue(objT, DateTime.MinValue); break;
                                }
                                Console.WriteLine(pro.Name);
                            }
                            else
                                //*/
                                pro.SetValue(objT, row[pro.Name]);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
                return objT;
            }).ToList();
        }

        public static object ConvertJsonObject<T>(string item)
        {
            var json = JsonConvert.DeserializeObject<T>(item);
            return json;
        }

        internal static List<SqlParameter> ConvertQueryParameters(QueryParameters queryParameters)
        {
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            foreach (var p in queryParameters)
            {
                sqlParameters.Add(new SqlParameter(p.Name, p.Value));
            }

            return sqlParameters;
        }
    }
}
