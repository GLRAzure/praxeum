using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Praxeum.FunctionApp.Features.MicrosoftProfiles;
using System.Threading.Tasks;

namespace Praxeum.FunctionApp.Features.Learners
{
    public static class MicrosoftProfileFetchHttpTrigger
    {
        [FunctionName("MicrosoftProfileFetchHttpTrigger")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "profiles/microsoft/{name}")] HttpRequest req,
            string name,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            if (string.IsNullOrWhiteSpace(name))
            {
                return new BadRequestObjectResult("Please pass a name on the query string or in the request body");
            }

            var microsoftProfileFetch =
                new MicrosoftProfileFetch
                {
                    UserName = name
                };

            var microsoftProfileFetchHandler =
                new MicrosoftProfileFetchHandler();

            var microsoftProfileFetched =
                await microsoftProfileFetchHandler.ExecuteAsync(
                    microsoftProfileFetch);

            return new OkObjectResult(microsoftProfileFetched);
        }
    }
}
