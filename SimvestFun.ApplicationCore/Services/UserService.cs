using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using SimvestFun.ApplicationCore.ApplicationExceptions;
using SimvestFun.ApplicationCore.Entities;
using SimvestFun.ApplicationCore.Interfaces;
using SimvestFun.ApplicationCore.Models;

namespace SimvestFun.ApplicationCore.Services
{
    public class UserService : IUserService
    {
        private readonly ISimvestFunContext _context;
        private readonly IMapper _mapper;
        private readonly int _pageSize = 20;
        private readonly IUserStockService _userStocksService;
        private readonly IPortfolioValuesService _portfolioValuesService;
        private readonly IUserActionService _userActionService;
        private readonly IConfiguration _configurationProvider;

        public UserService(ISimvestFunContext context,
                           IMapper mapper,
                           IUserStockService userStockService,
                           IPortfolioValuesService portfolioValuesService,
                           IUserActionService userActionService,
                           IConfiguration configurationProvider)
        {
            _context = context;
            _mapper = mapper;
            _userStocksService = userStockService;
            _portfolioValuesService = portfolioValuesService;
            _userActionService = userActionService;
            _configurationProvider = configurationProvider;
        }

        public async Task<ApplicationUser> GetById(string id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
                throw new Exception("User not found");

            return user;
        }

        public async Task UpdatePortfolioValuesForAllUsers(bool dayChanged)
        {
            var users = _context.Users.ToList();

            foreach (var user in users)
            {
                if (user.YesterdayPortfolioValue == 0M)
                    user.YesterdayPortfolioValue = await _portfolioValuesService.GetYesterdayPortfolioValueAsync(user.Id);

                if (dayChanged)
                    user.YesterdayPortfolioValue = user.TotalPortfolioValue;

                var userStocks = _context.UserStocks
                    .Where(us => us.ApplicationUserId == user.Id)
                    .ToList();

                var newStocksValue = 0M;
                foreach(var userStock in userStocks)
                {
                    var stock = _context.Stocks
                        .FirstOrDefault(s => s.Id == userStock.StockId);
                    newStocksValue += stock.CurrentPrice * userStock.UnitCount;
                }

                user.TotalPortfolioValue = newStocksValue + user.Cash;
                user.PortfolioChange = user.TotalPortfolioValue - user.YesterdayPortfolioValue;
            }

            await _context.SaveChangesAsync();
        }

        public async Task UpdateRankingForAllUsers(bool dayChanged)
        {
            var users = _context.Users
                .Where(u => u.HasBoughtAnyStocks)
                .OrderByDescending(u => u.TotalPortfolioValue).ToList();
            foreach(var user in users)
            {
                if (dayChanged)
                     user.YesterdayPosition = user.CurrentPosition;
                user.CurrentPosition = users.IndexOf(user)+1;
            }
            await _context.SaveChangesAsync();
        }

        public async Task<List<ApplicationUser>> GetLeaderboardUsers(int count)
        {
            var topUsers = _context.Users
                .Where(u => u.HasBoughtAnyStocks)
                .OrderBy(x => x.CurrentPosition).Take(count);

            return await topUsers.ToListAsync();
        }

        public async Task<UserPagingModel> GetUsersByPageIndex(int pageIndex, string name)
        {
            IQueryable<ApplicationUser> allUsers;
            if (name == null)
            {
                allUsers = _context.Users.OrderByDescending(u => u.TotalPortfolioValue);
            }
            else
            {
                allUsers = _context.Users.Where(u => u.Name.ToLower().Contains(name.ToLower()))
                    .OrderBy(u => u.Name.IndexOf(name.ToLower()));
            }
            var listSize = allUsers.Count();
            var totalPages = listSize / _pageSize;
            if (listSize == 0 || listSize % _pageSize != 0)
                totalPages++;

            var result = await allUsers
                    .Include(u => u.UserStocks)
                    .Skip((pageIndex - 1) * _pageSize)
                    .Take(_pageSize).ToListAsync();

            foreach(var user in result)
            {
                if (user.TopInvestments == null)
                    await _userStocksService.CalculateUserTopInvestments(user.Id);
            }

            var usersPaging = new UserPagingModel
            {
                Users = _mapper.Map<List<UserModel>>(result),
                TotalPages = totalPages,
                TotalSize = listSize
            };
            return usersPaging;
        }

        public async Task<ApplicationUser> ResetAccountAsync(string userId)
        {
            var dbUser = await _context.Users.Where(u => u.Id == userId)
                                             .Include(us => us.UserStocks)
                                             .FirstOrDefaultAsync();
            if (dbUser == null)
                throw new Exception("User not found");

            var actionHistory = new UserAction()
            {
                ApplicationUserId = dbUser.Id,
                ActionType = "Reset",
                TimeStamp = DateTime.UtcNow,
                Description = "Account was reset from $" + dbUser.TotalPortfolioValue + " to $10000."
            };

            dbUser.Cash = 10000M;
            dbUser.TotalPortfolioValue = 10000M;
            dbUser.PortfolioChange = 0M;
            dbUser.TopInvestments = "";
            await UpdateRankingForAllUsers(false);
            
            _context.UserActions.Add(actionHistory);

            foreach(var userStock in dbUser.UserStocks)
            {
                _context.UserStocks.Remove(userStock);
            }

            var userPortfolioValues = new PortfolioValue()
            {
                ApplicationUserId = dbUser.Id,
                TimeStamp = DateTime.UtcNow,
                TotalPortfolioValue = 10000M
            };
            _context.PortfolioValues.Add(userPortfolioValues);

            await _context.SaveChangesAsync();

            return dbUser;
        }

