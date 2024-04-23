using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Admin.Pages.Locations
{
    public class IndexModel : PageModel
    {
        public int StateID { get; set; }
        public void OnGet(int? id)
        {
        }
    }
}
