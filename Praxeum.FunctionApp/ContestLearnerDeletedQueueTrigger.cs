using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Praxeum.Domain.Contests;
using Praxeum.Domain.Contests.Learners;

namespace Praxeum.FunctionApp
{
    public static class ContestLearnerDeletedQueueTrigger
    {
        [FunctionName("ContestLearnerDeletedQueueTrigger")]
        public static void Run(
            [QueueTrigger("contestlearner-deleted", Connection = "AzureStorageOptions:ConnectionString")] ContestLearnerDeleted contestLearnerDeleted,
            [Queue("contestnumberoflearners-update", Connection = "AzureStorageOptions:ConnectionString")] ICollector<ContestNumberOfLearnersUpdate> contestNumberOfLearnersUpdates,
            ILogger log)
        {
            log.LogInformation(
                JsonConvert.SerializeObject(contestLearnerDeleted, Formatting.Indented));

            var contestNumberOfLearnersUpdate =
                new ContestNumberOfLearnersUpdate
                {
                    ContestId = contestLearnerDeleted.ContestId
                };

            log.LogInformation(
                JsonConvert.SerializeObject(contestNumberOfLearnersUpdate, Formatting.Indented));

            contestNumberOfLearnersUpdates.Add(
                contestNumberOfLearnersUpdate);
        }
    }
}
