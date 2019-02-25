using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Praxeum.Domain;
using Praxeum.Domain.Contests.Learners;
using Praxeum.FunctionApp.Helpers;

namespace Praxeum.FunctionApp
{
    public static class ContestLearnerProgressUpdateQueueTrigger
    {
        [FunctionName("ContestLearnerProgressUpdateQueueTrigger")]
        public static async Task Run(
            [QueueTrigger("contestlearnerprogress-update", Connection = "AzureStorageOptions:ConnectionString")]ContestLearnerProgressUpdate contestLearnerProgressUpdate,
            ILogger log)
        {
            log.LogInformation(
                JsonConvert.SerializeObject(contestLearnerProgressUpdate, Formatting.Indented));

            var contestLearnerProgressUpdater =
                new ContestLearnerProgressUpdater(
                    ObjectFactory.CreateMapper(),
                    ObjectFactory.CreateContestRepository(),
                    ObjectFactory.CreateContestLearnerRepository(),
                    ObjectFactory.CreateMicrosoftProfileRepository(),
                    ObjectFactory.CreateContestLearnerTargetValueUpdater(),
                    ObjectFactory.CreateContestLearnerCurrentValueUpdater(),
                    ObjectFactory.CreateExperiencePointsCalculator());

            var contestLearnerProgressUpdated =
                await contestLearnerProgressUpdater.ExecuteAsync(
                    contestLearnerProgressUpdate);

            log.LogInformation(
                JsonConvert.SerializeObject(contestLearnerProgressUpdated, Formatting.Indented));
        }
    }
}
