using Microsoft.EntityFrameworkCore;
using SimvestFun.ApplicationCore.Entities;
using SimvestFun.ApplicationCore.Interfaces;
using SimvestFun.ApplicationCore.ApplicationExceptions;

namespace SimvestFun.ApplicationCore.Services
{
    public class UserStockService : IUserStockService
    {
        private readonly ISimvestFunContext _context;

        public UserStockService(ISimvestFunContext context)
        {
            _context = context;
        }

        public async Task BuyUserStockAsync(UserStock userStock)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userStock.ApplicationUserId);
            if (user == null) throw new EntityNotFoundException();

            var stock = _context.Stocks.FirstOrDefault(s => s.Id == userStock.StockId);
            var totalValue = userStock.UnitCount * stock.CurrentPrice;
            if (user.Cash < totalValue || userStock.UnitCount <= 0)
                throw new InvalidActionException();

            var existingUserStock = _context.UserStocks
                .Where(us => us.StockId == userStock.StockId && us.ApplicationUserId == userStock.ApplicationUserId)
                .FirstOrDefault();

            if(existingUserStock != null)
            {
                var weightedMean = (existingUserStock.BuyingPricePerUnit * existingUserStock.UnitCount + userStock.BuyingPricePerUnit * userStock.UnitCount) /
                    (existingUserStock.UnitCount + userStock.UnitCount);
                var newBuyingPrice = Math.Round(weightedMean, 2);
                existingUserStock.UnitCount += userStock.UnitCount;
                existingUserStock.BuyingPricePerUnit = newBuyingPrice;
            } else
            {
                _context.UserStocks.Add(userStock);
            }

            user.Cash -= totalValue;
            user.HasBoughtAnyStocks = true;
            await _context.SaveChangesAsync();
            await CalculateUserTopInvestments(user.Id);
        }

        public async Task<List<UserStock>> GetUserStocksByUserIdAsync(string userId)
        {
            var result = await _context.UserStocks
                .Include(us => us.Stock)
                .Where(us => us.ApplicationUserId == userId)
                .OrderByDescending(us => us.Stock.CurrentPrice * us.UnitCount)
                .ToListAsync();

            return result;
        }

        public async Task<UserStock> GetUserStockByIdAsync(int id)
        {
            return await _context.UserStocks.FindAsync(id);
        }

        public async Task<UserStock> SellUserStockAsync(UserStock sellingUserStock, int id)
        {
            var existingUserStock = _context.UserStocks.FirstOrDefault(us => us.Id == id);
            if (existingUserStock == null) throw new EntityNotFoundException();

            var user = _context.Users.FirstOrDefault(u => u.Id == sellingUserStock.ApplicationUserId);
            if (existingUserStock.UnitCount < sellingUserStock.UnitCount || sellingUserStock.UnitCount <= 0)
                throw new InvalidActionException();
            
            var stock = _context.Stocks.FirstOrDefault(s => s.Id == sellingUserStock.StockId);
            user.Cash += sellingUserStock.UnitCount * stock.CurrentPrice;

            existingUserStock.UnitCount -= sellingUserStock.UnitCount;
            if (existingUserStock.UnitCount == 0)
                _context.UserStocks.Remove(existingUserStock);

            await _context.SaveChangesAsync();
            await CalculateUserTopInvestments(user.Id);
            return existingUserStock;
        }

        public async Task CalculateUserTopInvestments(string userId)
        {
            var topInvestmentsList = await _context.UserStocks.Where(us => us.ApplicationUserId == userId)
                .GroupBy(us => us.StockId, 
                    (stockId, userStocks) => new { 
                        StockId = stockId, 
                        InvestedMoney = userStocks.Sum(us => us.UnitCount * us.Stock.CurrentPrice) 
                    })
                .OrderByDescending(pair => pair.InvestedMoney)
                .Select(pair => pair.StockId)
                .Take(4)
                .ToListAsync();

            var user = _context.Users.Find(userId);
            user.TopInvestments = string.Join(",", topInvestmentsList);
            await _context.SaveChangesAsync();
        }
    }
}
