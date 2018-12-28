using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Praxeum.FunctionApp.Features.LeaderBoards;
using System.IO;
using System.Threading.Tasks;

namespace Praxeum.FunctionApp.Features.LeaderBoards
{
    public static class LeaderBoardAddHttpTrigger
    {
        [FunctionName("LeaderBoardAddHttpTrigger")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "leaderboards")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var sr =
                new StreamReader(req.Body);
            var requestBody = 
                await sr.ReadToEndAsync();
            var leaderBoardAdd = 
                JsonConvert.DeserializeObject<LeaderBoardAdd>(requestBody);

            var leaderBoardAddHandler =
                new LeaderBoardAddHandler();

            var leaderBoardAdded =
                await leaderBoardAddHandler.ExecuteAsync(leaderBoardAdd);

            return new OkObjectResult(leaderBoardAdded);
        }
    }
}
