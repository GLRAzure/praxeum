using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Praxeum.FunctionApp.Features.LeaderBoards;
using System.Threading.Tasks;

namespace Praxeum.FunctionApp.Features.Learners
{
    public static class LeaderBoardFetchListHttpTrigger
    {
        [FunctionName("LeaderBoardFetchListHttpTrigger")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "leaderboards")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var leaderBoardFetchList =
                new LeaderBoardFetchList();

            var leaderBoardFetchListHandler =
                new LeaderBoardFetchListHandler();

            var leaderBoardFetchedList =
                await leaderBoardFetchListHandler.ExecuteAsync(leaderBoardFetchList);

            return new OkObjectResult(leaderBoardFetchedList);
        }
    }
}
