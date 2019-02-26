using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Praxeum.Domain.Contests.Learners;

namespace Praxeum.FunctionApp.Features.Learners
{
    public static class ContestLearnerUpdatedQueueTrigger
    {
        [FunctionName("ContestLearnerUpdatedQueueTrigger")]
        public static void Run(
            [QueueTrigger("contestlearner-updated", Connection = "AzureStorageOptions:ConnectionString")] ContestLearnerUpdated contestLearnerUpdated,
            [Queue("contestlearnerprogress-update", Connection = "AzureStorageOptions:ConnectionString")] ICollector<ContestLearnerProgressUpdate> contestLearnerProgressUpdates,
            ILogger log)
        {
            log.LogInformation(
                JsonConvert.SerializeObject(contestLearnerUpdated, Formatting.Indented));

            contestLearnerProgressUpdates.Add(
                new ContestLearnerProgressUpdate
                {
                    Id = contestLearnerUpdated.Id,
                    ContestId = contestLearnerUpdated.ContestId
                });
        }
    }
}
