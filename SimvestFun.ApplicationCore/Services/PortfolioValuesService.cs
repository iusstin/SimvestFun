using Microsoft.EntityFrameworkCore;
using SimvestFun.ApplicationCore.Entities;
using SimvestFun.ApplicationCore.Interfaces;

namespace SimvestFun.ApplicationCore.Services
{
    public class PortfolioValuesService : IPortfolioValuesService
    {
        private readonly ISimvestFunContext _context;
        public PortfolioValuesService(ISimvestFunContext context)
        {
            _context = context;
        }

        public async Task CreatePortfolioValuesAsync(ApplicationUser user)
        {
            var newPortfolioValue = new PortfolioValue()
            {
                ApplicationUserId = user.Id,
                TotalPortfolioValue = user.TotalPortfolioValue,
                TimeStamp = DateTime.UtcNow
            };
            _context.PortfolioValues.Add(newPortfolioValue);

            await _context.SaveChangesAsync();
        }

        public async Task<decimal> GetYesterdayPortfolioValueAsync(string userId)
        {
            var portfolioValue = await _context.PortfolioValues.Where(pv => pv.ApplicationUserId == userId &&
                                                                      pv.TimeStamp.Date.CompareTo(DateTime.UtcNow.Date) == -1)
                                                               .OrderBy(pv => pv.TimeStamp)
                                                               .LastOrDefaultAsync();
            if (portfolioValue == null)
                return 10000M;

            return portfolioValue.TotalPortfolioValue;
        }

        public async Task SaveYesterdayPortfolioValueForAllUsersAsync()
        {
            var users = await _context.Users.ToListAsync();

            foreach (var user in users)
            {
                var newPortfolioValue = new PortfolioValue()
                {
                    ApplicationUserId = user.Id,
                    TotalPortfolioValue = user.YesterdayPortfolioValue,
                    TimeStamp = DateTime.UtcNow.Date.AddDays(-1)
                };
                await _context.PortfolioValues.AddAsync(newPortfolioValue);
            }

            await _context.SaveChangesAsync();
        }
    }
}
