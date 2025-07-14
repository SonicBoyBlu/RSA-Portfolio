using API.Models;
using API.Models.DTO.Stocks;
using API.Models.SearchFilers;

namespace API.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync(StockSeachFilter filter);
        Task<Stock?> GetByIdAsync(int stockId);
        Task<Stock> CreateAsync(Stock model);
        Task<Stock?> UpdateAsync(StockUpdateRequestDto dto);
        Task<Stock?> DeleteAsync(int stockId);
        Task<bool> CheckExists(int stockId);
    }
}
