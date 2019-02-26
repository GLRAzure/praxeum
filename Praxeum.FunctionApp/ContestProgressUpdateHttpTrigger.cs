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
    public static class ContestProgressUpdateHttpTrigger
    {
        [FunctionName("ContestProgressUpdateHttpTrigger")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "contests/progress")] HttpRequest req,
            [Queue("contestprogress-update", Connection = "AzureStorageOptions:ConnectionString")] ICollector<ContestProgressUpdate> contestProgressUpdates,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

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

            foreach (var contest in contests)
            {
                contestProgressUpdates.Add(
                    new ContestProgressUpdate
                    {
                        Id = contest.Id
                    });
            }

            return new OkResult();
        }
    }
}
