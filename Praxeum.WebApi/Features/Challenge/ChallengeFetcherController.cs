using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Praxeum.WebApi.Features.Challenges
{
    [Route("api/challenges")]
    [Produces("application/json")]
    [ApiController]
    public class ChallengeFetcherController : ControllerBase
    {
        private readonly IHandler<ChallengeFetch, ChallengeFetched> _challengeFetcher;

        public ChallengeFetcherController(
            IHandler<ChallengeFetch, ChallengeFetched> challengeFetcher)
        {
            _challengeFetcher = challengeFetcher;
        }

        [HttpGet("{id}", Name = "FetchChallenge")]
        [ProducesResponseType(200, Type = typeof(ChallengeFetched))]
        [ProducesResponseType(401)]
        [SwaggerOperation(Tags = new[] { "Challenges" })]
        public async Task<IActionResult> FetchAsync(
            [FromRoute] Guid id)
        {
            var challengeFetched =
                await _challengeFetcher.ExecuteAsync(
                    new ChallengeFetch
                    {
                        Id = id
                    });

            return Ok(challengeFetched);
        }
    }
}