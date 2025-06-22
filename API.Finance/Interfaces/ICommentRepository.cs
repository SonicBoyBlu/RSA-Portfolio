using API.Models;
using API.Models.DTO.Comments;
using API.Models.SearchFilers;

namespace API.Interfaces
{
    /// <summary>
    ///  Comments repository
    /// </summary>
    public interface ICommentRepository
    {
        /// <summary>
        /// Get all Comments as spicified by filter(s)
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<List<Comment>> GetAllAsync(CommentSearchFilter filter);

        /// <summary>
        /// Get a single Comment by ID
        /// </summary>
        /// <param name="commentID"></param>
        /// <returns></returns>
        Task<Comment?> GetByIdAsync (int commentID);

        /// <summary>
        /// Create a new Comment
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        Task<Comment> CreateAsync (CommentCreateRequestDto comment);

        /// <summary>
        /// Update an existing Comment
        /// </summary>
        /// <param name="commentID"></param>
        /// <param name="comment"></param>
        /// <returns></returns>
        Task<Comment?> UpdateAsync(CommentUpdateRequestDto comment);
        //Task<Comment?> UpdateAsync(int commentID, Comment comment);
        
        
        /// <summary>
        /// Delete a specific Comment by ID
        /// </summary>
        /// <param name="commentID"></param>
        /// <returns></returns>
        Task<Comment?> DeleteAsync (int commentID);

        /// <summary>
        /// Check to see if the comment exists by ID
        /// </summary>
        /// <param name="commentID"></param>
        /// <returns></returns>
        Task<bool> CheckExists(int commentID);
    }
}
