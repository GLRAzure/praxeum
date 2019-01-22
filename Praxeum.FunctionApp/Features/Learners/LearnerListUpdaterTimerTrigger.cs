using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Praxeum.FuncApp.Features.Learners;
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
                    new LearnerRepository(azureCosmosDbOptions));

            var learnerListUpdate =
                new LearnerListUpdate();

            log.LogInformation(
                JsonConvert.SerializeObject(learnerListUpdate));

            var learnerListUpdated =
                await learnerListUpdater.ExecuteAsync(
                    learnerListUpdate);
        }
    }
}
