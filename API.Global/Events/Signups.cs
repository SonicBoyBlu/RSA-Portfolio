using Global.Data.SQL;

namespace Global.Events
{
    public class SignupSheets
    {
        public static async Task<Global.Models.GeneralResponse> AddRecipient(Models.SignupSheets.SignupSheetItem i)
        {
            var res = new Global.Models.GeneralResponse();
            await Task.Run(() =>
            {

                List<QueryParameter> qp =
                [
                    // Event Info
                    //new() { Name = "@EventID", Value = i.EventID == 0 ? DBNull.Value : i.EventID},
                    new() { Name = "@SignupSheetID", Value = i.SignupSheetID},
                    new() { Name = "@PreID", Value = i.UserPreID == 0 ? DBNull.Value : i.UserPreID },
                    new() { Name = "@UserID", Value = i.UserID == 0 ? DBNull.Value : i.UserID },

                    // Contact Info
                    new() { Name = "@FirstName", Value = i.FirstName },
                    new() { Name = "@LastName", Value = i.LastName },
                    new() { Name = "@phone", Value = i.Phone },
                    new() { Name = "@email", Value = i.Email },

                    // Grad Info
                    // TODO: Grad dates need to be parsed from full date
                    new() { Name = "@MajorName", Value = i.MajorName },
                    new() { Name = "@MajorID", Value = i.MajorID == 0 ? DBNull.Value : i.MajorID },
                    new() { Name = "@GradMonth", Value = i.GradMonth == 0 ? DBNull.Value : i.GradMonth },
                    new() { Name = "@GradYear", Value = i.GradYear == 0 ? DBNull.Value : i.GradYear },

                    // Admin Info
                    new() { Name = "@CpUserID", Value = i.CpUserID },
                    new() { Name = "@notes", Value = i.Notes },
                    new() { Name = "@ascore", Value = i.ScoreA == 0 ? DBNull.Value : i.ScoreA },
                    new() { Name = "@pscore", Value = i.ScoreP == 0 ? DBNull.Value : i.ScoreP },

                ];
                QueryParameters p = [.. qp];
                try
                {
                    var preReg = DbContext.SqlDataGetObject<Candidates.Models.PreRegDetails>("spSignupSheet_UserPreRegistrant_InsertUpdate_v2", p);
                    res = new Global.Models.GeneralResponse()
                    {
                        Data = preReg,
                        IsSuccess = true,
                        Message = string.Format("{0} {1} successfully signed up!", i.FirstName, i.LastName),
                        Status = "success"
                    };

                    // TODO: Send Email for event signup confirm

                    if (i.IsAccountCreate)
                    {
                        // NOTE: Create user account
                        res = Candidates.Accounts.Register(new Candidates.Models.SetRegistration()
                        {
                            UserPreID = preReg.PreRegID,
                            CpUserID = i.CpUserID,
                            FirstName = i.FirstName,
                            LastName = i.LastName,
                            Email = i.Email,
                            //Password = string.Empty,
                            Phone = i.Phone,

                            ReferralID = preReg.ReferralTypeID,
                            ReferralName = preReg.EventName,
                            MarketID = (int)preReg.Market,

                            Notes = i.Notes
                        });
                    }
                }
                catch (Exception ex)
                {
                    res = new Global.Models.GeneralResponse()
                    {
                        Data = ex,
                        IsSuccess = false,
                        Message = ex.Message,
                        Status = "error"
                    };
                }
            });
            return res;
        }
    }
}
