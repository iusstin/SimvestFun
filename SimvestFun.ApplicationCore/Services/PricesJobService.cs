using SimvestFun.ApplicationCore.Interfaces;

namespace SimvestFun.ApplicationCore.Services
{
    public class PricesJobService : IPricesJobService
    {
        private readonly IPortfolioValuesService _portfolioValuesService;
        private readonly IStockService _stockService;
        private readonly IUserService _userService;
        private readonly IStockPriceService _stockPriceService;

        public PricesJobService(IPortfolioValuesService portfolioValuesService,
                               IStockService stockService,
                               IUserService userService,
                               IStockPriceService stockPriceService)
        {
            _portfolioValuesService = portfolioValuesService;
            _stockService = stockService;
            _userService = userService;
            _stockPriceService = stockPriceService;
        }

        public async Task UpdatePricesAndPortfolioValues()
        {
            var dayChanged = await _stockService.CheckTwelveHourChange();
            var pricesHavechanged = await _stockService.UpdateStockPricesAsync(dayChanged);

            if(pricesHavechanged)
            {
                await _userService.UpdatePortfolioValuesForAllUsers(dayChanged);
                await _userService.UpdateRankingForAllUsers(dayChanged);
                if (dayChanged)
                {
                    await _portfolioValuesService.SaveYesterdayPortfolioValueForAllUsersAsync();
                    await _stockPriceService.SaveYesterdayStockPricesAsync();
                }
            }
        }
    }
}
