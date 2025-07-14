using System.ComponentModel.DataAnnotations;

namespace API.Models.DTO.Comments
{

    /// <summary>Update a Comment</summary>
    public class CommentUpdateRequestDto
    {
        /// <summary>
        /// ID of the Comment context
        /// </summary>
        [Required]
        public int CommentID { get; set; }


        /// <summary>Updated title for this Comment.</summary>
        [Required]
        [MinLength(5, ErrorMessage = "Title is required and must be at least 5 characters")]
        [MaxLength(250, ErrorMessage = "Title cannot be over 280 characters")]
        public string Title { get; set; } = string.Empty;

        /// <summary>Updated body for this Comment.</summary>
        [Required]
        [MinLength(5, ErrorMessage = "Content is required and must be at least 5 characters")]
        [MaxLength(4000, ErrorMessage = "Content cannot exceed 4,000 characters")]
        public string Content { get; set; } = string.Empty;

        /// <summary>Is current conmnent active?</summary>
        public bool IsActive { get; set; } = true;
    }
}