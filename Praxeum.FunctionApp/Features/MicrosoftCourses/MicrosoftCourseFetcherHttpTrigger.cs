using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Praxeum.FunctionApp.Features.MicrosoftCourses;
using System.Threading.Tasks;

namespace Praxeum.FunctionApp.Features.Learners
{
    public static class MicrosoftCourseFetcherHttpTrigger
    {
        [FunctionName("MicrosoftCourseFetcherHttpTrigger")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "microsoft/courses/{id}")] HttpRequest req,
            string id,
            ILogger log)
        {
            var microsoftCourseFetch =
                new MicrosoftCourseFetch
                {
                    Id = id
                };

            var microsoftCourseFetcher =
                new MicrosoftCourseFetcher();

            var microsoftCourseFetched =
                await microsoftCourseFetcher.ExecuteAsync(
                    microsoftCourseFetch);

            return new OkObjectResult(microsoftCourseFetched);
        }
    }
}
