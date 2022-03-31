using Quartz;
using System;
using System.Threading.Tasks;

namespace OnboardingApp
{
    public class TestJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await Console.Out.WriteLineAsync($"current time is {DateTime.Now}");
        }
    }
}
