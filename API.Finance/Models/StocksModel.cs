using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class Stock
    {
        public int StockID { get; set; }
        public string Symbol { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal PricePurchase { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal LastDiv { get; set; }

        public string IndustryCategory { get; set; } = string.Empty;
        public long MarketCap { get; set; }
        /// <summary>List of associated Comments to this Stock</summary>
        public List<Comment> Comments { get; set; } = [];
        /// <summary>
        /// Date Stock entry was entered
        /// </summary>
        [Column(TypeName = "datetime2")]
        public DateTime DateCreated { get; set; } = DateTime.Now;
        /// <summary>Date Stock entry was last updated</summary>
        [Column(TypeName = "datetime2")] 
        public DateTime DateUpdated { get; set; } = DateTime.Now;
        /// <summary>Is the Stock Active</summary>
        [DefaultValue(true)]
        public bool IsActive { get; set; } = true;
    }
}