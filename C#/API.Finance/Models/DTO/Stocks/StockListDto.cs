using API.Models.DTO.Comments;
using System.ComponentModel;

namespace API.Models.DTO.Stocks
{
    /// <summary>
    /// List of Stock entities
    /// </summary>
    public class StockListResultDto
    {
        public int StockID { get; set; }
        public string Symbol { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public decimal Purchase { get; set; }
        public decimal LastDiv { get; set; }
        public string Industry { get; set; } = string.Empty;
        public long MarketCap { get; set; }
        public List<CommentListResultDto> Comments { get; set; }
        public int CommentCount { get; set; }
        //[DefaultValue(DateTime.Now)]
        public DateTime DateCreated { get; set; } //= DateTime.Now;
        public DateTime DateUpdated { get; set; } //= DateTime.Now;
        [DefaultValue(true)]
        public bool IsActive { get; set; } = true;
    }
}