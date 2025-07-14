using Global.Data.SQL;
using Global.Location;

namespace Global.Events
{
    public class Get
    {
        //public static List<Models.Event> EventsList(Models.RequestEventsList req)
        public static List<Models.Event> EventsList()
        {
            var events = new List<Models.Event>();
            int span = 15;

            try
            {
                DateTime now = DateTime.Now;
                QueryParameters p = new();
                p.Add(new QueryParameter() { Name = "@IsActive", Value = true });
                p.Add(new QueryParameter() { Name = "@DateStart", Value = now.AddDays(-1* span) });
                p.Add(new QueryParameter() { Name = "@DateEnd", Value = now.AddDays(span) });
                p.Add(new QueryParameter() { Name = "@MarketID", Value = DBNull.Value });
                events = DbContext.ConvertToList<Models.Event>(DbContext.SqlDataSet("spGetEvents_V3", p).Tables[0]);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return events;
        }

        /*
        public static Models.Event Event(int EventID)
        {
            var e = new Models.Event();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();

                    string spName = "spGetEventInfo";
                    SqlCommand cmd = new SqlCommand(spName, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@EventID", EventID));
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            e = new Models.Event()
                            {
                                EventID = EventID,
                                DateStart = (DateTime)r["eventDateFrom"],
                                DateEnd = (DateTime)r["eventDateTo"],
                                Location = r["eventLocation"] == DBNull.Value ? "" : (string)r["eventLocation"],
                                SchoolID = r["schoolid"] == DBNull.Value ? 0 : int.Parse(r["schoolid"].ToString()),
                                Description = r["eventInformation"] == DBNull.Value ? "" : (string)r["eventInformation"],
                                MarketID = int.Parse(r["eventMarketID"].ToString()),
                                MarketName = (string)r["market_name"],
                                Market = (DataTypes.Markets)(Int32.Parse(r["eventMarketID"].ToString())),
                                SignupSheetID = r["signupsheet_id"] == DBNull.Value ? 0 : (int)r["signupsheet_id"],
                                SignupSheetCount = (int)r["ss_count"],
                                SignupSheetPreRegCount = (int)r["ss_prereg"],
                                AdditionalNotes = r["eventAdditionalNotes"] == DBNull.Value ? "" : (string)r["eventAdditionalNotes"],
                                EventBuilding = r["eventBuilding"] == DBNull.Value ? "" : (string)r["eventBuilding"],
                                EventRoom = r["eventRoom"] == DBNull.Value ? "" : (string)r["eventRoom"],
                                NameShort = r["eventNameShort"] == DBNull.Value ? "" : (string)r["eventNameShort"],
                                Name = r["eventNameLong"] == DBNull.Value ? "" : (string)r["eventNameLong"],
                                Url = r["eventUrl"] == DBNull.Value ? "" : (string)r["eventUrl"],
                                Address = new Location.Address()
                                {
                                    Address1 = r["eventAddress1"] == DBNull.Value ? "" : (string)r["eventAddress1"],
                                    Address2 = r["eventAddress2"] == DBNull.Value ? "" : (string)r["eventAddress2"],
                                    City = r["eventAddressCity"] == DBNull.Value ? "" : (string)r["eventAddressCity"],
                                    State = r["StateAbbrev"] == DBNull.Value ? "" : (string)r["StateAbbrev"],
                                    StateID = r["stateid"] == DBNull.Value ? 1 : (int)r["stateID"],
                                    Zip = r["eventAddressZip"] == DBNull.Value ? "" : (string)r["eventAddressZip"]
                                },
                                isPublic = r["eventVisible"] == DBNull.Value ? false : (bool)r["eventVisible"],
                                IsActive = (bool)r["IsActive"],
                                IsOnline = (bool)r["IsOnline"]
                            };
                        }
                    }
                }

            }
            catch (Exception ex) { Console.WriteLine("CAUGHT ERROR: " + ex.Message); }

            return e;
        }
        */
    }
}
