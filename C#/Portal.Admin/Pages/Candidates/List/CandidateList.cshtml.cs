using Microsoft.AspNetCore.Mvc.RazorPages;


namespace Admin.Pages.Candidates.List
{
    public class IndexModel : PageModel
    {
        public List<Global.Users.Models.User> Users { get; set; }
        //public List<Global.Location.MarketDetail> Markets { get; set; }
        public void OnGet(Global.Search.Models.KeywordSearch s)
        {
            Users = 
                Global.Candidates.Search.Basic(s ?? new Global.Search.Models.KeywordSearch()
                {

                });
            //Markets = Global.Location.MarketDetail.GetMarketByStateID(1);
        }
    }
}
