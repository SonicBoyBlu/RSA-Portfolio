using System.ComponentModel.DataAnnotations;

namespace API.Models.DTO.Comments
{

    /// <summary>Create a new Comment</summary>
    public class CommentCreateRequestDto
    {
         /// <summary>
         /// ID of the Comment context
         /// </summary>
        public int CommentID { get; set; }

        /// <summary>
        /// StockID of the related comment.
        /// </summary>
        [Required]
        public int StockID { get; set; }

        /// <summary>Title for this Comment</summary>
        [Required]
        [MinLength(5, ErrorMessage = "Title is required and must be at least 5 characters")]
        [MaxLength(280, ErrorMessage = "Title cannot be over 280 characters")]
        public string Title { get; set; } = string.Empty;
        
        /// <summary>Content body of this Comment</summary>
        [Required]
        [MinLength(5, ErrorMessage = "Content is required and must be at least 5 characters")]
        [MaxLength(4000, ErrorMessage = "Content cannot exceed 4,000 characters")]
        public string Content { get; set; } = string.Empty;
    }
}