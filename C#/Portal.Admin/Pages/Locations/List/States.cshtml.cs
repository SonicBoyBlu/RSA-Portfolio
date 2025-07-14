using Global.Location;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Admin.Pages.Locations.List
{
    public class StatesModel : PageModel
    {
        internal int _defaultStateId = 1;
        public int StateID { get; set; }

        public List<State>? States { get; set; }
        public MarketDetail? Market { get; set; }
        public void OnGet(int? id)
        {
            int _id = _defaultStateId;
            int.TryParse(id.ToString(), out _id);
            StateID = _id;
            States = Global.Location.States.GetStates() ?? [];
            Market = Global.Location.MarketDetail.GetMarketByStateID(_id);
        }
    }
}
