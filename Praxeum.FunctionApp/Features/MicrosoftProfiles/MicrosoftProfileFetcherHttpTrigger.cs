using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Praxeum.FunctionApp.Features.MicrosoftProfiles;
using System.Threading.Tasks;

namespace Praxeum.FunctionApp.Features.Learners
{
    public static class MicrosoftProfileFetcherHttpTrigger
    {
        [FunctionName("MicrosoftProfileFetcherHttpTrigger")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "microsoft/profiles/{name}")] HttpRequest req,
            string name,
            ILogger log)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return new BadRequestObjectResult("Please pass a name on the query string or in the request body");
            }

            var microsoftProfileFetch =
                new MicrosoftProfileFetch
                {
                    UserName = name
                };

            var microsoftProfileFetcher =
                new MicrosoftProfileFetcher();

            var microsoftProfileFetched =
                await microsoftProfileFetcher.ExecuteAsync(
                    microsoftProfileFetch);

            return new OkObjectResult(microsoftProfileFetched);
        }
    }
}
