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
    public static class ContestProgressUpdateTimerTrigger
    {
        [FunctionName("ContestProgressUpdateTimerTrigger")]
        public static async Task Run(
            [TimerTrigger("0 */15 * * * *")] TimerInfo myTimer,
            [Queue("contestprogress-update", Connection = "AzureStorageOptions:ConnectionString")] ICollector<ContestProgressUpdate> contestProgressUpdates,
            ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            var contestLister =
                new ContestLister(
                    ObjectFactory.CreateMapper(),
                    ObjectFactory.CreateContestRepository());

            var contests =
                await contestLister.ExecuteAsync(
                    new ContestList
                    {
                        Status = ContestStatus.InProgress
                    });

            foreach (var contest in contests
                .Where(x => x.NextProgressUpdateOn >= DateTime.UtcNow))
            {
                contestProgressUpdates.Add(
                    new ContestProgressUpdate
                    {
                        Id = contest.Id
                    });
            }
        }
    }
}