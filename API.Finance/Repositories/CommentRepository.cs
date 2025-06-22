using API.Data;
using API.Interfaces;
using API.Mappers;
using API.Models;
using API.Models.DTO.Comments;
using API.Models.SearchFilers;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    /// <summary>
    /// Comments Repository
    /// </summary>
    public class CommentRepository(ApplicationDBContext dbContext) : ICommentRepository
    {
        public async Task<List<Comment>> GetAllAsync(CommentSearchFilter filter)
        {
            var data = dbContext.Comments
                            .Where(x => x.IsActive == (filter.ShowInactive != true))
                            .AsQueryable();

            // TODO: reverse lookups
            /*
            if (!string.IsNullOrEmpty(filter.Symbol))
                data = data.Where(x => x.Symbol.Contains(filter.Symbol));

            if (!string.IsNullOrEmpty(filter.CompanyName))
                data = data.Where(x => x.CompanyName.Contains(filter.CompanyName));
            */


            if (!string.IsNullOrEmpty(filter.Content))
                data = data.Where(x => x.Content.Contains(filter.Content));            
            
            if (filter.StockID > 0)
                data = data.Where(x => x.StockID == filter.StockID);

            return await
                data.ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int commentId)
        {
            return await dbContext.Comments.FindAsync(commentId);
        }
        public async Task<Comment> CreateAsync(CommentCreateRequestDto dto)
        {
            var model = CommentMapper.CommentCreateDtoToModel(dto);
            await dbContext.Comments.AddAsync(model);
            await dbContext.SaveChangesAsync();            
            return model;
        }
        public async Task<Comment?> UpdateAsync(CommentUpdateRequestDto dto)
        {
            //var model = await dbContext.Comments.FirstOrDefaultAsync(x => x.CommentID == commentId);
            var model = await dbContext.Comments.FindAsync(dto.CommentID);
            if (model == null) return null;

            model.Title = dto.Title;
            model.Content = dto.Content;
            model.IsActive = dto.IsActive;
            model.DateUpdated = DateTime.Now;
            await dbContext.SaveChangesAsync();
            return model;
        }
        public async Task<Comment?> DeleteAsync(int commentId)
        {
            var model = await dbContext.Comments.FirstOrDefaultAsync(x => x.CommentID == commentId);
            if (model == null) { return null; }

            // This will likely be an IsActive: false
            // If delete from DB is permitted, all child dependant records from keyed tables will need to be implimented.
            //_dbContext.Comments.Remove(model);
            model.IsActive = false;
            model.DateUpdated = DateTime.Now;
            await dbContext.SaveChangesAsync();
            return model;
        }

        public async Task<bool> CheckExists(int commentId)
        {
            return await dbContext.Comments.AnyAsync(x => x.CommentID == commentId);
        }

    }
}
