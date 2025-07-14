using Microsoft.Data.SqlClient;
using System.Data;

namespace Global.Candidates.Details
{
    public class Journal
    {
        public static Models.CandidateLogs GetJournal(int UserID, int TypeID, string View)
        {
            var journal = new Models.CandidateLogs();
            try
            {
                using (SqlConnection conn = new SqlConnection(Global.Settings.DataStores.DefaultConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("spGetSystemEventsCandidates", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@userID", UserID));
                    cmd.Parameters.Add(new SqlParameter("@typeID", TypeID == 0 ? DBNull.Value : (object)TypeID));
                    cmd.Parameters.Add(new SqlParameter("@view", string.IsNullOrEmpty(View) ? DBNull.Value : (object)View));

                    conn.Open();
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            var i = new Global.Candidates.Details.Models.CandidateEventItem()
                            {
                                SystemEventID = (int)r["SystemEventID"],
                                SystemEventTypeID = (int)r["SystemEventTypeID"],
                                SystemEventName = (string)r["SystemEventName"],
                                DateOccurred = r["OccuranceDate"] == DBNull.Value ? DateTime.MinValue : (DateTime)r["OccuranceDate"],

                                AppID = r["AppID"] == DBNull.Value ? 0 : (int)r["AppID"],
                                HireID = r["HireID"] == DBNull.Value ? 0 : (int)r["HireID"],
                                JobID = r["JobID"] == DBNull.Value ? 0 : (int)r["JobID"],
                                JobName = r["JobName"] == DBNull.Value ? "" : (string)r["JobName"],
                                CompanyID = r["CompanyID"] == DBNull.Value ? 0 : (int)r["CompanyID"],
                                CompanyName = r["CompanyName"] == DBNull.Value ? "" : (string)r["CompanyName"],

                                DateStart = r["DateStart"] == DBNull.Value ? DateTime.MinValue : (DateTime)r["DateStart"],
                                DateEnd = r["DateEnd"] == DBNull.Value ? DateTime.MinValue : (DateTime)r["DateEnd"],
                                //MarketID = Int32.Parse(r["MarketID"].ToString()),
                                //MarketName = (string)r["marketName"],
                                //Market = (DataTypes.Markets)(Int32.Parse(r["MarketID"].ToString())),
                                Market = r["Market"] == DBNull.Value ? "" : (string)r["Market"],
                                ReasonInactivated = r["InactReason"] == DBNull.Value ? "" : (string)r["InactReason"],
                                HourlyRate = r["HourlyPay"] == DBNull.Value ? 0 : decimal.Parse(r["HourlyPay"].ToString()),
                                TotalHours = r["TotalHrs"] == DBNull.Value ? 0 : decimal.Parse(r["TotalHrs"].ToString()),

                                //*
                                InterviewID = r["InterviewID"] == DBNull.Value ? 0 : (int)r["InterviewID"],
                                IsOrientated = r["IsOrientated"] == DBNull.Value ? false : (bool)r["IsOrientated"],
                                ScoreA = r["AScore"] == DBNull.Value ? "" : (string)r["AScore"],
                                ScoreP = r["CScore"] == DBNull.Value ? "" : (string)r["CScore"],
                                ScoreI = r["IScore"] == DBNull.Value ? "" : (string)r["IScore"],
                                Notes = r["Notes"] == DBNull.Value ? "" : (string)r["Notes"],
                                ActionTakenByName = r["ActionTakenByName"] == DBNull.Value ? "" : (string)r["ActionTakenByName"],
                                InterviewerName = r["InterviewerName"] == DBNull.Value ? "" : (string)r["InterviewerName"],
                                InterviewContactName = r["InterviewContactName"] == DBNull.Value ? "" : (string)r["InterviewContactName"],
                                DateInterviewScheduled = r["SchedInterviewDate"] == DBNull.Value ? DateTime.MinValue : (DateTime)r["SchedInterviewDate"],

                                DateDenied = r["DateDenied"] == DBNull.Value ? DateTime.MinValue : (DateTime)r["DateDenied"],
                                IsAppDenied = r["IsAppDenied"] == DBNull.Value ? false : (bool)r["IsAppDenied"],
                                IsFeedback = r["IsFeedback"] == DBNull.Value ? false : (bool)r["IsFeedback"],
                                IsActive = r["IsActive"] == DBNull.Value ? false : (bool)r["IsActive"],
                                VerificationID = r["VerificationID"] == DBNull.Value ? 0 : (int)r["VerificationID"],
                                VerificationItemID = r["VerificationItemID"] == DBNull.Value ? 0 : (int)r["VerificationItemID"],
                                VerificationName = r["VerificationName"] == DBNull.Value ? "" : (string)r["VerificationName"],
                                IsVerificationNormal = r["VerificationIsNormal"] == DBNull.Value ? false : (bool)r["VerificationIsNormal"],
                                RespondedBy = r["RespondedBy"] == DBNull.Value ? "" : (string)r["RespondedBy"],
                                ItemID = r["ItemID"] == DBNull.Value ? 0 : (int)r["ItemID"],
                                SweType = r["SweType"] == DBNull.Value ? 0 : (int)r["SweType"],
                                InterestLevel = r["InterestLevel"] == DBNull.Value ? "" : (string)r["InterestLevel"],

                                IsOPT = r["IsOPT"] == DBNull.Value ? false : (bool)r["IsOPT"],
                                DateOptExpires = r["DateOptExpires"] == DBNull.Value ? DateTime.MinValue : (DateTime)r["DateOptExpires"],
                                OptNotes = r["OptNotes"] == DBNull.Value ? "" : (string)r["OptNotes"],

                                IsOtherVisa = r["IsOtherVisa"] == DBNull.Value ? false : (bool)r["IsOtherVisa"],
                                DateOtherVisaExpires = r["DateOtherVisaExpires"] == DBNull.Value ? DateTime.MinValue : (DateTime)r["DateOtherVisaExpires"],
                                OtherVisaNotes = r["OtherVisaNotes"] == DBNull.Value ? "" : (string)r["OtherVisaNotes"],

                                IsIdVerified = r["IsIdVerified"] == DBNull.Value ? false : (bool)r["IsIdVerified"],
                                IdVerificationNotes = r["IdVerificationNotes"] == DBNull.Value ? "" : (string)r["IdVerificationNotes"],
                                //*/
                            };
                            journal.Add(i);
                        }
                    }
                    conn.Close();
                }

            }
            catch (Exception ex) { Console.WriteLine("CAUGHT ERROR: " + ex.Message); }

            return journal;
        }
    }
}
