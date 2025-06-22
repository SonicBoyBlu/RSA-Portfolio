using Microsoft.AspNetCore.Mvc;
using API.Interfaces;
using API.Mappers;
using API.Models.DTO.Comments;
using API.Models.SearchFilers;

namespace API.Controllers
{
    /// <summary>General controller for Comment objects</summary>
    /// <remarks>Controller for Comments</remarks>
    /// <param name="commentRepository"></param>
    [Route("comments")]
    [ApiController]
    public class CommentController(ICommentRepository commentRepository, IStockRepository stocks) : ControllerBase
    {

        /// <summary>Get all Comments as specified by query parameters</summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        //[Route("~/comments/")]
        public async Task<IActionResult> GetAllAsync([FromQuery] CommentSearchFilter filter)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            var data = await commentRepository.GetAllAsync(filter);
            return Ok(data.Select(x => x.CommentModleToDto()));
        }

        /// <summary>Get a single Comment by ID</summary>
        /// <param name="commentId"></param>
        /// <returns></returns>
        [HttpGet("{commentId:int}")]
        public async Task<IActionResult> GetCommentById([FromRoute] int commentId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState); 
            
            var data = await commentRepository.GetByIdAsync(commentId);
            if (data == null) { return NotFound(); }
            return Ok(data.CommentModleToDto());
        }

        /// <summary>Create a new Comment</summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> CreateAsync(CommentCreateRequestDto comment)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await stocks.CheckExists(comment.StockID))
                return BadRequest("Stock ID does not exist.");

            var model = await commentRepository.CreateAsync(comment);
            return CreatedAtAction(nameof(GetCommentById), new { commentId = model.CommentID }, model.CommentModleToDto());
        }

        /// <summary>Update an existing Comment</summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateAsync([FromBody] CommentUpdateRequestDto comment)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var data = await commentRepository.UpdateAsync(comment);
            if (data == null) { return NotFound("Unable to update; cannot find this Comment."); }
            return Ok(data.CommentModleToDto());
        }

        /// <summary>Delete an existing Comment</summary>
        /// <param name="commentId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("x")]
        public async Task<IActionResult?> DeleteAsync([FromBody] int commentId)
        {
            if (!await commentRepository.CheckExists(commentId))
                return BadRequest("Comment ID does not exist.");
            var model = await commentRepository.DeleteAsync(commentId);
            if (model == null) return NotFound();
            return NoContent();
        }
    }
}