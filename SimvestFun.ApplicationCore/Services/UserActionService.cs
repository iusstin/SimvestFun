using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimvestFun.ApplicationCore.Entities;
using SimvestFun.ApplicationCore.Interfaces;
using SimvestFun.ApplicationCore.Models;

namespace SimvestFun.ApplicationCore.Services
{
    public class UserActionService : IUserActionService
    {
        private readonly ISimvestFunContext _context;
        private readonly IMapper _mapper;

        public UserActionService(ISimvestFunContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserAction> AddUserActionAsync(string actionType, UserStockModel model)
        {
            if (!_context.Users.Where(u => u.Id == model.ApplicationUserId).Any())
            {
                throw new KeyNotFoundException();
            }

            var userAction = new UserAction()
            {
                ActionType = actionType,
                ApplicationUserId = model.ApplicationUserId,
                TimeStamp = DateTime.UtcNow,
                Description = (actionType == "Sell" ? "Sold " : "Bought ") + model.UnitCount + " " +
                                (model.UnitCount == 1 ? "unit of " : "units of ") + model.StockId +
                                " for $" + model.BuyingPricePerUnit + " per unit."
            };
  
            _context.UserActions.Add(userAction);
            await _context.SaveChangesAsync();

            return userAction;
        }

        public async Task<ActionsModel> GetUserActionsAsync(string userId, int numberOfActions)
        {
            var actions = await _context.UserActions.Where(ua => ua.ApplicationUserId == userId)
                                                    .OrderByDescending(ua => ua.TimeStamp)
                                                    .Take(numberOfActions).ToListAsync();

            return new ActionsModel()
            {
                UserActions = _mapper.Map<List<UserActionModel>>(actions),
                AllActionsCount = await _context.UserActions.CountAsync(ua => ua.ApplicationUserId == userId)
            };
        }

        public async Task<UserAction?> GetLastUserActionAsync(string userId)
        {
            return await _context.UserActions.Where(ua => ua.ApplicationUserId == userId)
                                             .OrderBy((ua) => ua.TimeStamp)
                                             .LastOrDefaultAsync();
        }

        public async Task<List<UserAction>> GetRecentUserActionsAsync()
        {
            return await _context.UserActions
                .Include(ua => ua.User)
                .OrderByDescending(ua => ua.TimeStamp)
                .Take(60).ToListAsync();
        }

        public async Task<bool> CheckIfUserHasReceivedBonusToday(string userId)
        {
            var action = await _context.UserActions
                .Where(ua => ua.ApplicationUserId == userId && ua.ActionType == "DailyBonus" && ua.TimeStamp.Date == DateTime.UtcNow.Date)
                .FirstOrDefaultAsync();
            return action != null;
        }

        public async Task CheckHasBoughtAnyStocks(string userId)
        {
            var userActions = await _context.UserActions
                .Where(ac => ac.User.Id == userId && ac.ActionType == "Buy")
                .ToListAsync();
            var user = _context.Users.Find(userId);
            if (userActions.Count != 0)
                user.HasBoughtAnyStocks = true;
            else
                user.HasBoughtAnyStocks = false;

            await _context.SaveChangesAsync();
        }
    }
}
