using SimvestFun.ApplicationCore.Entities;

namespace SimvestFun.ApplicationCore.Interfaces
{
    public interface IPortfolioValuesService
    {
        Task CreatePortfolioValuesAsync(ApplicationUser user);
        Task SaveYesterdayPortfolioValueForAllUsersAsync();
        Task<decimal> GetYesterdayPortfolioValueAsync(string userId);
    }
}
