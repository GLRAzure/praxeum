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
    public static class LeaderBoardLearnerAdderQueueTrigger
    {
        [FunctionName("LeaderBoardLearnerAdderQueueTrigger")]
        public static async Task Run(
            [QueueTrigger("leaderboardlearner-add", Connection = "AzureStorageOptions:ConnectionString")]LeaderBoardLearnerAdd leaderBoardLearnerAdd,
            ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {JsonConvert.SerializeObject(leaderBoardLearnerAdd, Formatting.Indented)}");

            // NOTE: Doing it this way as there because there is no startup file support to just run the mapping configuration once
            var mapper =
                LeaderBoardLearnerProfile.CreateMapper();

            var azureCosmosDbOptions =
                new AzureCosmosDbOptions();

            var leaderBoardLearnerAdder =
                new LeaderBoardLearnerAdder(
                    log,
                    mapper,
                    new LearnerRepository(Options.Create(azureCosmosDbOptions)),
                    new LeaderBoardRepository(Options.Create(azureCosmosDbOptions)));

            var leaderBoardLearnerAdded =
               await leaderBoardLearnerAdder.ExecuteAsync(
                   leaderBoardLearnerAdd);

            log.LogInformation(
                JsonConvert.SerializeObject(leaderBoardLearnerAdded, Formatting.Indented));
        }
    }
}
