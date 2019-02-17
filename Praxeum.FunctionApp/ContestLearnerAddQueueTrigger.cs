using AutoMapper;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Praxeum.Domain.Contests.Learners;
using Praxeum.FunctionApp.Helpers;
using System.Threading.Tasks;

namespace Praxeum.FunctionApp
{
    public static class ContestLearnerAddQueueTrigger
    {
        [FunctionName("ContestLearnerAddQueueTrigger")]
        public static async Task Run(
            [QueueTrigger("contestlearner-add", Connection = "AzureStorageOptions:ConnectionString")] ContestLearnerAdd contestLearnerAdd,
            ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {JsonConvert.SerializeObject(contestLearnerAdd, Formatting.Indented)}");

            var contestLearnerAdder =
                new ContestLearnerAdder(
                    ObjectFactory.CreateMapper(),
                    ObjectFactory.CreateAzureQueueStorageEventPublisher(),
                    ObjectFactory.CreateContestRepository(),
                    ObjectFactory.CreateContestLearnerRepository(),
                    ObjectFactory.CreateMicrosoftProfileRepository(),
                    ObjectFactory.CreateContestLearnerStartValueUpdater(),
                    ObjectFactory.CreateContestLearnerTargetValueUpdater(),
                    ObjectFactory.CreateContestLearnerCurrentValueUpdater());

            var contestLearnerAdded =
                await contestLearnerAdder.ExecuteAsync(
                    contestLearnerAdd);

            log.LogInformation(
                JsonConvert.SerializeObject(contestLearnerAdded, Formatting.Indented));
        }
    }
}
