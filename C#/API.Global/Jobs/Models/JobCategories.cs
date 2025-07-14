namespace Global.Jobs.Models
{
    public class CategoryList : List<Category>
    {
        public CategoryList()
        {
        }
    }

    public class CategoryStats
    {
        public CategoryStats()
        {
            Categories = new List<Category>();
            CategoryPayScale = new List<CategoryPayScale>();
            OverallPayScale = new List<OverallPayScale>();
        }
        public List<Category> Categories { get; set; }
        public List<CategoryPayScale> CategoryPayScale { get; set; }
        public List<OverallPayScale> OverallPayScale { get; set; }
    }

    public class Category
    {
        public Category()
        {
            IsActive = true;
            Description = string.Empty;
            IsSelected = false;
        }
        public int CategoryID { get; set; }
        public required string CategoryName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public int TotalJobs { get; set; }
        public bool IsSelected { get; set; }
    }


    public class CategoryPayScale
    {
        public int CategoryID { get; set; }
        public required string CategoryName { get; set; }
        public required double PayRangeUpper { get; set; }
        public required double PayRangeLower { get; set; }
        public required double PayAvgUpper { get; set; }
        public required double PayAvgLower { get; set; }
        public required string PayType { get; set; }
    }

    public class OverallPayScale
    {
        public required double PayRangeUpper { get; set; }
        public required double PayRangeLower { get; set; }
        public required double PayAvgUpper { get; set; }
        public required double PayAvgLower { get; set; }
        public required string PayType { get; set; }
    }
}