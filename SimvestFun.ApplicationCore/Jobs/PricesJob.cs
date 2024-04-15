using Microsoft.Extensions.DependencyInjection;
using Quartz;
using SimvestFun.ApplicationCore.Interfaces;

namespace SimvestFun.ApplicationCore.Jobs
{
    public class PricesJob : IJob
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public PricesJob(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var _dailyJobService = scope.ServiceProvider.GetService<IPricesJobService>();
                await _dailyJobService.UpdatePricesAndPortfolioValues();
            }
        }
    }
}
