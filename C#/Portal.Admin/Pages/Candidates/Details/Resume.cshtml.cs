using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Admin.Pages.Candidates.Details
{
    public class ResumeModel : PageModel
    {
        Global.Candidates.Details.Models.Overview Me { get; set; }
        public void OnGet(int UserID)
        {
            Me = Global.Candidates.Details.Overview.GetOverview(UserID);
            Me.UserID = UserID;
        }
    }
}
