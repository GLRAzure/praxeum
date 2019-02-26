using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Praxeum.Domain.Contests;
using Praxeum.Domain.Contests.Learners;

namespace Praxeum.FunctionApp
{
    public static class ContestLearnerAddedQueueTrigger
    {
        [FunctionName("ContestLearnerAddedQueueTrigger")]
        public static void Run(
            [QueueTrigger("contestlearner-added", Connection = "AzureStorageOptions:ConnectionString")] ContestLearnerAdded contestLearnerAdded,
            [Queue("contestnumberoflearners-update", Connection = "AzureStorageOptions:ConnectionString")] ICollector<ContestNumberOfLearnersUpdate> contestNumberOfLearnersUpdates,
            ILogger log)
        {
            log.LogInformation(
                JsonConvert.SerializeObject(contestLearnerAdded, Formatting.Indented));

            var contestNumberOfLearnersUpdate =
                new ContestNumberOfLearnersUpdate
                {
                    ContestId = contestLearnerAdded.ContestId
                };

            log.LogInformation(
                JsonConvert.SerializeObject(contestNumberOfLearnersUpdate, Formatting.Indented));

            contestNumberOfLearnersUpdates.Add(
                contestNumberOfLearnersUpdate);
        }
    }
}
