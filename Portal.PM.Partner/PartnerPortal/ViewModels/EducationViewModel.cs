using System.Collections.Generic;

namespace Acme.ViewModels
{
    public class KnowledgeBaseViewModel
    {
        public KnowledgeBaseViewModel()
        {
            Lessons = new List<Models.KnowledgeBaseItem>();
            Categories = new Dictionary<int, string>();
        }
        public List<Models.KnowledgeBaseItem> Lessons { get; set; }
        public Dictionary<int, string> Categories { get; set; }
    }
}

namespace Acme.Models
{
    public class KnowledgeBaseCollection : List<KnowledgeBaseItem>
    {
    };
    public class KnowledgeBaseItem
    {
        public int KbID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Thumbnail { get; set; }
        public string Video { get; set; }
        public int CategoryID { get; set; }
        public string Category { get; set; }
    };

    public class KnowledgeBaseCategoryCount
    {
        public int CategoryID { get; set; }
        public string Title { get; set; }
        public int Total { get; set; }
    }
}