        public async Task<ApplicationUser> GetUserPortofolioValues(string userId)
        {
            var user = _context.Users.Find(userId);
            if (user == null)
                throw new EntityNotFoundException();

            user.PortfolioValues = _context.PortfolioValues
                .Where(pv => pv.ApplicationUserId == userId)
                .OrderByDescending(pv => pv.TimeStamp)
                .Take(19)
                .OrderBy(pv => pv.TimeStamp)
                .ToList();

            user.PortfolioValues.Add(
                new PortfolioValue()
                {
                    ApplicationUserId = userId,
                    TotalPortfolioValue = user.TotalPortfolioValue,
                    TimeStamp = DateTime.UtcNow
                });

            return user;
        }

        public async Task<UserModel> UpdateUserDetails(UserModel user)
        {

            if (user.Name.Length > 30 || user.About.Length > 300)
                throw new InvalidOperationException();

            var dbUser = await _context.Users.FindAsync(user.Id);

            dbUser.Name = user.Name;
            dbUser.About = user.About;

            await _context.SaveChangesAsync();

            return _mapper.Map<UserModel>(dbUser);
        }

        public async Task<UserModel> UpdateUserDetailsByAdmin(UserModel user, string connectedUserId)
        {
            var connectedUser = _context.Users.FirstOrDefault(us => us.Id == connectedUserId);
            if (connectedUser == null)
                throw new EntityNotFoundException();

            if (!connectedUser.IsAdmin)
                throw new UnauthorizedAccessException();

            if (user.Name.Length > 30 || user.About.Length > 300)
                throw new InvalidOperationException();

            var updatedUser = await _context.Users.FindAsync(user.Id);

            updatedUser.Name = user.Name;
            updatedUser.About = user.About;

            await _context.SaveChangesAsync();

            return _mapper.Map<UserModel>(updatedUser);
        }

        public async Task<ApplicationUser> AddDailyBonusAsync(string userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                throw new EntityNotFoundException();

            var hasReceivedBonus = await _userActionService.CheckIfUserHasReceivedBonusToday(userId);
            if (hasReceivedBonus)
                throw new InvalidActionException();

            var userAction = new UserAction()
            {
                ActionType = "DailyBonus",
                ApplicationUserId = userId,
                TimeStamp = DateTime.UtcNow,
                Description = "Received the $10 daily bonus."
            };

            user.TotalPortfolioValue += 10;
            user.Cash += 10;
            user.LastVisitedOn = DateTime.UtcNow;
            _context.UserActions.Add(userAction);
            await _context.SaveChangesAsync();

            await UpdateRankingForAllUsers(false);

            return user;
        }

        public async Task<UserModel> UnsubscribeUserByGuid(string id)
        {
            var guidId = new Guid(id);
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UnsubscribeGuid == guidId);
            if (user == null)
                throw new EntityNotFoundException();

            user.IsUnsubscribedFromEmails = true;
            await _context.SaveChangesAsync();

            return _mapper.Map<UserModel>(user);
        }

        public async Task SendUpdateEmails()
        {
            var usersToSendMail = await _context.Users
                .Where(
                    u => !u.IsUnsubscribedFromEmails &&
                    u.LastVisitedOn.AddMonths(1) < DateTime.UtcNow &&
                    (u.LastEmailSentOn == null || (u.LastEmailSentOn.Value.AddMonths(1) < DateTime.UtcNow)))
                .ToListAsync();
            var topUser = _context.Users.OrderByDescending(u => u.TotalPortfolioValue).First();

            foreach (var user in usersToSendMail)
            {
                var position = "-";
                if (user.CurrentPosition != null)
                    position = user.CurrentPosition.ToString();

                var apiKey = _configurationProvider.GetSection("SendGrid:ApiKey").Value;
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress("iustin.deaconu@fortech.ro", "Simvest.fun Team");
                var subject = "Simvest.fun Update";
                var to = new EmailAddress(user.Email, user.Name);
                var bcc = new EmailAddress("dan.dumitru@fortech.ro");
                var plainTextContent = "You haven't visited Simvest.fun in a while, here is an update: " +
                    $"/nYour portfolio value: ${user.TotalPortfolioValue} (virtual money)" +
                    $"Your position: {position} (only put this if it has a Position) /nTop user: {topUser.Name} (${topUser.TotalPortfolioValue.ToString("n")})" +
                    "/nhttps://simvest.fun/n" + "We only send this email once a month, if you haven't logged in." +
                    "You can easily unsubscribe by clicking here: https://simvest.fun/unsubscribe/GUID";
                var htmlContent = $"<div><p>You haven't visited <strong>Simvest.fun</strong> in a while, here is an update: <br/>" +
                    $"<br/>Your portfolio value: ${user.TotalPortfolioValue.ToString("n")} (virtual money)" +
                    $"<br/>Your position: {position}" +
                    $"<br/>Top user: {topUser.Name} (${topUser.TotalPortfolioValue.ToString("n")})<br/>" +
                    "<br/><a href=https://simvest.fun>https://simvest.fun/</a><br/>" +
                    "<br/>We only send this email once a month, if you haven't logged in. " +
                    $"You can easily unsubscribe by clicking here: </p> <a href=https://simvest.fun/unsubscribe/{user.UnsubscribeGuid}>https://simvest.fun/unsubscribe/GUID</a></div>";

                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                msg.AddBcc(bcc);
                var response = await client.SendEmailAsync(msg);

                user.LastEmailSentOn = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();
        }
    }
}