using Microsoft.EntityFrameworkCore;
using SimvestFun.ApplicationCore.Entities;
using Microsoft.Extensions.Configuration;
using SimvestFun.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace SimvestFun.Infrastructure
{
    public class SimvestFunContext : IdentityDbContext<ApplicationUser>, ISimvestFunContext
    {
        private readonly IConfiguration _config;
        public SimvestFunContext(DbContextOptions<SimvestFunContext> options, IConfiguration config) : base(options)
        {
            _config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(_config.GetConnectionString("SimvestFunContext"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new UserConfiguration().Configure(modelBuilder.Entity<ApplicationUser>());
            new FollowConfiguration().Configure(modelBuilder.Entity<Follow>());
            new SettingConfiguration().Configure(modelBuilder.Entity<Setting>());
        }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        public DbSet<Stock> Stocks { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<UserStock> UserStocks { get; set; }
        public DbSet<PortfolioValue> PortfolioValues { get; set; }
        public DbSet<StockPrice> StockPrices { get; set; }
        public DbSet<UserAction> UserActions { get; set; }
        public DbSet<Follow> Follows { get; set; }
        public DbSet<Setting> Settings { get; set; }
    }
}
