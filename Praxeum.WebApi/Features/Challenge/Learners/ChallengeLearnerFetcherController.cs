using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Praxeum.WebApi.Features.Challenges.Learners
{
    [Route("api/challenges/{challengeId}/learners")]
    [Produces("application/json")]
    [ApiController]
    public class ChallengeLearnerFetcherController : ControllerBase
    {
        private readonly IHandler<ChallengeLearnerFetch, ChallengeLearnerFetched> _challengeLearnerFetcher;

        public ChallengeLearnerFetcherController(
            IHandler<ChallengeLearnerFetch, ChallengeLearnerFetched> challengeLearnerFetcher)
        {
            _challengeLearnerFetcher = challengeLearnerFetcher;
        }

        [HttpGet("{learnerId}", Name = "FetchChallengeLearner")]
        [ProducesResponseType(200, Type = typeof(ChallengeLearnerFetched))]
        [ProducesResponseType(401)]
        [SwaggerOperation(Tags = new[] { "Challenge Learners" })]
        public async Task<IActionResult> FetchAsync(
            [FromRoute] Guid challengeId,
            [FromRoute] Guid learnerId)
        {
            var challengeLearnerFetched =
                await _challengeLearnerFetcher.ExecuteAsync(
                    new ChallengeLearnerFetch
                    {
                        LearnerId = learnerId,
                        ChallengeId = challengeId
                    });

            return Ok(challengeLearnerFetched);
        }
    }
}