using Global.Data.SQL;

namespace Global.Candidates
{
    public partial class Accounts
    {
        // Administrative/Auto-Create
        public static Global.Models.GeneralResponse Register (Models.SetRegistration registration)
        {
            var j = new Global.Models.GeneralResponse();
            var r = registration;

            // Check if user account/email already exists
            var verify = VerifyUserAccount(r.Email);
            if (verify != null)
            {
                j = new Global.Models.GeneralResponse()
                {
                    IsSuccess = false,
                    Status = "warning",
                    Message = "This username already exists, and belongs to<br/> " + verify.FirstName + " " + verify.LastName
                };
            }
            else
            {
                r.Password = r.Email;
                // register our user
                j = CreateNewUser(r);
                if (j.IsSuccess)
                {
                    // Get new user's info
                    var u = VerifyUserAccount(r.Email);
                    j.Message = (u.FirstName + " " + u.LastName + " was successfully created.<br/>UserID: " + u.UserID);
                    var tmp = GenerateTempPassword();

                    var v = VerifyAccountRecovery(u.UserID, tmp);
                    tmp = v.Token;
                    UpdatePassword(u.UserID, tmp);

                    //string arUrl = "https://www.campuspoint.com/AccountRecovery/verify/" + HttpContext.Current.Server.HtmlEncode(u.UserID + "/" + tmp);


                    // Steps in the admin registration process

                    // SMS
                    /*
                    if (!string.IsNullOrEmpty(u.PhonePrimary))
                    {
                        var sender = GlobalSettings.Communication.SMS.AccountRecoverySender.SEA;
                        if (u.Market == DataTypes.Markets.PDX) sender = GlobalSettings.Communication.SMS.AccountRecoverySender.PDX;

                        var sms = new CampusPoint.Common.Models.Communication.SMS.Message()
                        {
                            AuthReq = sender,
                            recipient = u.PhonePrimary,
                            userID = u.UserID,
                            body = string.Format("{2}, Here is your CampusPoint activation code: {0}\n\n {1}", tmp, arUrl, (u.FirstName + " " + u.LastName).Trim())
                        };
                        CampusPoint.Communication.SMS.SendSms(sms);
                    }
                    //*/

                    // Parse resume data
                    /*
                    if (r.ResumeData != null)
                    {
                        r.ResumeData.UserID = u.UserID;
                        var resume = new CampusPoint.Models.Resumes.HireAbility.ResumeParserResponse()
                        {
                            Results = new List<CampusPoint.Models.Resumes.HireAbility.Results>()
                        };
                        resume.Results.Add(new CampusPoint.Models.Resumes.HireAbility.Results());
                        resume.Results[0].HireAbilityJSONResults.Add(r.ResumeData);
                        ResumeParsing.HireAbility.PopulateResumeNodes(resume);

                        if (r.FileInfo != null)
                        {
                            var fi = r.FileInfo;
                            var ha = new CampusPoint.Models.Resumes.HireAbility.ResumeParserResponse();
                            var haRes = new CampusPoint.Models.Resumes.HireAbility.Results();
                            haRes.HireAbilityJSONResults.Add(r.ResumeData);
                            ha.Results.Add(haRes);

                            ResumeParsing.HireAbility.SaveParsedResume(new CampusPoint.Models.Resumes.HireAbility.ResumeParserRequest()
                            {
                                UserID = u.UserID,
                                file_name = fi.file_name,
                                file_type = fi.file_type,
                                base64string = fi.base64string,
                                json = JsonConvert.SerializeObject(ha),
                                CreateAccount = false,
                                Notes = fi.Notes,
                                JobID = r.JobID
                            });
                        }

                    }
                    //*/


                    // Apply for job if JobID is provided
                    /*
                    if (r.JobID > 0)
                    {
                        // Apply user for job;
                        CampusPoint.Jobs.Applications.SubmitApplication(new CampusPoint.Models.JobApplications.ApplicationSaveSubmission()
                        {
                            UserID = u.UserID,
                            PhoneNum = u.PhonePrimary,
                            JobID = r.JobID
                        });
                    }
                    //*/

                    // Send welcome  email
                    /*
                    string emailBody = string.Format(@"
                        <p>{0} {1},</p>
                        <p>Welcome to CampusPoint!  You are receiving this email because you signed-up at a career fair, emailed your resume to us, or applied for a job.  For your convenience, we’ve created an account on your behalf.  Your login is your email address.</p>
                        <p>Activation Code: <strong>{2}</strong></p>
                        <p>Please finish activating your account by <a href='{4}' target='_blank'>clicking here</a>.</p>
                        <p>There are some great features to your CampusPoint account:
                        <ul>
                            <li><strong>App Tracker for Real-Time Job Tracking:</strong>  You will see the status of each of your job applications.</li>
                            <li><strong>Apply for Jobs:</strong>  We add new jobs every day, so visit us often.  You can apply for as many jobs as you’d like!</li>
                            <li><strong>See Company Names:</strong>  Once you login, you will see the company that is associated with every job listing at CampusPoint.</li>
                        </ul>
                        </p>
                        <p>I am looking forward to working with you!</p>
                        "
                        , u.FirstName, u.LastName, tmp, GlobalSettings.CompanyDetails.CompanyName,
                        //context.Request.Url.AbsoluteUri.Replace("/settemp","") + "/Verified/Email/" + context.Server.HtmlEncode(u.Username.Replace(".","~") +"|"+tmp));
                        //context.Request.Url.AbsolutePath + "/AccountRecovery/vtoken/" + context.Server.HtmlEncode(u.UserID +"/"+tmp));
                        //"https://www.campuspoint.com/AccountRecovery/verify/" + context.Server.HtmlEncode(u.UserID +"/"+tmp));
                        arUrl
                    );

                    var env = new CampusPoint.Common.Models.Communication.Mail.Envelope()
                    {
                        EmailFromRecipients = new System.Net.Mail.MailAddress("activations@campuspoint.com", GlobalSettings.CompanyDetails.CompanyName + " Account Activation"),
                        EmailSubject = GlobalSettings.CompanyDetails.CompanyName + " Account Activation",
                        EmailBody = emailBody,
                        IsSignatureIncluded = true,
                        IsHeaderIncluded = true
                    };
                    env.EmailToRecipients.Add(new System.Net.Mail.MailAddress(u.Email, (u.FirstName + " " + u.LastName)));
                    //env.EmailBccRecipients.Add(new System.Net.Mail.MailAddress("ryan@campuspoint.com", "Ryan S. Alexander"));
                    Mail.Send(env);
                    //*/
                }
            }
            return j;
        }

