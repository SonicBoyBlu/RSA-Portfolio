using Microsoft.AspNetCore.Mvc.RazorPages;
using Global.Jobs.Models;

namespace Jobs.Pages
{
    public class IndexModel : PageModel
    {
        public required List<Job> Jobs { get; set; }
        public void OnGet(int? id)
        {
            Jobs = Jobs ?? new List<Job>();
            int _id = 0;
            int.TryParse(id.ToString(), out _id);
            if (_id > 0)
            {
                Jobs = new List<Global.Jobs.Models.Job>();
                var job = Global.Jobs.Get.Job(_id, 0);
                Jobs.Add(job);
            }
            else
            {
                Jobs = Global.Jobs.Get.Jobs(_id);
                //Jobs.Where(j => j.JobID == id).ToList();
            }
        }
    }
}
