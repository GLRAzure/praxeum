using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Praxeum.Data;
using Microsoft.Extensions.Options;
using Praxeum.Data.Helpers;

namespace Praxeum.FunctionApp.Features.LeaderBoards.Learners
{
    public static class LeaderBoardLearnerCountUpdaterTimerTrigger
    {
        [FunctionName("LeaderBoardLearnerCountUpdaterTimerTrigger")]
        public static async Task Run(
            [TimerTrigger("%LeaderBoardLearnerCountUpdaterTimerTrigger:ScheduleExpression%")]TimerInfo myTimer,
            ILogger log)
        {
            log.LogInformation($"C# Timer trigger function {nameof(LeaderBoardLearnerCountUpdaterTimerTrigger)} executed at: {DateTime.Now}");
            
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
        }
    }
}
