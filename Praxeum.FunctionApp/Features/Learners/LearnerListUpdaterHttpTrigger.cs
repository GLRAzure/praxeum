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

namespace Praxeum.FunctionApp.Features.Learners
{
    public static class LearnerListUpdaterHttpTrigger
    {
        static LearnerListUpdaterHttpTrigger()
        {
            Mapper.Initialize(
                cfg =>
                {
                    cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;

                    cfg.AddProfile<LearnerProfile>();
                });

            Mapper.AssertConfigurationIsValid();
        }

        [FunctionName("LearnerListUpdaterHttpTrigger")]
        public static async Task<IActionResult> Run(
             [HttpTrigger(AuthorizationLevel.Function, "put", Route = "learners")] HttpRequest req,
             ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var azureCosmosDbOptions =
                new AzureCosmosDbOptions();

            var learnerListUpdater =
                new LearnerListUpdater(
                    log,
                    new MicrosoftProfileScraper(),
                    new LearnerRepository(azureCosmosDbOptions));

            var learnerListUpdate =
                new LearnerListUpdate();

            if (DateTime.TryParse(req.Query["lastModifiedOn"], out var lastModifiedOn))
            {
                learnerListUpdate.LastModifiedOn = lastModifiedOn;
            }
            else
            {
                learnerListUpdate.LastModifiedOn = DateTime.UtcNow.SubtractMinutes(
                    Convert.ToInt32(Environment.GetEnvironmentVariable("LearnerListUpdaterTimerTrigger:LastModifiedDateInMinutes")));
            }

            var learnerListUpdated =
                await learnerListUpdater.ExecuteAsync(
                    learnerListUpdate);

            return new OkObjectResult(learnerListUpdated);
        }
    }
}
