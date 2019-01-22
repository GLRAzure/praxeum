using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Praxeum.FuncApp.Features.Learners;
using Praxeum.FunctionApp.Helpers;
using Praxeum.FunctionApp.Data;
using System;
using Newtonsoft.Json;

namespace Praxeum.FunctionApp.Features.Learners
{
    public static class LearnerListUpdaterHttpTrigger
    {
        [FunctionName("LearnerListUpdaterHttpTrigger")]
        public static async Task<IActionResult> Run(
             [HttpTrigger(AuthorizationLevel.Function, "put", Route = "learners")] HttpRequest req,
             ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            
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

            return new OkObjectResult(learnerListUpdated);
        }
    }
}
