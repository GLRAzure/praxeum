using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Praxeum.FunctionApp.Data;
using Praxeum.FunctionApp.Helpers;

namespace Praxeum.FunctionApp
{
    public static class MicrosoftProfileHttpTrigger
    {
        [FunctionName("MicrosoftProfileHttpTrigger")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "learners")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            if (string.IsNullOrWhiteSpace(name))
            {
                return new BadRequestObjectResult("Please pass a name on the query string or in the request body");
            }

            var learnerRepository =
                new LearnerRepository();

            var learner =
                await learnerRepository.FetchAsync(name);

            var cacheExpirationInMinutes =
                Convert.ToInt32(Environment.GetEnvironmentVariable("CacheExpirationInMinutes"));

            if (learner == null
                || learner.ModifiedOn == null
                || learner.ModifiedOn.AddMinutes(cacheExpirationInMinutes) <= DateTime.UtcNow)
            {
                var microsoftProfileScraper =
                    new MicrosoftProfileScraper();

                learner =
                    microsoftProfileScraper.FetchProfile(name);
                learner.ModifiedOn = 
                    DateTime.UtcNow;

                await learnerRepository.AddOrUpdateAsync(learner);
            }

            return new OkObjectResult(learner);
        }
    }
}
