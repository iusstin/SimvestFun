using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using SimvestFun.ApplicationCore.Interfaces;

namespace SimvestFun.ApplicationCore.Jobs
{
    public class RememberUsJob : IJob
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IConfiguration _configurationProvider;

        public RememberUsJob(IServiceScopeFactory serviceScopeFactory,IConfiguration configurationProvider)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _configurationProvider = configurationProvider;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var sendUpdateEmails = Boolean.Parse(_configurationProvider.GetSection("SendGrid:SendUpdateEmails").Value);
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var userService = scope.ServiceProvider.GetService<IUserService>();
                if (sendUpdateEmails)
                {
                    await userService.SendUpdateEmails();
                }
            }
        }
    }
}
