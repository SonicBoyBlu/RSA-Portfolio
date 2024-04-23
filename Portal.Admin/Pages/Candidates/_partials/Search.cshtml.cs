using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Admin.Pages.Candidates
{
    public class IndexModel : PageModel
    {
        public Global.Search.Models.KeywordSearch KeywordSearch { get; set; }
        public void OnGet()
        {
            KeywordSearch = new Global.Search.Models.KeywordSearch();
        }
    }
}
