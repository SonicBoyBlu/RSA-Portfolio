namespace API.Models.SearchFilers
{
    /// <summary>
    /// Search filter for comments.
    /// </summary>    
    public class CommentSearchFilter
    {
        /// <summary>
        /// Default: only show active comments. When set to 'false' ONLY 'inactive' comments will be provided.
        /// </summary>
        public bool? ShowInactive { get; set; } = null;

        /// <summary>
        /// Search comments associated by StockID.
        /// </summary>
       public int? StockID { get; set; } = null;

        /*
        /// <summary>
        /// Search comments by ticker symbol.
        /// </summary>
        public string? Symbol { get; set; } = null;

        /// <summary>
        /// Search comments by company name.
        /// </summary>
        public string? CompanyName { get; set; } = null; 
        */

        /// <summary>
        /// Search comments by content keyword.
        /// </summary>
        public string? Content { get; set; } = null;
    }
}
