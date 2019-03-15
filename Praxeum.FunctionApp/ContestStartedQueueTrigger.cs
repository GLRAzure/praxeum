using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Praxeum.Domain.Contests;
using Praxeum.FunctionApp.Helpers;

namespace Praxeum.FunctionApp
{
    public static class ContestStartedQueueTrigger
    {
        [FunctionName("ContestStartedQueueTrigger")]
        public static async Task Run(
            [QueueTrigger("contest-started", Connection = "AzureStorageOptions:ConnectionString")],
            ILogger log)
        {
            log.LogInformation(
                JsonConvert.SerializeObject(contestFetch, Formatting.Indented));

            var contestFetch =
                new ContestFetcher(
                    ObjectFactory.CreateMapper(),
                    ObjectFactory.CreateContestRepository(),
                    ObjectFactory.CreateContestLearnerRepository());

            var contestFetch =
                await contestFetch.ExecuteAsync(
                    contestFetch);

            log.LogInformation(
                JsonConvert.SerializeObject(contestFetch, Formatting.Indented));
        }
    }
}
