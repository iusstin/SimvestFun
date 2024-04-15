using SimvestFun.ApplicationCore.Entities;
using SimvestFun.ApplicationCore.Models;

namespace SimvestFun.ApplicationCore.Interfaces
{
    public interface IUserService
    {
        Task<ApplicationUser> GetById(string id);
        Task UpdatePortfolioValuesForAllUsers(bool dayChanged);
        Task<List<ApplicationUser>> GetLeaderboardUsers(int count);
        Task<UserPagingModel> GetUsersByPageIndex(int pageIndex, string name);
        Task<ApplicationUser> ResetAccountAsync(string userId);
        Task<ApplicationUser> GetUserPortofolioValues(string id);
        Task<UserModel> UpdateUserDetails(UserModel user);
        Task<UserModel> UpdateUserDetailsByAdmin(UserModel user, string connectedUserId);
        Task<ApplicationUser> AddDailyBonusAsync(string id);
        Task UpdateRankingForAllUsers(bool dayChanged);
        Task SendUpdateEmails();
        Task<UserModel> UnsubscribeUserByGuid(string id);
    }
}