using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Praxeum.FunctionApp.Data;
using Praxeum.FunctionApp.Helpers;

namespace Praxeum.FunctionApp.Features.Learners
{
    public static class LearnerListUpdaterTimerTrigger
    {
        [FunctionName("LearnerListUpdaterTimerTrigger")]
        public static async Task Run(
            [TimerTrigger("%LearnerListUpdaterTimerTrigger:ScheduleExpression%")]TimerInfo myTimer, 
            ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            var azureCosmosDbOptions =
                new AzureCosmosDbOptions();

            var learnerListUpdater =
                new LearnerListUpdater(
                    log,
                    new MicrosoftProfileScraper(),
                    new LearnerRepository(azureCosmosDbOptions));

            var learnerListUpdate =
                new LearnerListUpdate();

            var learnerListUpdated =
                await learnerListUpdater.ExecuteAsync(
                    learnerListUpdate);
        }
    }
}
