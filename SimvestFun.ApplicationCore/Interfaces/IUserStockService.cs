using SimvestFun.ApplicationCore.Entities;

namespace SimvestFun.ApplicationCore.Interfaces
{
    public interface IUserStockService
    {
        Task BuyUserStockAsync(UserStock model);
        Task<List<UserStock>> GetUserStocksByUserIdAsync(string userId);
        Task<UserStock> GetUserStockByIdAsync(int id);
        Task<UserStock> SellUserStockAsync(UserStock model, int id);
        Task CalculateUserTopInvestments(string userId);
    }
}