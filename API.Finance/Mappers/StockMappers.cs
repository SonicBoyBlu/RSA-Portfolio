using API.Models;
using API.Models.DTO.Stocks;

namespace API.Mappers
{
    public static class StocksMapper
    {
        public static StockListResultDto StockModelToDto(this Stock model)
        {
            return new StockListResultDto
            {
                StockID = model.StockID,
                Symbol = model.Symbol,
                CompanyName = model.CompanyName,
                Purchase = model.PricePurchase,
                LastDiv = model.LastDiv,
                Industry = model.IndustryCategory,
                MarketCap = model.MarketCap,
                Comments = model.Comments.Select(x => x.CommentModleToDto()).ToList(),
                CommentCount = model.Comments.Count, 
                DateCreated = model.DateCreated, 
                DateUpdated = model.DateUpdated,
                IsActive = model.IsActive
            };
        }

        public static Stock FromStockDtoToCreateRequest(this StockCreateRequestDto model)
        {
            return new Stock
            {
                Symbol = model.Symbol,
                CompanyName = model.CompanyName,
                PricePurchase = model.Purchase,
                LastDiv = model.LastDiv,
                IndustryCategory = model.Industry,
                MarketCap = model.MarketCap,
                DateCreated = model.DateCreated, 
                DateUpdated = model.DateUpdated,
                IsActive = model.IsActive            };
        }
    }
}