using Microsoft.EntityFrameworkCore;
using SimvestFun.ApplicationCore.Entities;
using SimvestFun.ApplicationCore.Interfaces;

namespace SimvestFun.ApplicationCore.Services
{
    public class StockService : IStockService
    {
        private readonly ISimvestFunContext _context;
        private readonly IFinnhubAPIService _finnhubAPIService;
        private readonly IStockPriceService _stockPriceService;

        public StockService(ISimvestFunContext context,
                            IFinnhubAPIService finnhubAPIService,
                            IStockPriceService stockPriceService)
        {
            _context = context;
            _finnhubAPIService = finnhubAPIService;
            _stockPriceService = stockPriceService;
        }
        public async Task<List<Stock>> GetAllStocksAsync()
        {
            return await _context.Stocks.OrderBy(s => s.Index).ToListAsync();
        }

        public async Task<bool> UpdateStockPricesAsync(bool dayChanged)
        {
            var stocks = _context.Stocks.ToList();

            foreach (var stock in stocks)
            {
                var oldPrice = stock.CurrentPrice;
                stock.CurrentPrice = await _finnhubAPIService.GetStockPriceAsync(stock.Id);

                if (oldPrice == stock.CurrentPrice && stock.Index == 1)
                    return false;

                if (dayChanged)
                {
                    if (stock.YesterdayPrice == 0M)
                    {
                        stock.YesterdayPrice = await _stockPriceService.GetYesterdayPriceAsync(stock.Id);
                        if (stock.YesterdayPrice == 0M)
                            stock.YesterdayPrice = stock.CurrentPrice;
                    }
                    else
                        stock.YesterdayPrice = oldPrice;
                }  
                
                stock.PricePercentChange = (stock.CurrentPrice - stock.YesterdayPrice) / stock.YesterdayPrice * 100;
                stock.PriceUpdatedOn = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Stock> GetStockWithPricesAsync(string id)
        {
            var stock = _context.Stocks
                .Include(s => s.StockPrices)
                .FirstOrDefault(s => s.Id == id);

            stock.StockPrices = stock.StockPrices
                .OrderByDescending(sp => sp.TimeStamp)
                .Take(19)
                .OrderBy(sp => sp.TimeStamp)
                .ToList();

            stock.StockPrices.Add(
                new StockPrice()
                {
                    StockId = id,
                    Price = stock.CurrentPrice,
                    TimeStamp = DateTime.UtcNow,
                });

            return stock;
        }

        public async Task<bool> CheckTwelveHourChange()
        {
            var fistStock = await _context.Stocks.FirstOrDefaultAsync();

            return (DateTime.UtcNow - fistStock.PriceUpdatedOn).TotalHours >= 12;
        }
    }
}
