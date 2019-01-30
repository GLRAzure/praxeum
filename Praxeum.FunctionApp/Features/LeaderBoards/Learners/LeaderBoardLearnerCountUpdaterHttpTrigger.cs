using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Praxeum.Data;
using System;
using Microsoft.Extensions.Options;
using Praxeum.Data.Helpers;
using Newtonsoft.Json;

namespace Praxeum.FunctionApp.Features.LeaderBoards.Learners
{
    public static class LeaderBoardLearnerCountUpdaterHttpTrigger
    {
        [FunctionName("LeaderBoardLearnerCountUpdaterHttpTrigger")]
        public static async Task<IActionResult> Run(
             [HttpTrigger(AuthorizationLevel.Function, "put", Route = "leaderboards/learners")] HttpRequest req,
             ILogger log)
        {
            log.LogInformation($"C# HTTP trigger function {nameof(LeaderBoardLearnerCountUpdaterHttpTrigger)} executed at: {DateTime.Now}");

            var azureCosmosDbOptions =
                new AzureCosmosDbOptions();

            var leaderBoardLearnerCountUpdater =
                new LeaderBoardLearnerCountUpdater(
                    new LearnerRepository(Options.Create(azureCosmosDbOptions)),
                    new LeaderBoardRepository(Options.Create(azureCosmosDbOptions)));
 
            var leaderBoardLearnerCountUpdated =
                await leaderBoardLearnerCountUpdater.ExecuteAsync(
                    new LeaderBoardLearnerCountUpdate());

            log.LogInformation(
                JsonConvert.SerializeObject(leaderBoardLearnerCountUpdated, Formatting.Indented));

            return new OkObjectResult(leaderBoardLearnerCountUpdated);
        }
    }
}
