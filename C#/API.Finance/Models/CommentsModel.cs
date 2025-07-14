namespace API.Models
{
    public class Comment
    {
        /// <summary>Unique ID for this Comment</summary>
        public int CommentID { get; set; }
        /// <summary>Title for this Comment</summary>
        public string Title { get; set; } = string.Empty;
        /// <summary>Content body of this Comment</summary>
        public string Content { get; set; } = string.Empty;
        /// <summary>Date Comment was created</summary>
        public DateTime DateCreated { get; set; } = DateTime.Now;
        /// <summary>Date Comment was updated</summary>
        public DateTime DateUpdated { get; set; } = DateTime.Now;
        /// <summary>ID of the Stock associated with this Comment</summary>
        public int? StockID { get; set; }
        /// <summary>Stock details for the associated Comment</summary>
        public Stock? Stock { get; set; }
        /// <summary>Is Comment Active</summary>
        public bool IsActive { get; set; } = true;
    }
}