using Microsoft.EntityFrameworkCore;
using SimvestFun.ApplicationCore.Entities;
using SimvestFun.ApplicationCore.Interfaces;

namespace SimvestFun.ApplicationCore.Services
{
    public class StockPriceService : IStockPriceService
    {
        private readonly ISimvestFunContext _context;
        public StockPriceService(ISimvestFunContext context)
        {
            _context = context;
        }

        public async Task SaveYesterdayStockPricesAsync()
        {
            var stocks = await _context.Stocks.ToListAsync();
            foreach (var stock in stocks)
            {
                var stockPrice = new StockPrice()
                {
                    Price = stock.YesterdayPrice,
                    StockId = stock.Id,
                    TimeStamp = DateTime.UtcNow.Date.AddDays(-1)
                };
                _context.StockPrices.Add(stockPrice);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<decimal> GetYesterdayPriceAsync(string stockId)
        {
            var yesterdayStockPrice  = await _context.StockPrices.Where(sp => sp.StockId == stockId &&
                                                    sp.TimeStamp.Date.CompareTo(DateTime.UtcNow.Date) == -1)
                                             .OrderBy(sp => sp.TimeStamp)
                                             .LastOrDefaultAsync();

            if(yesterdayStockPrice == null)
                return 0;

            return yesterdayStockPrice.Price;
        }
    }
}
