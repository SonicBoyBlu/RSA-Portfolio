using API.Models;
using API.Models.DTO.Comments;

namespace API.Mappers
{
    /// <summary>Model/DTO mappings</summary>
    public static class CommentMapper
    {
        /// <summary>Comment list result</summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static CommentListResultDto CommentModleToDto(this Comment model)
        {
            return new CommentListResultDto
            {
                CommentID = model.CommentID,
                Title = model.Title, 
                Content = model.Content, 
                DateCreated = model.DateCreated, 
                DateUpdated = model.DateUpdated,
                IsActive = model.IsActive,
                StockID = model.StockID
            };
        }
        public static Comment CommentCreateDtoToModel(this CommentCreateRequestDto dto)
        {
            return new Comment
            {
                Title = dto.Title,
                Content = dto.Content,
                StockID = dto.StockID,
                CommentID = dto.CommentID// != 0 ? (int)dto.CommentID : null,
            };
        }
        public static Comment CommentUpdateDtoToModel(this CommentUpdateRequestDto dto, int stockID)
        {
            return new Comment
            {
                Title = dto.Title,
                Content = dto.Content,
                StockID = stockID,
                IsActive = dto.IsActive
            };
        }
    }
}
