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
    public static class LearnerLeaderBoardAdderQueueTrigger
    {
        [FunctionName("LearnerLeaderBoardAdderQueueTrigger")]
        public static async Task Run(
            [QueueTrigger("learnerleaderboard-add", Connection = "AzureStorageOptions:ConnectionString")]LearnerLeaderBoardAdd learnerLeaderBoardAdd,
            ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {JsonConvert.SerializeObject(learnerLeaderBoardAdd, Formatting.Indented)}");

            // NOTE: Doing it this way as there because there is no startup file support to just run the mapping configuration once
            var mapper =
                LearnerLeaderBoardProfile.CreateMapper();

            var azureCosmosDbOptions =
                new AzureCosmosDbOptions();

            var learnerLeaderBoardAdder =
                new LearnerLeaderBoardAdder(
                    log,
                    mapper,
                    new LearnerLeaderBoardRepository(Options.Create(azureCosmosDbOptions)));

            var learnerLeaderBoardAdded =
               await learnerLeaderBoardAdder.ExecuteAsync(
                   learnerLeaderBoardAdd);

            log.LogInformation(
                JsonConvert.SerializeObject(learnerLeaderBoardAdded, Formatting.Indented));
        }
    }
}