        // Standard candidate account creation
        public static Global.Models.GeneralResponse CreateNewUser(Models.SetRegistration s)
        {
            var j = new Global.Models.GeneralResponse();
            var u = VerifyUserAccount(s.Email);
            if (u == null)
            {
                try
                {
                    var otherIDs = new List<int> { 0, 5, 24, 50, 79, 102, 125, 167 };
                    bool isOther = otherIDs.Contains(s.ReferralID);

                    List<QueryParameter> qp =
                    [
                        new() { Name = "@FirstName", Value = s.FirstName },
                        new() { Name = "@MiddleName", Value = string.IsNullOrEmpty(s.MiddleName) ? DBNull.Value : s.MiddleName },
                        new() { Name = "@LastName", Value = s.LastName },

                        new() { Name = "@Phone", Value = s.Phone },
                        new() { Name = "@Email", Value = s.Email },
                        new() { Name = "@Password", Value = s.Password },
                        new() { Name = "@MarketID", Value = s.MarketID },

                        new() { Name = "@ReferralID", Value = s.ReferralID == 0 ? DBNull.Value : s.ReferralID },
                        new() { Name = "@ReferralName", Value = string.IsNullOrEmpty(s.ReferralName) || isOther ? DBNull.Value : s.ReferralName },

                        new() { Name = "@AccessToken", Value = string.IsNullOrEmpty(s.FacebookAccessToken) ? DBNull.Value : s.FacebookAccessToken },
                        new() { Name = "@IsAgree", Value = true },

                        new() { Name = "@Notes", Value = string.IsNullOrEmpty(s.Notes) ? DBNull.Value : s.Notes },
                        new() { Name = "@UserPreID", Value = s.UserPreID == 0 ? DBNull.Value : s.UserPreID },
                        new() { Name = "@CpUserID", Value = s.CpUserID == 0 ? DBNull.Value : s.CpUserID },
                    ];
                    QueryParameters p = [.. qp];

                    /*
                    cmd.Parameters.AddWithValue("@firstname", s.FirstName);
                    cmd.Parameters.AddWithValue("@middlename", string.IsNullOrEmpty(s.MiddleName) ? DBNull.Value : (object)s.MiddleName);
                    cmd.Parameters.AddWithValue("@lastname", s.LastName);
                    cmd.Parameters.AddWithValue("@phone", s.Phone);
                    cmd.Parameters.AddWithValue("@email", s.Email);
                    cmd.Parameters.AddWithValue("@password", s.Password);
                    cmd.Parameters.AddWithValue("@marketid", s.MarketID);
                    cmd.Parameters.AddWithValue("@referralid", s.ReferralID == 0 ? DBNull.Value : (object)s.ReferralID);
                    cmd.Parameters.AddWithValue("@referralname", (string.IsNullOrEmpty(s.ReferralName) || isOther ? DBNull.Value : (object)s.ReferralName));
                    //cmd.Parameters.AddWithValue("@referralname", (string.IsNullOrEmpty(s.ReferralName) ? DBNull.Value : (object)s.ReferralName));
                    cmd.Parameters.AddWithValue("@accesstoken", (string.IsNullOrEmpty(s.FacebookAccessToken) ? DBNull.Value : (object)s.FacebookAccessToken));
                    cmd.Parameters.AddWithValue("@isagree", Identity.Current.UserType == DataTypes.UserType.Guest ? true : false);

                    cmd.Parameters.AddWithValue("@notes", string.IsNullOrEmpty(s.Notes) ? DBNull.Value : (object)s.Notes);
                    cmd.Parameters.AddWithValue("@userpreid", s.UserPreID == 0 ? DBNull.Value : (object)s.UserPreID);
                    cmd.Parameters.AddWithValue("@cpuserid", s.CpUserID == 0 ? DBNull.Value : (object)s.CpUserID);
                    //*/

                    var userid = DbContext.SqlDataGetValue<int>("spRegistrationV3", p);

                    j = new Global.Models.GeneralResponse()
                    {
                        Data = userid,
                        IsSuccess = true,
                        Status = "success",
                        Message = 
                            s.CpUserID > 0 ?
                            "You have successfully registered.  You will now be logged in." 
                            :
                            string.Format("User successfully created. (ID:{0} Name: {1} {2})", userid, s.FirstName, s.LastName)
                    };
                    /*
                    using (SqlConnection conn = new SqlConnection(GlobalSettings.DataStores.DefaultConnectionString))
                    {
                        SqlCommand cmd = new SqlCommand("spRegistrationV3", conn)
                        {
                            CommandType = CommandType.StoredProcedure
                        };
                        cmd.Parameters.AddWithValue("@firstname", s.FirstName);
                        cmd.Parameters.AddWithValue("@middlename", string.IsNullOrEmpty(s.MiddleName) ? DBNull.Value : (object)s.MiddleName);
                        cmd.Parameters.AddWithValue("@lastname", s.LastName);
                        cmd.Parameters.AddWithValue("@phone", s.Phone);
                        cmd.Parameters.AddWithValue("@email", s.Email);
                        cmd.Parameters.AddWithValue("@password", s.Password);
                        cmd.Parameters.AddWithValue("@marketid", s.MarketID);
                        cmd.Parameters.AddWithValue("@referralid", s.ReferralID == 0 ? DBNull.Value : (object)s.ReferralID);
                        cmd.Parameters.AddWithValue("@referralname", (string.IsNullOrEmpty(s.ReferralName) || isOther ? DBNull.Value : (object)s.ReferralName));
                        //cmd.Parameters.AddWithValue("@referralname", (string.IsNullOrEmpty(s.ReferralName) ? DBNull.Value : (object)s.ReferralName));
                        cmd.Parameters.AddWithValue("@accesstoken", (string.IsNullOrEmpty(s.FacebookAccessToken) ? DBNull.Value : (object)s.FacebookAccessToken));
                        cmd.Parameters.AddWithValue("@isagree", Identity.Current.UserType == DataTypes.UserType.Guest ? true : false);

                        cmd.Parameters.AddWithValue("@notes", string.IsNullOrEmpty(s.Notes) ? DBNull.Value : (object)s.Notes);
                        cmd.Parameters.AddWithValue("@userpreid", s.UserPreID == 0 ? DBNull.Value : (object)s.UserPreID);
                        cmd.Parameters.AddWithValue("@cpuserid", s.CpUserID == 0 ? DBNull.Value : (object)s.CpUserID);

                        conn.Open();
                        SqlDataReader r = cmd.ExecuteReader();
                        while (r.Read())
                        {
                            j.Data = (int)r["UserID"];
                            j.Status = "success";
                            j.IsSuccess = true;
                            j.Message = "You have successfully registered.  You will now be logged in.";
                        }
                        r.Close();
                        r.Close();
                    }
                    //*/
                }
                catch (Exception ex) { 
                    j.Message = ex.Message; 
                }
            }
            else
            {
                j.Status = "warning";
                j.Message = "An account with this email is already registered.";
            }
            return j;
        }
    }
}
