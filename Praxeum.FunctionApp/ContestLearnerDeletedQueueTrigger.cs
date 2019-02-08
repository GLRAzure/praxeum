using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Praxeum.Data;
using Praxeum.Data.Helpers;
using Praxeum.Domain.Contests;
using Praxeum.Domain.Contests.Learners;
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
            log.LogInformation($"C# Queue trigger function processed: {JsonConvert.SerializeObject(contestLearnerDeleted, Formatting.Indented)}");

            var azureCosmosDbOptions =
                new AzureCosmosDbOptions();

            var contestNumberOfLearnersUpdater =
                new ContestNumberOfLearnersUpdater(
                    new ContestRepository(Options.Create(azureCosmosDbOptions)),
                    new ContestLearnerRepository(Options.Create(azureCosmosDbOptions)));

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
