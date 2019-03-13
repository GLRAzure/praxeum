using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Praxeum.Domain.Contests;
using Praxeum.Data;
using Praxeum.FunctionApp.Helpers;

namespace Praxeum.FunctionApp
{
    public static class ContestStatusUpdateHttpTrigger
    {
        [FunctionName("ContestStatusUpdateHttpTrigger")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "contests/status")] HttpRequest req,
            [Queue("contestprogress-update", Connection = "AzureStorageOptions:ConnectionString")] ICollector<ContestStatusUpdate> contestStatusUpdates,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

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

            return new OkResult();
        }
    }
}
