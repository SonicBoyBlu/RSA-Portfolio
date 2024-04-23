using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Admin.Pages.Candidates.Details
{
    public class JournalModel : PageModel
    {
        public int UserID { get; set; }
        public Global.Candidates.Details.Models.CandidateLogs Journal { get; set; }

        public void OnGet(int userID)
        {
            UserID = userID;
            Journal = Global.Candidates.Details.Journal.GetJournal(UserID, 0, "all");
        }
    }
}
