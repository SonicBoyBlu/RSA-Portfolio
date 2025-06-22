using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace API.Models.DTO.Stocks
{
    /// <summary>
    /// Request object for updating an existing Stock entry
    /// </summary>
    public class StockUpdateRequestDto
    {
        /// <summary>
        /// ID of the Stock object to update
        /// </summary>
        [Required]
        public int StockID { get; set; }

        /// <summary>
        /// Exchange symbol of the Stock object to update
        /// </summary>        [Required]
        [Required]
        [MaxLength(10, ErrorMessage = "Symbol cannot exceed 10 characters")]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// Company Name of the Stock object to update
        /// </summary>        [Required]
        [MaxLength(280, ErrorMessage = "Company name cannot exceed 280 characters")]
        public string CompanyName { get; set; } = string.Empty;
        [Required]
        [Range(1, 1000000000)]
        public decimal Purchase { get; set; }
        [Required]
        [Range(0.001, 100)]
        public decimal LastDiv { get; set; }
        [Required]
        [MaxLength(10, ErrorMessage = "Industry label cannot exceed 10 characters")]
        public string Industry { get; set; } = string.Empty;
        [Range(1, 5000000000)]
        public long MarketCap { get; set; }
        public DateTime DateUpdated { get; set; } = DateTime.Now;
        [DefaultValue(true)]
        public bool IsActive { get; set; } = true;

    }
}