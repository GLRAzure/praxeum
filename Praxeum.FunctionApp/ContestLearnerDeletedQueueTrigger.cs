using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Praxeum.Domain.Contests;
using Praxeum.Domain.Contests.Learners;
using Praxeum.FunctionApp.Helpers;
using System.Threading.Tasks;

namespace Praxeum.FunctionApp.Features.Learners
{
    public static class ContestLearnerDeletedQueueTrigger
    {
        [FunctionName("ContestLearnerDeletedQueueTrigger")]
        public static async Task Run(
            [QueueTrigger("contestlearner-deleted", Connection = "AzureStorageOptions:ConnectionString")]ContestLearnerDeleted contestLearnerDeleted,
            ILogger log)
        {
            log.LogInformation(
                JsonConvert.SerializeObject(contestLearnerDeleted, Formatting.Indented));

            var contestNumberOfLearnersUpdater =
                new ContestNumberOfLearnersUpdater(
                    ObjectFactory.CreateContestRepository(),
                    ObjectFactory.CreateContestLearnerRepository());

            var contestNumberOfLearnersUpdate =
                new ContestNumberOfLearnersUpdate
                {
                    ContestId = contestLearnerDeleted.ContestId
                };

            var contestNumberOfLearnersUpdated =
                await contestNumberOfLearnersUpdater.ExecuteAsync(
                    contestNumberOfLearnersUpdate);

            log.LogInformation(
                JsonConvert.SerializeObject(contestNumberOfLearnersUpdated, Formatting.Indented));
        }
    }
}
