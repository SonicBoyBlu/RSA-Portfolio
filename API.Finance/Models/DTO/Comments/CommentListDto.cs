using System.ComponentModel.DataAnnotations;

namespace API.Models.DTO.Comments
{
    /// <summary>Comment DTO object from the Comment model</summary>
    public class CommentListResultDto
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

        /*
        /// <summary>
        /// Campnay Name associated with this comment
        /// </summary>
        public string? CompanyName { get; set; } = string.Empty;
        */

        /// <summary>Is Comment Active</summary>
        public bool IsActive { get; set; } = true;
    }
}