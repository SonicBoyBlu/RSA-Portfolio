using Global.Jobs;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Jobs.Pages
{
    public class JobsModel : PageModel
    {
        public List<Global.Jobs.Models.Job>? Jobs { get; set; }
        public void OnGet(int? id)
        {
            int _id = 0;
            int.TryParse(id.ToString(), out _id);
            if (_id > 0)
            {
                Jobs = new List<Global.Jobs.Models.Job>();
                var job = Get.Job(_id, 0);
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
