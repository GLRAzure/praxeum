using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Praxeum.WebApi.Features.LeaderBoards
{
    [Route("api/leaderboards")]
    [Produces("application/json")]
    [ApiController]
    public class LeaderBoardFetcherController : ControllerBase
    {
        private readonly IHandler<LeaderBoardFetch, LeaderBoardFetched> _leaderBoardFetcher;

        public LeaderBoardFetcherController(
            IHandler<LeaderBoardFetch, LeaderBoardFetched> leaderBoardFetcher)
        {
            _leaderBoardFetcher = leaderBoardFetcher;
        }

        [HttpGet("{id}", Name = "FetchLeaderBoard")]
        [ProducesResponseType(200, Type = typeof(LeaderBoardFetched))]
        [ProducesResponseType(401)]
        [SwaggerOperation(Tags = new[] { "Leader Boards" })]
        public async Task<IActionResult> FetchAsync(
            [FromRoute] Guid id)
        {
            var leaderBoardFetched =
                await _leaderBoardFetcher.ExecuteAsync(
                    new LeaderBoardFetch
                    {
                        Id = id
                    });

            return Ok(leaderBoardFetched);
        }
    }
}