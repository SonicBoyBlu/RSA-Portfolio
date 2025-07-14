using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Jobs.Pages
{
    public class EmployersModel : PageModel
    {
        [BindProperty]
        public string? CompanyName { get; set; }
        [BindProperty]
        public string? YourName { get; set; }
        [BindProperty]
        public string? Title { get; set; }
        [BindProperty]
        public string? Phone { get; set; }
        [BindProperty]
        public string? Email { get; set; }
        [BindProperty]
        public string? Details { get; set; }
        [BindProperty]
        public bool IsSubmitted { get; set; }

        public void OnGet()
        {
            Console.WriteLine("ERI");
        }

        public void OnPostSubmit()
        {
            _ = YourName;
            _ = Title;
            _ = CompanyName;
            _ = Phone;
            _ = Email;
            _ = Details;
            _ = IsSubmitted;
        }
    }
}
