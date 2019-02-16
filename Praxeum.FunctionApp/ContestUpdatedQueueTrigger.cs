using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Praxeum.Domain.Contests;

namespace Praxeum.FunctionApp.Features.Learners
{
    public static class ContestUpdatedQueueTrigger
    {
        [FunctionName("ContestUpdatedQueueTrigger")]
        public static void Run(
            [QueueTrigger("contest-updated", Connection = "AzureStorageOptions:ConnectionString")] ContestUpdated contestUpdated,
            [Queue("contestprogress-update", Connection = "AzureStorageOptions:ConnectionString")] ICollector<ContestProgressUpdate> contestProgressUpdates,
            ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {JsonConvert.SerializeObject(contestUpdated, Formatting.Indented)}");

            contestProgressUpdates.Add(
                new ContestProgressUpdate
                {
                    Id = contestUpdated.Id
                });
        }
    }
}
