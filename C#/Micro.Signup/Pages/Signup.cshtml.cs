using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Signups.Pages
{
    [ValidateAntiForgeryToken()]
    public class SignupModel : PageModel
    {
        public Global.Events.Models.SignupSheets.SignupSheetItem? form;
        public Global.Models.GeneralResponse? res;
        public List<Global.Resumes.Models.Major>? Majors;
        public void OnGet()
        {
            Majors = Global.Resumes.Majors.GetMajors();
        }
        public async Task OnPost(Global.Events.Models.SignupSheets.SignupSheetItem i)
        {
            await Global.Events.SignupSheets.AddRecipient(i);
        }

        /*
        public void OnPost() {
            helloWord = "Foo/Bar";
        }
        public void OnPost(Global.Events.Models.SignupSheets.SignupSheetItem i)
        {
            Global.Events.SignupSheets.AddRecipient(i);
        }
        public async Task OnPostAsync(Global.Events.Models.SignupSheets.SignupSheetItem i)
        {
            await Global.Events.SignupSheets.AddRecipient(i);
        }
        */
    }
}
