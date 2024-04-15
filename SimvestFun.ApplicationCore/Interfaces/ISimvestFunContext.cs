using Microsoft.EntityFrameworkCore;
using SimvestFun.ApplicationCore.Entities;

namespace SimvestFun.ApplicationCore.Interfaces
{
    public interface ISimvestFunContext
    {
        public DbSet<Stock> Stocks { get; }
        public DbSet<ApplicationUser> Users { get; }
        public DbSet<UserStock> UserStocks { get; }
        public DbSet<StockPrice> StockPrices { get; }
        public DbSet<PortfolioValue> PortfolioValues { get; }
        public DbSet<UserAction> UserActions { get; }
        public DbSet<Follow> Follows { get; }
        public DbSet<Setting> Settings { get; }
        public Task<int> SaveChangesAsync();
    }
}
