using SimvestFun.ApplicationCore.Entities;
using SimvestFun.ApplicationCore.Models;

namespace SimvestFun.ApplicationCore.Interfaces
{
    public interface IUserActionService
    {
        public Task<ActionsModel> GetUserActionsAsync(string userId, int numberOfActions);
        public Task<UserAction> AddUserActionAsync(string actionType, UserStockModel model);
        public Task<UserAction?> GetLastUserActionAsync(string userId);
        public Task<List<UserAction>> GetRecentUserActionsAsync();
        public Task<bool> CheckIfUserHasReceivedBonusToday(string userId);
        public Task CheckHasBoughtAnyStocks(string userId);
    }
}
