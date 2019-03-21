using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Praxeum.Data;
using Praxeum.Domain.Contests;
using Praxeum.FunctionApp.Helpers;

namespace Praxeum.FunctionApp
{
    public static class ContestEndTimerTrigger
    {
        // https://codehollow.com/2017/02/azure-functions-time-trigger-cron-cheat-sheet/
        // https://github.com/Azure/azure-webjobs-sdk-extensions/wiki/TimerTrigger

        [FunctionName("ContestEndTimerTrigger")]
        public static async Task Run(
            [TimerTrigger("0 0 */1 * * *")] TimerInfo myTimer,
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
                .Where(x => x.EndDate <= DateTime.UtcNow))
            {
                var contestEnder =
                    new ContestEnder(
                        ObjectFactory.CreateMapper(),
                        ObjectFactory.CreateContestRepository());

                await contestEnder.ExecuteAsync(
                     new ContestEnd
                     {
                         Id = contest.Id
                     });
                
                log.LogInformation(JsonConvert.SerializeObject(contest, Formatting.Indented));
            }
        }
    }
}