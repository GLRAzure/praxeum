using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Praxeum.FunctionApp.Features.Learners;
using System.Threading.Tasks;

namespace Praxeum.FunctionApp.Features.Learners
{
    public static class LearnerFetchHttpTrigger
    {
        [FunctionName("LearnerFetchHttpTrigger")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "learners")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];
            string forceRefresh = req.Query["forceRefresh"];

            if (string.IsNullOrWhiteSpace(name))
            {
                return new BadRequestObjectResult("Please pass a name on the query string or in the request body");
            }

            var learnerFetchByUserName =
                new LearnerFetchByUserName
                {
                    UserName = name
                };

            if (forceRefresh == "1")
            {
                learnerFetchByUserName.CacheExpirationInMinutes = 0;
            }

            var learnerFetchByUserNameHandler =
                new LearnerFetchByUserNameHandler();

            var learnerFetchedByUserName =
                await learnerFetchByUserNameHandler.ExecuteAsync(learnerFetchByUserName);

            return new OkObjectResult(learnerFetchedByUserName);
        }
    }
}
