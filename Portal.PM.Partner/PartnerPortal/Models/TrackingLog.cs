using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Acme.Models
{
    public class TrackingLog
    {       
        private string _Memo { get; set; }
        private int _UserID { get; set; }
        private int _RecordID { get; set; }
        private string _RecordTable { get; set; }
        private string _IPAddress { get; set; }

        public TrackingLog()
        {
           _IPAddress = HttpContext.Current != null ? HttpContext.Current.Request.UserHostAddress : "";
        }

        public void Add(string memo, int userID, int recordID, string recordtable)
        {
            _Memo = memo.ToStringOrDefault();
            _UserID = userID;
            _RecordID = recordID;
            _RecordTable = recordtable.ToStringOrDefault();

            using (SqlConnection conn = new SqlConnection(GlobalSettings.DateStores.AcmePortal))
            {
                conn.Open();
                string sql = "INSERT INTO TrackingLog (Memo,UserID,RecordID,RecordTable,IPAddress) VALUES(@Memo,@UserID,@RecordID,@RecordTable,@IPAddress)";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Memo", _Memo);
                    cmd.Parameters.AddWithValue("@UserID", _UserID);
                    cmd.Parameters.AddWithValue("@RecordID", _RecordID);
                    cmd.Parameters.AddWithValue("@RecordTable", _RecordTable);
                    cmd.Parameters.AddWithValue("@IPAddress", _IPAddress);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }
        }

    }
}