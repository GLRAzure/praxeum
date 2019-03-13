using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Praxeum.Domain.Contests;
using Praxeum.FunctionApp.Helpers;

namespace Praxeum.FunctionApp
{
    public static class ContestProgressUpdateQueueTrigger
    {
        [FunctionName("ContestProgressUpdateQueueTrigger")]
        public static async Task Run(
            [QueueTrigger("contestprogress-update", Connection = "AzureStorageOptions:ConnectionString")] ContestProgressUpdate contestProgressUpdate,
            ILogger log)
        {
            log.LogInformation(
                JsonConvert.SerializeObject(contestProgressUpdate, Formatting.Indented));

            var contestProgressUpdater =
                new ContestProgressUpdater(
                    ObjectFactory.CreateMapper(),
                    ObjectFactory.CreateAzureQueueStorageEventPublisher(),
                    ObjectFactory.CreateContestRepository(),
                    ObjectFactory.CreateContestLearnerRepository());

            var contestProgressUpdated =
                await contestProgressUpdater.ExecuteAsync(
                    contestProgressUpdate);

            log.LogInformation(
                JsonConvert.SerializeObject(contestProgressUpdated, Formatting.Indented));
        }
    }
}
