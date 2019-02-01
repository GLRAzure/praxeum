using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Praxeum.Domain.Learners
{
    [Route("api/learners")]
    [Produces("application/json")]
    [ApiController]
    public class LearnerFetcherController : ControllerBase
    {
        private readonly IHandler<LearnerFetch, LearnerFetched> _learnerFetcher;

        public LearnerFetcherController(
            IHandler<LearnerFetch, LearnerFetched> learnerFetcher)
        {
            _learnerFetcher = learnerFetcher;
        }

        [HttpGet("{id}", Name = "FetchLearner")]
        [ProducesResponseType(200, Type = typeof(LearnerFetched))]
        [ProducesResponseType(401)]
        [SwaggerOperation(Tags = new[] { "Learners" })]
        public async Task<IActionResult> FetchAsync(
            [FromRoute] Guid id)
        {
            var learnerFetched =
                await _learnerFetcher.ExecuteAsync(
                    new LearnerFetch
                    {
                        Id = id
                    });

            return Ok(learnerFetched);
        }
    }
}