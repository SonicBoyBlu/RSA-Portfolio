using Microsoft.AspNetCore.Mvc.RazorPages;
using Global.Jobs;

namespace Jobs.Pages
{
    public class CategoriesModel : PageModel
    {
        public Global.Jobs.Models.CategoryStats Stats {
            get
            {
                var stats = new Global.Jobs.Models.CategoryStats();
                try
                {
                    stats = Get.Categories.CategoryStats();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return stats;
            }
        }
        public void OnGet()
        {
        }
    }
}