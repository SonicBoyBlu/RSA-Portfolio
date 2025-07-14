using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Admin.Pages.Candidates.Details
{
    public class AccountModel : PageModel
    {
        public int UserID { get; set; }
        public Global.Candidates.Details.Models.CandidateLogs Journal { get; set; }
        public Global.Candidates.Details.Models.Overview Me { get; set; }
        public void OnGet(int userID)
        {
            UserID = userID;
            Me = Global.Candidates.Details.Overview.GetOverview(UserID);
            Journal = Global.Candidates.Details.Journal.GetJournal(UserID, 0, "account");
        }
    }
}