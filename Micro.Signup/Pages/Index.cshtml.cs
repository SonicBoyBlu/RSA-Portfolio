using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Signups.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public List<Global.Events.Models.Event> Events = [];
        public List<Global.Admin.Users.Models.User> Admins = [];

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            Events = Global.Events.Get.EventsList();
            Admins = Global.Admin.Users.Get.Users(new Global.Admin.Users.Models.User() { /*DepartmentID = 5*/ });
        }
    }
}
