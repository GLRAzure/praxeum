using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Praxeum.Domain.Contests;
using Praxeum.FunctionApp.Helpers;
using System.Threading.Tasks;

namespace Praxeum.FunctionApp
{
    public static class ContestNumberOfLearnersUpdateQueueTrigger
    {
        [FunctionName("ContestNumberOfLearnersUpdateQueueTrigger")]
        public static async Task Run(
            [QueueTrigger("contestnumberoflearners-update", Connection = "AzureStorageOptions:ConnectionString")] ContestNumberOfLearnersUpdate contestNumberOfLearnersUpdate,
            ILogger log)
        {
            log.LogInformation(
                JsonConvert.SerializeObject(contestNumberOfLearnersUpdate, Formatting.Indented));

            var contestNumberOfLearnersUpdater =
                new ContestNumberOfLearnersUpdater(
                    ObjectFactory.CreateContestRepository(),
                    ObjectFactory.CreateContestLearnerRepository());

            var contestNumberOfLearnersUpdated =
                await contestNumberOfLearnersUpdater.ExecuteAsync(
                    contestNumberOfLearnersUpdate);

            log.LogInformation(
                JsonConvert.SerializeObject(contestNumberOfLearnersUpdated, Formatting.Indented));

        }
    }
}
