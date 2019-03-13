using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Praxeum.Domain.Contests;
using Praxeum.FunctionApp.Helpers;

namespace Praxeum.FunctionApp
{
    public static class ContestStatusUpdateQueueTrigger
    {
        [FunctionName("ContestStatusUpdateQueueTrigger")]
        public static async Task Run(
            [QueueTrigger("conteststatus-update", Connection = "AzureStorageOptions:ConnectionString")] ContestStatusUpdate contestStatusUpdate,
            ILogger log)
        {
            log.LogInformation(
                JsonConvert.SerializeObject(contestStatusUpdate, Formatting.Indented));

            var contestStatusUpdater =
                new ContestStatusUpdater(
                    ObjectFactory.CreateMapper(),
                    ObjectFactory.CreateAzureQueueStorageEventPublisher(),
                    ObjectFactory.CreateContestRepository());

            var contestStatusUpdated =
                await contestStatusUpdater.ExecuteAsync(
                    contestStatusUpdate);

            log.LogInformation(
                JsonConvert.SerializeObject(contestStatusUpdated, Formatting.Indented));
        }
    }
}
