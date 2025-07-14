using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Acme.Models;

namespace Acme.Methods
{
    public class Properties
    {
        public static List<Property> GetProperties(string status = null, int EscapiaID = 0)
        {
            var props = new List<Property>();
            using (var conn = new SqlConnection(GlobalSettings.DateStores.Breezeway))
            {
                var cmd = new SqlCommand("spPropertyListGet", conn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@escapiaID", EscapiaID == 0 ? DBNull.Value : (object)EscapiaID);
                conn.Open();
                using(var r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        props.Add(new Property()
                        {
                            BwPropertyID = (int)r["BwPropertyID"],
                            EscapiaID = (int)r["EscapiaID"],
                            UnitCode = (string)r["UnitCode"],
                            PropertyName = (string)r["PropertyName"],
                            Address1 = (string)r["Address1"],
                            Address2 = (string)r["Address2"],
                            City = (string)r["City"],
                            State = (string)r["State"],
                            ZipCode = (string)r["ZipCode"],
                            UnitID = (int)r["UnitID"]
                        });
                    }
                }
            }
            return props;
        }

    }
}