using System.ComponentModel.DataAnnotations;

namespace API.Models.DTO.Comments
{

    /// <summary>Create a new Comment</summary>
    public class CommentDeleteRequestDto
    {
         /// <summary>
         /// ID of the Comment context
         /// </summary>
         [Required]
        public int CommentID { get; set; }

        /// <summary>UserID executing delete function</summary>
        [Required]
        public int UserID { get; set; }

        /// <summary>Deate stammp of executed delete function</summary>
        [Required]
        public DateTime DateUpdated { get; set; } = DateTime.Now;


    }
}