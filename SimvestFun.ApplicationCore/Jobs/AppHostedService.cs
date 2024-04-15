using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Spi;

namespace SimvestFun.ApplicationCore.Jobs
{
    public class AppHostedService : IHostedService
    {
        private readonly ISchedulerFactory _schedulerFactory;
        private readonly IJobFactory _jobFactory;
        private readonly IEnumerable<Job> _jobs;

        public AppHostedService(ISchedulerFactory schedulerFactory,
                                IJobFactory jobFactory,
                                IEnumerable<Job> jobs)
        {
            _schedulerFactory = schedulerFactory;
            _jobFactory = jobFactory;
            _jobs = jobs;
        }

        public IScheduler Scheduler { get; set; }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await ScheduleDailyJob(cancellationToken);
        }

        private async Task ScheduleDailyJob(CancellationToken cancellationToken)
        {
            Scheduler = await _schedulerFactory.GetScheduler(cancellationToken);
            Scheduler.JobFactory = _jobFactory;

            foreach(var _job in _jobs)
            {
                var createdJob = CreateJob(_job);
                await Scheduler.ScheduleJob(createdJob, _job.trigger, cancellationToken);
            }
            await Scheduler.Start(cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Scheduler?.Shutdown(cancellationToken);
        }

        private static IJobDetail CreateJob(Job job)
        {
            return JobBuilder.Create(job.Type)
                    .WithIdentity(job.Type.FullName)
                    .Build();
        }
    }
}
