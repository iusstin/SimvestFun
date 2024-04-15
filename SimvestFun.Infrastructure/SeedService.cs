using SimvestFun.ApplicationCore.Entities;
using SimvestFun.ApplicationCore.Interfaces;

namespace SimvestFun.Infrastructure
{
    public static class SeedService
    {
        public static async Task SeedData(ISimvestFunContext context)
        {
            if (!context.Stocks.Any())
            {
                var initialDate = new DateTime(2022, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                context.Stocks.Add(new Stock { Index = 1, Id = "AAPL", Name = "Apple", CurrentPrice = 162.95M, Industry = "Consumer electronics, software and online services", PriceUpdatedOn = initialDate });
                context.Stocks.Add(new Stock { Index = 2, Id = "MSFT", Name = "Microsoft", CurrentPrice = 288.50M, Industry = "Multinational technology", PriceUpdatedOn = initialDate });
                context.Stocks.Add(new Stock { Index = 3, Id = "GOOG", Name = "Alphabet", CurrentPrice = 2677M, Industry = "Conglomerate", PriceUpdatedOn = initialDate });
                context.Stocks.Add(new Stock { Index = 4, Id = "AMZN", Name = "Amazon", CurrentPrice = 2786M, Industry = "E-commerce", PriceUpdatedOn = initialDate });
                context.Stocks.Add(new Stock { Index = 5, Id = "TSLA", Name = "Tesla", CurrentPrice = 858.97M, Industry = "Electric vehicle and clean energy", PriceUpdatedOn = initialDate });
                context.Stocks.Add(new Stock { Index = 6, Id = "BRK.B", Name = "Berkshire Hathaway", CurrentPrice = 488.24M, Industry = "Financial services", PriceUpdatedOn = initialDate });
                context.Stocks.Add(new Stock { Index = 7, Id = "NVDA", Name = "Nvidia", CurrentPrice = 230.14M, Industry = "Visual computing", PriceUpdatedOn = initialDate });
                context.Stocks.Add(new Stock { Index = 8, Id = "FB", Name = "Meta", CurrentPrice = 198.50M, Industry = "Information technology", PriceUpdatedOn = initialDate });
                context.Stocks.Add(new Stock { Index = 9, Id = "UNH", Name = "United Health", CurrentPrice = 485.57M, Industry = "Managed healthcare insurance", PriceUpdatedOn = initialDate });
                context.Stocks.Add(new Stock { Index = 10, Id = "JNJ", Name = "Johnson & Johnson", CurrentPrice = 169.36M, Industry = "Pharmaceutical Medical devices Consumer healthcare", PriceUpdatedOn = initialDate });
                context.Stocks.Add(new Stock { Index = 11, Id = "V", Name = "Visa", CurrentPrice = 199.76M, Industry = "Financial services", PriceUpdatedOn = initialDate });
                context.Stocks.Add(new Stock { Index = 12, Id = "JPM", Name = "JPMorgan Chase", CurrentPrice = 133.44M, Industry = "Financial services", PriceUpdatedOn = initialDate });
                context.Stocks.Add(new Stock { Index = 13, Id = "WMT", Name = "Walmart", CurrentPrice = 139.46M, Industry = "Retail", PriceUpdatedOn = initialDate });
                context.Stocks.Add(new Stock { Index = 14, Id = "PG", Name = "Procter & Gamble", CurrentPrice = 148.77M, Industry = "Consumer goods", PriceUpdatedOn = initialDate });
                context.Stocks.Add(new Stock { Index = 15, Id = "XOM", Name = "Exxon Mobil", CurrentPrice = 82.79M, Industry = "Oil and gas", PriceUpdatedOn = initialDate });
                context.Stocks.Add(new Stock { Index = 16, Id = "HD", Name = "Home Depot", CurrentPrice = 317.20M, Industry = "Retail", PriceUpdatedOn = initialDate });
                context.Stocks.Add(new Stock { Index = 17, Id = "BAC", Name = "Bank of America", CurrentPrice = 41.04M, Industry = "Financial services", PriceUpdatedOn = initialDate });
                context.Stocks.Add(new Stock { Index = 18, Id = "CVX", Name = "Chevron", CurrentPrice = 166.27M, Industry = "Oil and gas", PriceUpdatedOn = initialDate });
                context.Stocks.Add(new Stock { Index = 19, Id = "MA", Name = "Mastercard", CurrentPrice = 328.13M, Industry = "Payments", PriceUpdatedOn = initialDate });
                context.Stocks.Add(new Stock { Index = 20, Id = "PFE", Name = "Pfizer", CurrentPrice = 48.75M, Industry = "Pharmaceutical and biotechnology", PriceUpdatedOn = initialDate });

                await context.SaveChangesAsync();
            }
        }
    }
}
