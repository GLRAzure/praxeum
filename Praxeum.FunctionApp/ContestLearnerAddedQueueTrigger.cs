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
    public static class ContestLearnerAddedQueueTrigger
    {
        [FunctionName("ContestLearnerAddedQueueTrigger")]
        public static async Task Run(
            [QueueTrigger("contestlearner-added", Connection = "AzureStorageOptions:ConnectionString")]ContestLearnerAdded contestLearnerAdded,
            ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {JsonConvert.SerializeObject(contestLearnerAdded, Formatting.Indented)}");

            var azureCosmosDbOptions =
                new AzureCosmosDbOptions();

            var contestNumberOfLearnersUpdater =
                new ContestNumberOfLearnersUpdater(
                    new ContestRepository(Options.Create(azureCosmosDbOptions)),
                    new ContestLearnerRepository(Options.Create(azureCosmosDbOptions)));

            var contestNumberOfLearnersUpdate =
                new ContestNumberOfLearnersUpdate
                {
                    ContestId = contestLearnerAdded.ContestId
                };

            var contestNumberOfLearnersUpdated =
                await contestNumberOfLearnersUpdater.ExecuteAsync(
                    contestNumberOfLearnersUpdate);

            log.LogInformation(
                JsonConvert.SerializeObject(contestNumberOfLearnersUpdated, Formatting.Indented));
        }
    }
}
