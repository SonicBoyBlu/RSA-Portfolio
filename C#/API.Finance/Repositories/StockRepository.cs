using API.Data;
using API.Interfaces;
using API.Models;
using API.Models.DTO.Stocks;
using API.Models.SearchFilers;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class StockRepository(ApplicationDBContext dbContext) : IStockRepository
    {
        public async Task<List<Stock>> GetAllAsync(StockSeachFilter filter)
        {
            /*
            var data = dbContext.Stocks
                .Include(x => x.Comments)
                .Where(x => x.IsActive == (filter.ShowInactive != true))
                .AsQueryable();
            */
            var data = dbContext.Stocks
                .Include(x => x.Comments)
                .AsQueryable();

            data = data.Where(x => x.IsActive == (filter.ShowInactive != true));
                

            if(!string.IsNullOrWhiteSpace(filter.Symbol))
                data = data.Where(x => x.Symbol.Contains(filter.Symbol));

            if(!string.IsNullOrWhiteSpace(filter.CompanyName))
                data = data.Where(x => x.CompanyName.Contains(filter.CompanyName));

            if (!string.IsNullOrWhiteSpace(filter.SortBy))
            {
                if (filter.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
                {
                    data = filter.IsDescending ?
                        data.OrderByDescending(x => x.Symbol) :
                        data.OrderBy(x => x.Symbol);
                }
                /*
                switch (filter.SortBy.ToLower())
                {
                    case "symbol": data.or break;
                }
                */
            }



            /*
            if (filter.ShowComments == true)
                data.Include(x => x.Comments);
            */

            int skip = (filter.PageNumber - 1) * filter.PageSize;
            return await 
                data
                    .Skip(skip)
                    .Take(filter.PageSize)
                    .ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            //return await _dbContext.Stocks.FindAsync(id);
            return await
                dbContext.Stocks
                .Include(x => x.Comments)
                .FirstOrDefaultAsync(i => i.StockID == id);
        }

        public async Task<Stock> CreateAsync(Stock model)
        {
            await dbContext.Stocks.AddAsync(model);
            await dbContext.SaveChangesAsync();
            return model;
        }
        public async Task<Stock?> UpdateAsync(StockUpdateRequestDto dto)
        {
            var model = await dbContext.Stocks.FindAsync(dto.StockID);
            if (model == null) { return null; }

            model.Symbol = dto.Symbol;
            model.CompanyName = dto.CompanyName;
            model.PricePurchase = dto.Purchase;
            model.LastDiv = dto.LastDiv;
            model.IndustryCategory = dto.Industry;
            model.MarketCap = dto.MarketCap;
            model.DateUpdated = DateTime.Now;
            model.IsActive = dto.IsActive;

            await dbContext.SaveChangesAsync();
            return model;
        }


        public async Task<Stock?> DeleteAsync(int id)
        {
            var model = await dbContext.Stocks.FirstOrDefaultAsync(x => x.StockID == id);
            if (model == null) { return null; }

            // This will likely be an IsActive: false
            // If delete from DB is permitted, all child dependant records from keyed tables will need to be implimented.
            //_dbContext.Stocks.Remove(model);
            model.IsActive = false;
            model.DateUpdated = DateTime.Now;
            await dbContext.SaveChangesAsync();
            return model;
        }

        public async Task<bool> CheckExists (int id)
        {
            return await dbContext.Stocks.AnyAsync(x => x.StockID == id);
        }
    }
}
