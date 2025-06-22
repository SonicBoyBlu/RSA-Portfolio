using System.ComponentModel.DataAnnotations;

namespace API.Models.DTO.Stocks
{
    /// <summary>
    /// Stock entry delete request
    /// </summary>
    public class StockDeleteRequesttDto
    {
        /// <summary>Stock ID</summary>
        [Required]
        public int StockID { get; set; }

        /// <summary>UserID executing delete function</summary>
        [Required]
        public int UserID { get; set; }

        /// <summary>Deate stammp of executed delete function</summary>
        [Required]
        public DateTime DateUpdated { get; set; } = DateTime.Now;
    }
}