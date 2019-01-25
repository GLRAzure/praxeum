using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Praxeum.Data;
using Praxeum.Data.Helpers;
using Praxeum.FunctionApp.Features.LeaderBoards.Learners;
using Praxeum.FunctionApp.Helpers;
using System.Threading.Tasks;

namespace Praxeum.FunctionApp.Features.Learners
{
    public static class LearnerAdderQueueTrigger
    {
        [FunctionName("LearnerAdderQueueTrigger")]
        public static async Task Run(
            [QueueTrigger("learner-add", Connection = "AzureStorageOptions:ConnectionString")]LearnerAdd learnerAdd,
            ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {JsonConvert.SerializeObject(learnerAdd, Formatting.Indented)}");
            
            // NOTE: Doing it this way as there because there is no startup file support to just run the mapping configuration once
            var mapper = 
                LearnerProfile.CreateMapper();

            var azureCosmosDbOptions =
                new AzureCosmosDbOptions();

            var learnerRepository =
                new LearnerRepository(Options.Create(azureCosmosDbOptions));

            var leaderBoardRepository =
                new LeaderBoardRepository(Options.Create(azureCosmosDbOptions));

            var leaderBoardLearnerAdder =
                new LeaderBoardLearnerAdder(
                    log,
                    mapper,
                    leaderBoardRepository,
                    learnerRepository);

            var learnerAdder =
                new LearnerAdder(
                    log,
                    mapper,
                    leaderBoardLearnerAdder,
                    new MicrosoftProfileScraper(),
                    new LearnerRepository(Options.Create(azureCosmosDbOptions)));

            var learnerAdded =
                await learnerAdder.ExecuteAsync(
                    learnerAdd);

            log.LogInformation(
                JsonConvert.SerializeObject(learnerAdded));
        }
    }
}
