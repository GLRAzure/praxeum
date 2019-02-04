using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Praxeum.Domain.LeaderBoards.Learners
{
    [Route("api/leaderboards/{leaderBoardId}/learners")]
    [Produces("application/json")]
    [ApiController]
    public class LeaderBoardLearnerFetcherController : ControllerBase
    {
        private readonly IHandler<LeaderBoardLearnerFetch, LeaderBoardLearnerFetched> _leaderBoardLearnerFetcher;

        public LeaderBoardLearnerFetcherController(
            IHandler<LeaderBoardLearnerFetch, LeaderBoardLearnerFetched> leaderBoardLearnerFetcher)
        {
            _leaderBoardLearnerFetcher = leaderBoardLearnerFetcher;
        }

        [HttpGet("{learnerId}", Name = "FetchLeaderBoardLearner")]
        [ProducesResponseType(200, Type = typeof(LeaderBoardLearnerFetched))]
        [ProducesResponseType(401)]
        [SwaggerOperation(Tags = new[] { "Leader Board Learners" })]
        public async Task<IActionResult> FetchAsync(
            [FromRoute] Guid leaderBoardId,
            [FromRoute] Guid learnerId)
        {
            var leaderBoardLearnerFetched =
                await _leaderBoardLearnerFetcher.ExecuteAsync(
                    new LeaderBoardLearnerFetch
                    {
                        LearnerId = learnerId,
                        LeaderBoardId = leaderBoardId
                    });

            return Ok(leaderBoardLearnerFetched);
        }
    }
}