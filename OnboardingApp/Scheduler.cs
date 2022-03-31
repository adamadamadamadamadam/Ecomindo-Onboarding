using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Impl;
using Quartz.Impl.Matchers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OnboardingApp
{
    public class Scheduler : IHostedService
    {
        private readonly IScheduler _scheduler;
        public Scheduler()
        {
            var schedulerFactory = new StdSchedulerFactory();

            _scheduler = schedulerFactory.GetScheduler().GetAwaiter().GetResult();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {

            // define the job and tie it to our HelloJob class
            IJobDetail job = JobBuilder.Create<TestJob>()
                .WithIdentity("myJob", "group1")
                .Build();

            // Trigger the job to run now, and then every 40 seconds
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("myTrigger", "group1")
                .StartNow()
                .WithCronSchedule("* * * * * ? *")
            .Build();

            await _scheduler.ScheduleJob(job, trigger);

            await _scheduler.Start(cancellationToken);

            Console.Out.WriteLine("job started");
            Console.Out.WriteLine(job.JobType);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            if (_scheduler.IsStarted)
            {
                await _scheduler.Standby();
            }

            System.Collections.Generic.IReadOnlyCollection<JobKey> jobKeys = _scheduler
                .GetJobKeys(GroupMatcher<JobKey>.AnyGroup())
                .GetAwaiter()
                .GetResult();

            foreach (JobKey jobKey in jobKeys)
            {
                _ = _scheduler.Interrupt(jobKey);
            }

            _scheduler
                .Shutdown(true)
                .GetAwaiter()
                .GetResult();
        }
    }
}
