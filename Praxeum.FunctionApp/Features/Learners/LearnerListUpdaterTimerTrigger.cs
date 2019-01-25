using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Praxeum.Data;
using Praxeum.FunctionApp.Helpers;
using Microsoft.Extensions.Options;
using Praxeum.Data.Helpers;

namespace Praxeum.FunctionApp.Features.Learners
{
    public static class LearnerListUpdaterTimerTrigger
    {
        [FunctionName("LearnerListUpdaterTimerTrigger")]
        public static async Task Run(
            [TimerTrigger("%LearnerListUpdaterTimerTrigger:ScheduleExpression%")]TimerInfo myTimer,
            ILogger log)
        {
            log.LogInformation($"C# Timer trigger function {nameof(LearnerListUpdaterTimerTrigger)} executed at: {DateTime.Now}");
            
            // NOTE: Doing it this way as there because there is no startup file support to just run the mapping configuration once
            var mapper = 
                LearnerProfile.CreateMapper();

            var azureCosmosDbOptions =
                new AzureCosmosDbOptions();

            var learnerListUpdater =
                new LearnerListUpdater(
                    log,
                    mapper,
                    new MicrosoftProfileScraper(),
                    new LearnerRepository(Options.Create(azureCosmosDbOptions)));

            var learnerListUpdate =
                new LearnerListUpdate();

            log.LogInformation(
                JsonConvert.SerializeObject(learnerListUpdate));

            await learnerListUpdater.ExecuteAsync(
                learnerListUpdate);
        }
    }
}
