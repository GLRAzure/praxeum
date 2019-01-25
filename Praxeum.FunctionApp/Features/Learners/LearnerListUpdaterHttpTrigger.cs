using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Praxeum.FunctionApp.Helpers;
using Praxeum.Data;
using System;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;
using Praxeum.Data.Helpers;

namespace Praxeum.FunctionApp.Features.Learners
{
    public static class LearnerListUpdaterHttpTrigger
    {
        [FunctionName("LearnerListUpdaterHttpTrigger")]
        public static async Task<IActionResult> Run(
             [HttpTrigger(AuthorizationLevel.Function, "put", Route = "learners")] HttpRequest req,
             ILogger log)
        {
            log.LogInformation($"C# HTTP trigger function {nameof(LearnerListUpdaterHttpTrigger)} executed at: {DateTime.Now}");
            
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
 
            var learnerListUpdated =
                await learnerListUpdater.ExecuteAsync(
                    learnerListUpdate);

            return new OkObjectResult(learnerListUpdated);
        }
    }
}
