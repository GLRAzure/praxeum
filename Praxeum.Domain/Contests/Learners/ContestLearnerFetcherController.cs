using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Praxeum.Domain.Contests.Learners
{
    [Route("api/contests/{contestId}/learners")]
    [Produces("application/json")]
    [ApiController]
    public class ContestLearnerFetcherController : ControllerBase
    {
        private readonly IHandler<ContestLearnerFetch, ContestLearnerFetched> _contestLearnerFetcher;

        public ContestLearnerFetcherController(
            IHandler<ContestLearnerFetch, ContestLearnerFetched> contestLearnerFetcher)
        {
            _contestLearnerFetcher = contestLearnerFetcher;
        }

        [HttpGet("{learnerId}", Name = "FetchContestLearner")]
        [ProducesResponseType(200, Type = typeof(ContestLearnerFetched))]
        [ProducesResponseType(401)]
        [SwaggerOperation(Tags = new[] { "Contest Learners" })]
        public async Task<IActionResult> FetchAsync(
            [FromRoute] Guid contestId,
            [FromRoute] Guid learnerId)
        {
            var contestLearnerFetched =
                await _contestLearnerFetcher.ExecuteAsync(
                    new ContestLearnerFetch
                    {
                        LearnerId = learnerId,
                        ContestId = contestId
                    });

            return Ok(contestLearnerFetched);
        }
    }
}