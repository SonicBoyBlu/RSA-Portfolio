using API.Interfaces;
using API.Mappers;
using API.Models.DTO.Stocks;
using API.Models.SearchFilers;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/stocks")]
    [ApiController]
    public class StockController(IStockRepository stockRepo) : ControllerBase
    {

        /// <summary>
        /// Get all stocks as specified by query parameters
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        //[Route("~/stocks/{showInactive:bool?}")]
        public async Task<IActionResult> GetAll([FromQuery] StockSeachFilter filter)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stocks = await stockRepo.GetAllAsync(filter);
            return Ok(stocks.Select(s => s.StockModelToDto()));
        }

        /// <summary>
        /// Get a specific stock by its ID
        /// </summary>
        /// <param name="stockId"></param>
        /// <returns></returns>
        [HttpGet("{stockId:int}")]
        public async Task<IActionResult> GetById([FromRoute] int stockId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stock = await stockRepo.GetByIdAsync(stockId);
            if (stock == null) { return NotFound(); }
            return Ok(stock);
        }

        /// <summary>
        /// Create a new definition for a stock
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddItem([FromBody] StockCreateRequestDto stock)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var model = stock.FromStockDtoToCreateRequest();
            await stockRepo.CreateAsync(model);
            return CreatedAtAction(nameof(GetById), new { id = model.StockID }, model.StockModelToDto());
        }

        /// <summary>
        /// Update the values for a specific stock
        /// </summary>
        /// <param name="stock"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Update([FromBody] StockUpdateRequestDto stock)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var model = await stockRepo.UpdateAsync(stock);
            if (model == null) return NotFound();
            return Ok(model.StockModelToDto());

        }

        /// <summary>
        /// Delete a single specific stock by ID
        /// </summary>
        /// <param name="stockId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("x")]
        public async Task<IActionResult> Delete([FromBody] int stockId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var model = await stockRepo.DeleteAsync(stockId);
            if (model == null) return NotFound();
            //return Ok();
            return NoContent();
        }
    }
}