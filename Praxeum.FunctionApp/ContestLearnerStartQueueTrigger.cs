using AutoMapper;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Praxeum.Domain.Contests.Learners;
using Praxeum.FunctionApp.Helpers;
using System.Threading.Tasks;

namespace Praxeum.FunctionApp
{
    public static class ContestLearnerStartQueueTrigger
    {
        [FunctionName("ContestLearnerStartQueueTrigger")]
        public static async Task Run(
            [QueueTrigger("contestlearner-start", Connection = "AzureStorageOptions:ConnectionString")] ContestLearnerStart contestLearnerStart,
            ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {JsonConvert.SerializeObject(contestLearnerStart, Formatting.Indented)}");

            var contestLearnerStarter =
                new ContestLearnerStarter(
                    ObjectFactory.CreateMapper(),
                    ObjectFactory.CreateContestRepository(),
                    ObjectFactory.CreateContestLearnerRepository(),
                    ObjectFactory.CreateMicrosoftProfileRepository(),
                    ObjectFactory.CreateContestLearnerStartValueUpdater(),
                    ObjectFactory.CreateContestLearnerCurrentValueUpdater(),
                    ObjectFactory.CreateExperiencePointsCalculator());

            var contestLearnerStarted =
                await contestLearnerStarter.ExecuteAsync(
                    contestLearnerStart);

            log.LogInformation(
                JsonConvert.SerializeObject(contestLearnerStarted, Formatting.Indented));
        }
    }
}
