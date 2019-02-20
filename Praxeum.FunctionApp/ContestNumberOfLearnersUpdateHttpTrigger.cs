using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Praxeum.Domain.Contests;
using Praxeum.FunctionApp.Helpers;

namespace Praxeum.FunctionApp
{
    public static class ContestNumberOfLearnersUpdateHttpTrigger
    {
        [FunctionName("ContestNumberOfLearnersUpdateHttpTrigger")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "contests/numberoflearners")] HttpRequest req,
            [Queue("contestnumberoflearners-update", Connection = "AzureStorageOptions:ConnectionString")] ICollector<ContestNumberOfLearnersUpdate> contestNumberOfLearnersUpdates,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var contestLister =
                new ContestLister(
                    ObjectFactory.CreateMapper(),
                    ObjectFactory.CreateContestRepository());

            var contests =
                await contestLister.ExecuteAsync(
                    new ContestList());

            foreach (var contest in contests)
            {
                var contestNumberOfLearnersUpdate =
                    new ContestNumberOfLearnersUpdate
                    {
                        ContestId = contest.Id
                    };

                log.LogInformation(
                    JsonConvert.SerializeObject(contestNumberOfLearnersUpdate, Formatting.Indented));

                contestNumberOfLearnersUpdates.Add(
                    contestNumberOfLearnersUpdate);
            }

            return new OkResult();
        }
    }
}
