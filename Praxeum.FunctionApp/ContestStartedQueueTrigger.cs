using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Praxeum.Domain.Contests;
using Praxeum.Domain.Contests.Learners;
using Praxeum.FunctionApp.Helpers;

namespace Praxeum.FunctionApp
{
    public static class ContestStartedQueueTrigger
    {
        [FunctionName("ContestStartedQueueTrigger")]
        public static async Task Run(
            [QueueTrigger("contest-started", Connection = "AzureStorageOptions:ConnectionString")] ContestStarted contestStarted,
            [Queue("contestlearner-start", Connection = "AzureStorageOptions:ConnectionString")] ICollector<ContestLearnerStart> contestLearnerStarts,
            ILogger log)
        {
            log.LogInformation(
                JsonConvert.SerializeObject(contestStarted, Formatting.Indented));

            var contestFetcher =
                new ContestFetcher(
                    ObjectFactory.CreateMapper(),
                    ObjectFactory.CreateContestRepository(),
                    ObjectFactory.CreateContestLearnerRepository());

            var contestFetched =
                await contestFetcher.ExecuteAsync(
                    new ContestFetch
                    {
                        Id = contestStarted.Id
                    });

            foreach (var learner in contestFetched.Learners)
            {
                contestLearnerStarts.Add(
                    new ContestLearnerStart
                    {
                        ContestId = learner.ContestId,
                        Id = learner.Id
                    });
            }
        }
    }
}
