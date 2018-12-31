using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Praxeum.WebApi.Features.LeaderBoards
{
    [Authorize]
    [Route("api/leaderboards")]
    [Produces("application/json")]
    [ApiController]
    public class LeaderBoardFetchListController : ControllerBase
    {
        private readonly ILeaderBoardHandler<LeaderBoardFetchList, IEnumerable<LeaderBoardFetchedList>> _leaderBoardFetchListHandler;

        public LeaderBoardFetchListController(
            ILeaderBoardHandler<LeaderBoardFetchList, IEnumerable<LeaderBoardFetchedList>> leaderBoardFetchListHandler)
        {
            _leaderBoardFetchListHandler = leaderBoardFetchListHandler;
        }

        [HttpGet(Name = "FetchLeaderBoards")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<LeaderBoardFetchedList>))]
        [ProducesResponseType(401)]
        [SwaggerOperation(Tags = new[] { "Leader Boards" })]
        public async Task<IActionResult> FetchListAsync()
        {
            var leaderBoardFetched =
                await _leaderBoardFetchListHandler.ExecuteAsync(
                    new LeaderBoardFetchList());

            return Ok(leaderBoardFetched);
        }
    }
}