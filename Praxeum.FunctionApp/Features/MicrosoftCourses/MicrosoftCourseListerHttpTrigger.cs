using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Praxeum.FunctionApp.Features.MicrosoftCourses;
using System.Threading.Tasks;

namespace Praxeum.FunctionApp.Features.Learners
{
    public static class MicrosoftCourseListHttpTrigger
    {
        [FunctionName("MicrosoftCourseListHttpTrigger")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "microsoft/courses")] MicrosoftCourseList microsoftCourseList,
            ILogger log)
        {
            var microsoftCourseLister =
                new MicrosoftCourseLister();

            var microsoftCourseListed =
                await microsoftCourseLister.ExecuteAsync(
                    microsoftCourseList);

            return new OkObjectResult(microsoftCourseListed);
        }
    }
}
