using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Praxeum.Data;
using Praxeum.Domain.Contests;
using Praxeum.FunctionApp.Helpers;

namespace Praxeum.FunctionApp
{
    public static class ContestStatusUpdateTimerTrigger
    {
        [FunctionName("ContestStatusUpdateTimerTrigger")]
        public static async Task Run(
            [TimerTrigger("0 */15 * * * *")] TimerInfo myTimer,
            [Queue("conteststatus-update", Connection = "AzureStorageOptions:ConnectionString")] ICollector<ContestStatusUpdate> contestStatusUpdates,
            ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            var contestLister =
                new ContestLister(
                    ObjectFactory.CreateMapper(),
                    ObjectFactory.CreateContestRepository());

            var readyContests =
                await contestLister.ExecuteAsync(
                    new ContestList
                    {
                        Status = ContestStatus.Ready
                    });

            foreach (var contest in readyContests
                .Where(x => x.StartDate <= DateTime.UtcNow))
            {
                contestStatusUpdates.Add(
                    new ContestStatusUpdate
                    {
                        Id = contest.Id
                    });
            }

            var inProgressContests =
                await contestLister.ExecuteAsync(
                    new ContestList
                    {
                        Status = ContestStatus.InProgress
                    });

            foreach (var contest in inProgressContests
                .Where(x => x.EndDate <= DateTime.UtcNow))
            {
                contestStatusUpdates.Add(
                    new ContestStatusUpdate
                    {
                        Id = contest.Id
                    });
            }
        }
    }
}